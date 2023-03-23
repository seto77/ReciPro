using MemoryPack.Compression;
using MemoryPack;
using Microsoft.Win32;

namespace Crystallography;

public static class Reg
{
    public enum Mode { Read, Write };
    public static void RW<T>(RegistryKey key, Mode mode, object owner, string propName)
    {
        if (owner == null)
            return;
        var prop = owner.GetType().GetProperty(propName);
        var regName = $"{prop.ReflectedType.FullName}.{propName}";
        if (mode == Mode.Read)
        {//読込の時
            var buffer = (byte[])key.GetValue(regName);
            if (buffer == null)
                return;
            using var decompressor = new BrotliDecompressor();
            var val = MemoryPackSerializer.Deserialize<T>(decompressor.Decompress(buffer));
            prop.SetValue(owner, val);
        }

        else
        {//書込の時
            using var compressor = new BrotliCompressor(System.IO.Compression.CompressionLevel.SmallestSize);
            MemoryPackSerializer.Serialize(compressor, (T)prop.GetValue(owner));
            key.SetValue(regName, compressor.ToArray());
        }
    }



}
