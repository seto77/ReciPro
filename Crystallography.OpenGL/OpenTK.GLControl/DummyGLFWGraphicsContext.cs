using System;
using OpenTK.Windowing.Desktop;

namespace OpenTK.GLControl
{
    /// <summary>
    /// At design-time, we don't have a real GLFW graphics context.
    /// We use this stub instead, which does nothing but prevent crashes.
    /// </summary>
    internal class DummyGLFWGraphicsContext : IGLFWGraphicsContext
    {
        /// <summary>
        /// The one-and-only instance of this class.
        /// </summary>
        public static DummyGLFWGraphicsContext Instance { get; }  = new DummyGLFWGraphicsContext();

        /// <summary>
        /// The mandatory WindowPtr, which is always a null handle.
        /// </summary>
        public IntPtr WindowPtr => IntPtr.Zero;

        /// <summary>
        /// A fake IsCurrent flag, which just stores its last usage.
        /// </summary>
        public bool IsCurrent { get; private set; }

        public int SwapInterval { get; set; }

        /// <summary>
        /// This can only be constructed internally.
        /// </summary>
        private DummyGLFWGraphicsContext()
        {
        }

        /// <summary>
        /// Make this graphics context "current."  This does mostly nothing.
        /// </summary>
        public void MakeCurrent()
            => IsCurrent = true;

        /// <summary>
        /// Make *no* graphics context "current."  This does mostly nothing.
        /// </summary>
        public void MakeNoneCurrent()
            => IsCurrent = false;

        /// <summary>
        /// Swap the displayed buffer.  This does *literally* nothing.
        /// </summary>
        public void SwapBuffers()
        {
        }
    }
}
