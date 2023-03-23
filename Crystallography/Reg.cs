using MemoryPack.Compression;
using MemoryPack;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        var regName = $"{prop.ReflectedType.FullName}.{propName}";
        //try
        //{
        if (mode == Mode.Read)
        {
            var buffer = (byte[])key.GetValue(regName);
            if (buffer == null)
                return;
            using var decompressor = new BrotliDecompressor();
            prop.SetValue(owner, MemoryPackSerializer.Deserialize<T>(decompressor.Decompress(buffer)));
        }

        else
        {
            using var compressor = new BrotliCompressor(System.IO.Compression.CompressionLevel.SmallestSize);
            MemoryPackSerializer.Serialize(compressor, (T)prop.GetValue(owner));
            key.SetValue(regName, compressor.ToArray());
        }
        //}
        //catch { MessageBox.Show("Registry I/O error!"); }
    }



}
