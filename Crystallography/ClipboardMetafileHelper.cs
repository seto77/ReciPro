using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Crystallography
{
    public class ClipboardMetafileHelper
    {
        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        private static extern bool EmptyClipboard();

        [DllImport("user32.dll")]
        private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

        [DllImport("user32.dll")]
        private static extern bool CloseClipboard();

        [DllImport("gdi32.dll")]
        private static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, IntPtr hNULL);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteEnhMetaFile(IntPtr hemf);

        // Metafile mf is set to an invalid state inside this function
        static public bool PutEnhMetafileOnClipboard(IntPtr hWnd, Metafile mf)
        {
            bool bResult = false;
            IntPtr hEMF, hEMF2;
            hEMF = mf.GetHenhmetafile(); // invalidates mf
            if (!hEMF.Equals(IntPtr.Zero))
            {
                hEMF2 = CopyEnhMetaFile(hEMF, new IntPtr(0));
                if (!hEMF2.Equals(IntPtr.Zero) && OpenClipboard(hWnd) && EmptyClipboard())
                {
                    IntPtr hRes = SetClipboardData(14 /*CF_ENHMETAFILE*/, hEMF2);
                    bResult = hRes.Equals(hEMF2);
                    CloseClipboard();
                }
                DeleteEnhMetaFile(hEMF);
            }
            return bResult;
        }
    }
}