using MemoryPack;
using MemoryPack.Compression;
using Microsoft.Win32;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Crystallography;

public static class Reg
{
    public enum Mode { Read, Write };

    // 呼び出し側を Reg.RW(key, mode, () => owner.Prop) と書けるようにするオーバーロード。
    public static void RW<T>(RegistryKey key, Mode mode, Expression<Func<T>> expr)
    {
        if (expr.Body is not MemberExpression me) return;
        var owner = EvaluateExpression(me.Expression);
        if (owner == null) return;
        RW<T>(key, mode, owner, me.Member.Name);
    }

    // Expression.Lambda().Compile() は 1 呼び出しあたり数 ms かかる。
    // レジストリ読み書きは FormMain.Registry(mode) で 70+ 回連続呼び出しされるため、
    // Compile を避けて MemberExpression チェーンを手で辿る。
    private static object EvaluateExpression(Expression expr)
    {
        return expr switch
        {
            null => null,
            ConstantExpression ce => ce.Value,
            MemberExpression me => me.Member switch
            {
                FieldInfo fi => fi.GetValue(EvaluateExpression(me.Expression)),
                PropertyInfo pi => pi.GetValue(EvaluateExpression(me.Expression)),
                _ => Expression.Lambda(expr).Compile().DynamicInvoke()
            },
            _ => Expression.Lambda(expr).Compile().DynamicInvoke()
        };
    }

    public static void RW<T>(RegistryKey key, Mode mode, object owner, string propName)
    {
        if (owner == null)
            return;
        // PropertyInfo が無ければ FieldInfo にフォールバック (public field を rw(() => ...) から扱うため)
        var ownerType = owner.GetType();
        var prop = ownerType.GetProperty(propName);
        var field = prop == null ? ownerType.GetField(propName) : null;
        if (prop == null && field == null)
            return;

        string regName;

        if (owner is Control c)
        {
            if (c.TopLevelControl != null && c.TopLevelControl.Name != c.Name)
                regName = c.TopLevelControl.Name + "." + c.Name + "." + propName;
            else
                regName = c.TopLevelControl.Name + "." + propName;
        }
        else if (owner is ToolStripItem t)
            regName = t.Name + "." + propName;
        else
            regName = $"{(prop?.ReflectedType ?? field.ReflectedType).FullName}.{propName}";

        if (mode == Mode.Read)
        {//読込の時
            try
            {
                var buffer = (byte[])key.GetValue(regName);

                if (buffer == null)
                    return;
                using var decompressor = new BrotliDecompressor();
                var val = MemoryPackSerializer.Deserialize<T>(decompressor.Decompress(buffer));
                if (regName != "System.Globalization.CultureInfo.Name")
                {
                    if (prop != null)
                        prop.SetValue(owner, val);
                    else
                        field.SetValue(owner, val);
                }
                else
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(val.ToString().ToLower().StartsWith("ja") ? "ja" : "en");
                }
            }
            catch { return; }
        }

        else
        {//書込の時
            using var compressor = new BrotliCompressor(System.IO.Compression.CompressionLevel.Optimal);
            var value = prop != null ? (T)prop.GetValue(owner) : (T)field.GetValue(owner);
            MemoryPackSerializer.Serialize(compressor, value);
            key.SetValue(regName, compressor.ToArray());
        }
    }
}
