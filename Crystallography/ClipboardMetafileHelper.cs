using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        // 260504Cl 追加: 任意の描画アクションを EMF+ 化してクリップボードへ書き込む。
        // 既存 5 箇所 (ScalablePictureBox / FormStereonet / FormDiffractionSimulator / FormImageSimulator
        // / FormSymmetryInformation) で同じ HDC→Metafile→PutEnh… の手順が複製されていたので集約。
        // draw 引数では SmoothingMode / Clear など Graphics の初期状態を呼び出し側で設定する。
        public static bool PutDrawingOnClipboardAsEnhMetafile(IntPtr hWnd, Action<Graphics> draw)
        {
            ArgumentNullException.ThrowIfNull(draw);
            using var refG = Graphics.FromHwnd(hWnd);
            IntPtr hdc = refG.GetHdc();
            using var ms = new MemoryStream();
            using var mf = new Metafile(ms, hdc, EmfType.EmfPlusDual);
            refG.ReleaseHdc(hdc);
            using (var g = Graphics.FromImage(mf))
                draw(g);
            return PutEnhMetafileOnClipboard(hWnd, mf);
        }
    }
}