using MemoryPack.Compression;
using MemoryPack;
using Microsoft.Win32;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Crystallography;

public static class Reg
{
    public enum Mode { Read, Write };
    public static void RW<T>(RegistryKey key, Mode mode, object owner, string propName)
    {
        if (owner == null)
            return;
        var prop = owner.GetType().GetProperty(propName);

        string regName;

        if(owner is Control c)
        {
            if (c.TopLevelControl != null && c.TopLevelControl.Name != c.Name)
                regName = c.TopLevelControl.Name + "." + c.Name + "." + propName;
            else
                regName = c.TopLevelControl.Name + "." + propName;
        }
        else if (owner is ToolStripItem t)
        {
            regName = t.Name + "." + propName;
        }
        else
            regName = $"{prop.ReflectedType.FullName}.{propName}";

        if (mode == Mode.Read)
        {//読込の時

            var buffer = (byte[])key.GetValue(regName);
            if (buffer == null)
                return;
            using var decompressor = new BrotliDecompressor();
            var val = MemoryPackSerializer.Deserialize<T>(decompressor.Decompress(buffer));
            if (regName != "System.Globalization.CultureInfo.Name")
                prop.SetValue(owner, val);
            else
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(val.ToString().ToLower().StartsWith("ja") ? "ja" : "en");
            }
        }

        else
        {//書込の時
            using var compressor = new BrotliCompressor(System.IO.Compression.CompressionLevel.SmallestSize);
            MemoryPackSerializer.Serialize(compressor, (T)prop.GetValue(owner));
            key.SetValue(regName, compressor.ToArray());
        }
    }



}
