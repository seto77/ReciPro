using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace OpenTK.GLControl
{
    /// <summary>
    /// Configuration settings for a GLControl.  The properties here are a subset
    /// of the NativeWindowSettings properties, restricted to those that make
    /// sense in a WinForms environment.
    /// </summary>
    public class GLControlSettings
    {
        /// <summary>
        /// Gets the default settings for a <see cref="GLControl"/>.
        /// </summary>
        public static readonly GLControlSettings Default = new GLControlSettings();

        /// <summary>
        /// Gets or sets a value representing the current version of the graphics API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// OpenGL 3.3 is selected by default, and runs on almost any hardware made within the last ten years.
        /// This will run on Windows, Mac OS, and Linux.
        /// </para>
        /// <para>
        /// OpenGL 4.1 is suggested for modern apps meant to run on more modern hardware.
        /// This will run on Windows, Mac OS, and Linux.
        /// </para>
        /// <para>
        /// OpenGL 4.6 is suggested for modern apps that only intend to run on Windows and Linux;
        /// Mac OS doesn't support it.
        /// </para>
        /// <para>
        /// Note that if you choose an API other than base OpenGL, this will need to be updated accordingly,
        /// as the versioning of OpenGL and OpenGL ES do not match.
        /// </para>
        /// </remarks>
        public Version APIVersion { get; set; } = new Version(3, 3, 0, 0);

        /// <summary>
        /// Gets or sets a value indicating whether or not OpenGL bindings should be automatically loaded
        /// when the window is created.
        /// </summary>
        public bool AutoLoadBindings { get; set; } = true;

        /// <summary>
        /// Gets or sets a value representing the current graphics profile flags.
        /// </summary>
        public ContextFlags Flags { get; set; } = ContextFlags.Default;

        /// <summary>
        /// Gets or sets a value representing the current graphics API profile.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This only has an effect on OpenGL 3.2 and higher. On older versions, this setting does nothing.
        /// </para>
        /// </remarks>
        public ContextProfile Profile { get; set; } = ContextProfile.Core;

        /// <summary>
        /// Gets or sets a value representing the current graphics API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this is changed, you'll have to modify the API version as well, as the versioning of OpenGL and OpenGL ES
        /// do not match.
        /// </para>
        /// </remarks>
        public ContextAPI API { get; set; } = ContextAPI.OpenGL;

        /// <summary>
        /// Gets or sets a value indicating whether or not this window is event-driven.
        /// An event-driven window will wait for events before updating/rendering. It is useful for non-game applications,
        /// where the program only needs to do any processing after the user inputs something.
        /// </summary>
        public bool IsEventDriven { get; set; } = true;

        /// <summary>
        /// Gets or sets the context to share.
        /// </summary>
        public IGLFWGraphicsContext? SharedContext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the number of samples that should be used.
        /// </summary>
        /// <remarks>
        /// <c>0</c> indicates that no multisampling should be used;
        /// otherwise multisampling is used if available. The actual number of samples is the closest matching the given number that is supported.
        /// </remarks>
        public int NumberOfSamples { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the number of stencil bits used for OpenGL context creation.
        /// </summary>
        /// <remarks>
        /// Default value is 8.
        /// </remarks>
        public int? StencilBits { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the number of depth bits used for OpenGL context creation.
        /// </summary>
        /// <remarks>
        /// Default value is 24.
        /// </remarks>
        public int? DepthBits { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the number of red bits used for OpenGL context creation.
        /// </summary>
        /// <remarks>
        /// Default value is 8.
        /// </remarks>
        public int? RedBits { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the number of green bits used for OpenGL context creation.
        /// </summary>
        /// <remarks>
        /// Default value is 8.
        /// </remarks>
        public int? GreenBits { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the number of blue bits used for OpenGL context creation.
        /// </summary>
        /// <remarks>
        /// Default value is 8.
        /// </remarks>
        public int? BlueBits { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the number of alpha bits used for OpenGL context creation.
        /// </summary>
        /// <remarks>
        /// Default value is 8.
        /// </remarks>
        public int? AlphaBits { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the backbuffer should be sRGB capable.
        /// </summary>
        public bool SrgbCapable { get; set; }

        /// <summary>
        /// Make a perfect shallow copy of this object.
        /// </summary>
        /// <returns>A perfect shallow copy of this GLControlSettings object.</returns>
        public GLControlSettings Clone()
        {
            return new GLControlSettings()
            {
                APIVersion = APIVersion,
                AutoLoadBindings = AutoLoadBindings,
                Flags = Flags,
                Profile = Profile,
                API = API,
                IsEventDriven = IsEventDriven,
                SharedContext = SharedContext,
                NumberOfSamples = NumberOfSamples,
                StencilBits = StencilBits,
                DepthBits = DepthBits,
                RedBits = RedBits,
                GreenBits = GreenBits,
                BlueBits = BlueBits,
                AlphaBits = AlphaBits,
                SrgbCapable = SrgbCapable,
            };
        }

        /// <summary>
        /// Derive a NativeWindowSettings object from this GLControlSettings object.
        /// The NativeWindowSettings has all of our properties and more, but many of
        /// its properties cannot be reasonably configured by the user when a
        /// NativeWindow is being used as a child window.
        /// </summary>
        /// <returns>The NativeWindowSettings to use when constructing a new
        /// NativeWindow.</returns>
        public NativeWindowSettings ToNativeWindowSettings()
        {
            return new NativeWindowSettings()
            {
                APIVersion = FixupVersion(APIVersion),
                AutoLoadBindings = AutoLoadBindings,
                Flags = Flags,
                Profile = Profile,
                API = API,
                IsEventDriven = IsEventDriven,
                SharedContext = SharedContext,
                NumberOfSamples = NumberOfSamples,
                StencilBits = StencilBits,
                DepthBits = DepthBits,
                RedBits = RedBits,
                GreenBits = GreenBits,
                BlueBits = BlueBits,
                AlphaBits = AlphaBits,
                SrgbCapable = SrgbCapable,

                StartFocused = false,
                StartVisible = false,
                WindowBorder = WindowBorder.Hidden,
                WindowState = WindowState.Normal,
            };
        }

        /// <summary>
        /// The WinForms Designer has bugs when it comes to editing Version objects:
        /// Many times when a component is left out, it is treated not as 0, but as -1!
        /// So this little method corrects for bad data from the WinForms designer.
        /// </summary>
        /// <param name="version">A version number.</param>
        /// <returns>The same version number, but with all negative values clipped to 0.</returns>
        private static Version FixupVersion(Version version)
        {
            return new Version(
                Math.Max(version.Major, 0),
                Math.Max(version.Minor, 0),
                Math.Max(version.Build, 0),
                Math.Max(version.Revision, 0)
            );
        }
    }
}
