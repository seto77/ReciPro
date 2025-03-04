using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using NativeWindow = OpenTK.Windowing.Desktop.NativeWindow;

namespace OpenTK.GLControl
{
    /// <summary>
    /// OpenGL-capable WinForms control that is a specialized wrapper around
    /// OpenTK's NativeWindow.
    /// </summary>
    public class GLControl : Control
    {
        /// <summary>
        /// The OpenGL configuration of this control.
        /// </summary>
        private GLControlSettings _glControlSettings;

        /// <summary>
        /// The underlying native window.  This will be reparented to be a child of
        /// this control.
        /// </summary>
        private NativeWindow? _nativeWindow = null;

        // Indicates that OnResize was called before OnHandleCreated.
        // To avoid issues with missing OpenGL contexts, we suppress
        // the premature Resize event and raise it as soon as the handle
        // is ready.
        private bool _resizeEventSuppressed;

        /// <summary>
        /// This is used to render the control at design-time, since we cannot
        /// use a real GLFW instance in the WinForms Designer.
        /// </summary>
        private GLControlDesignTimeRenderer? _designTimeRenderer;

        /// <summary>
        /// Gets or sets a value representing the current graphics API.
        /// This value cannot be changed after the control has been initialized (before <see cref="OnHandleCreated(EventArgs)"/> is triggered).
        /// </summary>
        [Category("OpenGL")]
        public ContextAPI API
        {
            get => _nativeWindow?.API ?? _glControlSettings.API;
            set
            {
                if (_nativeWindow == null)
                {
                    _glControlSettings.API = value;
                }
                else
                {
                    throw new InvalidOperationException("Can't set OpenGL settings when the control is initialized.");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value representing the current graphics API profile.
        /// This value cannot be changed after the control has been initialized (before <see cref="OnHandleCreated(EventArgs)"/> is triggered).
        /// </summary>
        [Category("OpenGL")]
        public ContextProfile Profile
        {
            get => _nativeWindow?.Profile ?? _glControlSettings.Profile;
            set
            {
                if (_nativeWindow == null)
                {
                    _glControlSettings.Profile = value;
                }
                else
                {
                    throw new InvalidOperationException("Can't set OpenGL settings when the control is initialized.");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value representing the current graphics profile flags.
        /// This value cannot be changed after the control has been initialized (before <see cref="OnHandleCreated(EventArgs)"/> is triggered).
        /// </summary>
        [Category("OpenGL")]
        public ContextFlags Flags
        {
            get => _nativeWindow?.Flags ?? _glControlSettings.Flags;
            set
            {
                if (_nativeWindow == null)
                {
                    _glControlSettings.Flags = value;
                }
                else
                {
                    throw new InvalidOperationException("Can't set OpenGL settings when the control is initialized.");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value representing the current version of the graphics API.
        /// This value cannot be changed after the control has been initialized (before <see cref="OnHandleCreated(EventArgs)"/> is triggered).
        /// </summary>
        [Category("OpenGL")]
        public Version APIVersion
        {
            get => _nativeWindow?.APIVersion ?? _glControlSettings.APIVersion;
            set
            {
                if (_nativeWindow == null)
                {
                    _glControlSettings.APIVersion = value;
                }
                else
                {
                    throw new InvalidOperationException("Can't set OpenGL settings when the control is initialized.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="GLControl"/> used to share OpenGL resources.
        /// This value cannot be changed after the control has been initialized (before <see cref="OnHandleCreated(EventArgs)"/> is triggered).
        /// </summary>
        [Category("OpenGL")]
        public GLControl? SharedContext
        {
            get => _sharedContext;
            set
            {
                if (_nativeWindow == null)
                {
                    _sharedContext = value;
                }
                else
                {
                    throw new InvalidOperationException("Can't set OpenGL settings when the control is initialized.");
                }
            }
        }
        private GLControl? _sharedContext;

        /// <summary>
        /// Gets the <see cref="IGraphicsContext"/> instance that is associated with the <see cref="GLControl"/>.
        /// </summary>
        [Browsable(false)]
        public IGLFWGraphicsContext? Context => _nativeWindow?.Context;

        /// <summary>
        /// Gets or sets a value indicating whether or not this window is event-driven.
        /// An event-driven window will wait for events before updating/rendering. It is useful for non-game applications,
        /// where the program only needs to do any processing after the user inputs something.
        /// </summary>
        [Category("Behavior")]
        public bool IsEventDriven
        {
            get => _nativeWindow?.IsEventDriven ?? _glControlSettings.IsEventDriven;
            set
            {
                if (value != IsEventDriven)
                {
                    _glControlSettings.IsEventDriven = value;
                    if (IsHandleCreated && _nativeWindow != null)
                    {
                        _nativeWindow.IsEventDriven = value;
                    }
                }
            }
        }

        /// <summary>
        /// The standard DesignMode property is horribly broken; it doesn't work correctly
        /// inside the constructor, and it doesn't work correctly under inheritance or when
        /// a control is contained by another control.  For compatibility reasons, Microsoft
        /// is also unlikely to fix it.  So this properly has *more* correct design-time
        /// behavior, everywhere except the constructor.  It tries several techniques to
        /// figure out if this is design-time or not, and then it caches the result.
        /// </summary>
        /// <remarks>
        /// In future versions of this control we can use
        /// <see href="https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.isancestorsiteindesignmode">IsAncestorSiteInDesignMode</see>
        /// instead of this check.
        /// </remarks>
        [Browsable(false)]
        public bool IsDesignMode
            => _isDesignMode ??= DetermineIfThisIsInDesignMode();
        private bool? _isDesignMode;

        /// <summary>
        /// Gets a value indicating whether the underlying native window was
        /// successfully created.
        /// </summary>
        [Browsable(false)]
        public bool HasValidContext => _nativeWindow != null;

        /// <summary>
        /// Gets the aspect ratio of this GLControl.
        /// </summary>
        [Description("The aspect ratio of the client area of this GLControl.")]
        [Category("Layout")]
        public float AspectRatio
            => Width / (float)Height;

        // Remove the Text property from the WinForms editor.
        [Browsable(false)]
        public override string Text { get => base.Text; set => base.Text = value; }

        /// <summary>
        /// Access to native-input properties and methods, for more direct control
        /// of the keyboard/mouse/joystick than WinForms natively provides.
        /// We don't instantiate this unless someone asks for it.  In general, if you
        /// *can* do input using WinForms, you *should* do input using WinForms.  But
        /// if you need more direct input control, you can use this property instead.
        ///
        /// This property is null by default.  If you need NativeInput, you
        /// *must* use EnableNativeInput to access it.
        /// </summary>
        private NativeInput? _nativeInput;

        /// <summary>
        /// Constructs a new instance with default GLControlSettings.  Various things
        /// that like to use reflection want to have an empty constructor available,
        /// so we offer this constructor rather than just adding `= null` to the
        /// constructor that does the actual construction work.
        /// </summary>
        public GLControl()
            : this(null)
        {
        }

        /// <summary>
        /// Constructs a new instance with the specified GLControlSettings.
        /// </summary>
        /// <param name="glControlSettings">The preferred configuration for the OpenGL
        /// renderer.  If null, 'GLControlSettings.Default' will be used instead.</param>
        public GLControl(GLControlSettings? glControlSettings)
        {
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = false;

            _glControlSettings = glControlSettings != null
                ? glControlSettings.Clone() : new GLControlSettings();
        }

        /// <summary>
        /// This event handler will be invoked by WinForms when the HWND of this
        /// control itself has been created and assigned in the Handle property.
        /// We capture the event to construct the NativeWindow that will be responsible
        /// for all of the actual OpenGL rendering and native device input.
        /// </summary>
        /// <param name="e">An EventArgs instance (ignored).</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            // We don't convert the GLControlSettings to NativeWindowSettings here as that would call GLFW.
            // And this function will be created in design mode.
            CreateNativeWindow(_glControlSettings);

            base.OnHandleCreated(e);

            if (_resizeEventSuppressed)
            {
                OnResize(EventArgs.Empty);
                _resizeEventSuppressed = false;
            }

            if (IsDesignMode)
            {
                _designTimeRenderer = new GLControlDesignTimeRenderer(this);
            }

            if (Focused || (_nativeWindow?.IsFocused ?? false))
            {
                ForceFocusToCorrectWindow();
            }

            IComponentChangeService changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
            if (changeService != null)
            {
                changeService.ComponentChanged -= ChangeService_ComponentChanged; // to avoid multiple subscriptions
                changeService.ComponentChanged += ChangeService_ComponentChanged;
            }
        }

        /// <summary>
        /// This is used to invalidate the control when properties on it change.
        /// This is needed as we display the <see cref="Control.Name"/> in the preview of the control.
        /// If the name changes we need to invalidate the control for it to update accordingly.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A System.ComponentModel.Design.ComponentChangedEventArgs that contains the event data.</param>
        private void ChangeService_ComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            if (e.Component == this && DesignMode)
            {
                Invalidate();
            }
        }

        /// <summary>
        /// Construct the child NativeWindow that will wrap the underlying GLFW instance.
        /// </summary>
        /// <param name="glControlSettings">The settings to use for
        /// the new GLFW window.</param>
        private void CreateNativeWindow(GLControlSettings glControlSettings)
        {
            if (IsDesignMode)
                return;

            if (SharedContext != null)
            {
                if (SharedContext._nativeWindow == null)
                {
                    throw new InvalidOperationException("The GLControl set as the shared context to this GLControl is not yet initialized. Initialization order when sharing contexts is important.");
                }

                _glControlSettings.SharedContext = SharedContext.Context;
            }

            NativeWindowSettings nativeWindowSettings = glControlSettings.ToNativeWindowSettings();

            _nativeWindow = new NativeWindow(nativeWindowSettings);
            _nativeWindow.FocusedChanged += OnNativeWindowFocused;

            NonportableReparent(_nativeWindow);

            // Force the newly child-ified GLFW window to be resized to fit this control.
            ResizeNativeWindow();

            // And now show the child window, since it hasn't been made visible yet.
            _nativeWindow.IsVisible = true;
        }

        /// <summary>
        /// Gets the CreateParams instance for this GLControl.
        /// This is overridden to force correct child behavior.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_VREDRAW = 0x1;
                const int CS_HREDRAW = 0x2;
                const int CS_OWNDC = 0x20;

                CreateParams cp = base.CreateParams;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    cp.ClassStyle |= CS_VREDRAW | CS_HREDRAW | CS_OWNDC;
                }
                return cp;
            }
        }

        /// <summary>
        /// Ensure that the required underlying GLFW window has been created.
        /// </summary>
        // FIXME: In .net5.0+ we could add this attribute.
        // This is not strictly true in DesignMode, but maybe it's better than nothing?
        //[MemberNotNull("_nativeWindow")]
        private void EnsureCreated()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (!IsHandleCreated)
            {
                CreateControl();

                if (_nativeWindow == null)
                    throw new InvalidOperationException("Failed to create GLControl."
                        + " This is usually caused by trying to perform operations on the GLControl"
                        + " before its containing form has been fully created.  Make sure you are not"
                        + " invoking methods on it before the Form's constructor has completed.");
            }

            if (_nativeWindow == null && !IsDesignMode)
            {
                RecreateHandle();

                if (_nativeWindow == null)
                    throw new InvalidOperationException("Failed to recreate GLControl :-(");
            }
        }

        /// <summary>
        /// Because we're really two windows in one, keyboard-focus is a complex
        /// topic.  To ensure correct behavior, we have to capture the various attempts
        /// to assign focus to one or the other window, and if focus is sent to the
        /// wrong window, we have to redirect it to the correct one.  So every attempt
        /// to set focus to *either* window will trigger this method, which will force
        /// the focus to whichever of the two windows it's supposed to be on.
        /// </summary>
        private void ForceFocusToCorrectWindow()
        {
            if (IsDesignMode || _nativeWindow == null)
                return;

            unsafe
            {
                if (IsNativeInputEnabled(_nativeWindow))
                {
                    // Focus should be on the NativeWindow inside the GLControl.
                    _nativeWindow.Focus();
                }
                else
                {
                    // Focus should be on the GLControl itself.
                    Focus();
                }
            }
        }

        /// <summary>
        /// Reparent the given NativeWindow to be a child of this GLControl.  This is a
        /// non-portable operation, as its name implies:  It works wildly differently
        /// between OSes.  The current implementation only supports Microsoft Windows.
        /// </summary>
        /// <param name="nativeWindow">The NativeWindow that must become a child of this control.</param>
        private unsafe void NonportableReparent(NativeWindow nativeWindow)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                IntPtr hWnd = GLFW.GetWin32Window(nativeWindow.WindowPtr);

                // Reparent the real HWND under this control.
                Win32.SetParent(hWnd, Handle);

                // Change the real HWND's window styles to be "WS_CHILD | WS_DISABLED" (i.e.,
                // a child of some container, with no input support), and turn off *all* the
                // other style bits (most of the rest of them could cause trouble).  In
                // particular, this turns off stuff like WS_BORDER and WS_CAPTION and WS_POPUP
                // and so on, any of which GLFW might have turned on for us.
                IntPtr style = (IntPtr)(long)(Win32.WindowStyles.WS_CHILD
                    | Win32.WindowStyles.WS_DISABLED);
                Win32.SetWindowLongPtr(hWnd, Win32.WindowLongs.GWL_STYLE, style);

                // Change the real HWND's extended window styles to be "WS_EX_NOACTIVATE", and
                // turn off *all* the other extended style bits (most of the rest of them
                // could cause trouble).  We want WS_EX_NOACTIVATE because we don't want
                // Windows mistakenly giving the GLFW window the focus as soon as it's created,
                // regardless of whether it's a hidden window.
                style = (IntPtr)(long)Win32.WindowStylesEx.WS_EX_NOACTIVATE;
                Win32.SetWindowLongPtr(hWnd, Win32.WindowLongs.GWL_EXSTYLE, style);
            }
            else throw new NotSupportedException("The current operating system is not supported by this control.");
        }

        /// <summary>
        /// Enable/disable NativeInput for the given NativeWindow.
        /// </summary>
        /// <param name="isEnabled">Whether NativeInput support should be enabled or disabled.</param>
        private unsafe void EnableNativeInput(NativeWindow nativeWindow, bool isEnabled)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                IntPtr hWnd = GLFW.GetWin32Window(nativeWindow.WindowPtr);

                // Tweak the WS_DISABLED style bit for the native window.  When enabled,
                // it will eat all input events directed to it.  When disabled, events will
                // "pass through" to the parent window (i.e., our WinForms control).
                IntPtr style = Win32.GetWindowLongPtr(hWnd, Win32.WindowLongs.GWL_STYLE);
                if (isEnabled)
                {
                    style = (IntPtr)((Win32.WindowStyles)(long)style & ~Win32.WindowStyles.WS_DISABLED);
                }
                else
                {
                    style = (IntPtr)((Win32.WindowStyles)(long)style | Win32.WindowStyles.WS_DISABLED);
                }
                Win32.SetWindowLongPtr(hWnd, Win32.WindowLongs.GWL_STYLE, style);
            }
            else throw new NotSupportedException("The current operating system is not supported by this control.");
        }

        /// <summary>
        /// Determine if native input is enabled for the given NativeWindow.
        /// </summary>
        /// <param name="nativeWindow">The NativeWindow to query.</param>
        /// <returns>True if native input is enabled; false if it is not.</returns>
        private unsafe bool IsNativeInputEnabled(NativeWindow nativeWindow)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                IntPtr hWnd = GLFW.GetWin32Window(nativeWindow.WindowPtr);
                IntPtr style = Win32.GetWindowLongPtr(hWnd, Win32.WindowLongs.GWL_STYLE);
                return ((Win32.WindowStyles)(long)style & Win32.WindowStyles.WS_DISABLED) == 0;
            }
            else throw new NotSupportedException("The current operating system is not supported by this control.");
        }

        /// <summary>
        /// A fix for the badly-broken DesignMode property, this answers (somewhat more
        /// reliably) whether this is DesignMode or not.  This does *not* work when invoked
        /// from the GLControl's constructor.
        /// </summary>
        /// <returns>True if this is in design mode, false if it is not.</returns>
        private bool DetermineIfThisIsInDesignMode()
        {
            // The obvious test.
            if (DesignMode)
                return true;

            // This works on .NET Framework but no longer seems to work reliably on .NET Core.
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;

            // Try walking the control tree to see if any ancestors are in DesignMode.
            for (Control control = this; control != null; control = control.Parent)
            {
                if (control.Site != null && control.Site.DesignMode)
                    return true;
            }

            // Try checking for `IDesignerHost` in the service collection.
            if (GetService(typeof(System.ComponentModel.Design.IDesignerHost)) != null)
                return true;

            // Last-ditch attempt:  Is the process named `devenv` or `VisualStudio`?
            // These are bad, hacky tests, but they *can* work sometimes.
            if (System.Reflection.Assembly.GetExecutingAssembly().Location.Contains("VisualStudio", StringComparison.OrdinalIgnoreCase))
                return true;
            if (string.Equals(System.Diagnostics.Process.GetCurrentProcess().ProcessName, "devenv", StringComparison.OrdinalIgnoreCase))
                return true;

            // Nope.  Not design mode.  Probably.  Maybe.
            return false;
        }

        /// <summary>
        /// This is triggered when the underlying Handle/HWND instance is *about to be*
        /// destroyed (this is called *before* the Handle/HWND is destroyed).  We use it
        /// to cleanly destroy the NativeWindow before its parent disappears.
        /// </summary>
        /// <param name="e">An EventArgs instance (ignored).</param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);

            DestroyNativeWindow();
        }

        /// <summary>
        /// Destroy the child NativeWindow that wraps the underlying GLFW instance.
        /// </summary>
        private void DestroyNativeWindow()
        {
            if (_nativeWindow != null)
            {
                _nativeWindow.Dispose();
                _nativeWindow = null!;
            }
        }

        /// <summary>
        /// This private object is used as the reference for the 'Load' handler in
        /// the Events collection, and is only needed if you use the 'Load' event.
        /// </summary>
        private static readonly object EVENT_LOAD = new();

        /// <summary>
        /// An event hook, triggered when the control is created for the first time.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the GLControl is first created.")]
        public event EventHandler Load
        {
            add => Events.AddHandler(EVENT_LOAD, value);
            remove => Events.RemoveHandler(EVENT_LOAD, value);
        }

        /// <summary>
        /// Raises the CreateControl event.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            OnLoad(EventArgs.Empty);
        }

        /// <summary>
        /// The Load event is fired before the control becomes visible for the first time.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnLoad(EventArgs e)
        {
            // There is no good way to explain this event except to say
            // that it's just another name for OnControlCreated.
            ((EventHandler)Events[EVENT_LOAD])?.Invoke(this, e);
        }

        /// <summary>
        /// This is raised by WinForms to paint this instance.
        /// </summary>
        /// <param name="e">A PaintEventArgs object that describes which areas
        /// of the control need to be painted.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            EnsureCreated();

            if (IsDesignMode)
            {
                _designTimeRenderer?.Paint(e.Graphics);
            }

            base.OnPaint(e);
        }

        /// <summary>
        /// This is invoked when the Resize event is triggered, and is used to position
        /// the internal GLFW window accordingly.
        ///
        /// Note: This method may be called before the OpenGL context is ready or the
        /// NativeWindow even exists, so everything inside it requires safety checks.
        /// </summary>
        /// <param name="e">An EventArgs instance (ignored).</param>
        protected override void OnResize(EventArgs e)
        {
            // Do not raise OnResize event before the handle and context are created.
            if (!IsHandleCreated)
            {
                _resizeEventSuppressed = true;
                return;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                BeginInvoke(new Action(ResizeNativeWindow)); // Need the native window to resize first otherwise our control will be in the wrong place.
            }
            else
            {
                ResizeNativeWindow();
            }

            base.OnResize(e);
        }

        /// <summary>
        /// Resize the native window to fit this control.
        /// </summary>
        private void ResizeNativeWindow()
        {
            if (IsDesignMode)
                return;

            if (_nativeWindow != null)
            {
                _nativeWindow.ClientRectangle = new Box2i(0, 0, Width, Height);
            }
        }

        /// <summary>
        /// This event is raised when this control's parent control is changed,
        /// which may result in this control becoming a different size or shape, so
        /// we capture it to ensure that the underlying GLFW window gets correctly
        /// resized and repositioned as well.
        /// </summary>
        /// <param name="e">An EventArgs instance (ignored).</param>
        protected override void OnParentChanged(EventArgs e)
        {
            ResizeNativeWindow();

            base.OnParentChanged(e);
        }

        /// <summary>
        /// This event is raised when something sets the focus to the GLControl.
        /// It is overridden to potentially force the focus to the NativeWindow, if
        /// necessary.
        /// </summary>
        /// <param name="e">An EventArgs instance (ignored).</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            if (!ReferenceEquals(e, _noRecursionSafetyArgs))
            {
                ForceFocusToCorrectWindow();
            }
        }

        /// <summary>
        /// These EventArgs are used as a safety check to prevent unexpected recursion
        /// in OnGotFocus.
        /// </summary>
        private static readonly EventArgs _noRecursionSafetyArgs = new();

        /// <summary>
        /// This event is raised when something sets the focus to the NativeWindow.
        /// It is overridden to potentially force the focus to the GLControl, if
        /// necessary.
        /// </summary>
        /// <param name="e">A FocusChangedEventArgs instance, used to detect if the
        /// NativeWindow is gaining the focus.</param>
        private void OnNativeWindowFocused(FocusedChangedEventArgs e)
        {
            if (e.IsFocused)
            {
                ForceFocusToCorrectWindow();
                OnGotFocus(_noRecursionSafetyArgs);
            }
            else
            {
                OnLostFocus(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Swaps the front and back buffers, presenting the rendered scene to the user.
        /// </summary>
        public void SwapBuffers()
        {
            if (IsDesignMode)
                return;

            EnsureCreated();
            // FIXME: See [MemberNotNull] comment on EnsureCreated().
            if (_nativeWindow == null)
                throw new Exception("EnsureCreated() failed to create _nativeWindow. This is a bug.");
            _nativeWindow.Context.SwapBuffers();
        }

        /// <summary>
        /// Makes this control's OpenGL context current in the calling thread.
        /// All OpenGL commands issued are hereafter interpreted by this context.
        /// When using multiple GLControls, calling MakeCurrent on one control
        /// will make all other controls non-current in the calling thread.
        /// A GLControl can only be current in one thread at a time.
        /// </summary>
        public void MakeCurrent()
        {
            if (IsDesignMode)
                return;

            EnsureCreated();
            // FIXME: See [MemberNotNull] comment on EnsureCreated().
            if (_nativeWindow == null)
                throw new Exception("EnsureCreated() failed to create _nativeWindow. This is a bug.");
            _nativeWindow.MakeCurrent();
        }

        /// <summary>
        /// Access to native-input properties and methods, for more direct control
        /// of the keyboard/mouse/joystick than WinForms natively provides.
        /// We don't enable this unless someone asks for it.  In general, if you
        /// *can* do input using WinForms, you *should* do input using WinForms.  But
        /// if you need more direct input control, you can use this property instead.
        ///
        /// Note that enabling native input causes *normal* WinForms input methods to
        /// stop working for this GLControl -- all input for will be sent through the
        /// NativeInput interface instead.
        /// </summary>
        public INativeInput EnableNativeInput()
        {
            EnsureCreated();
            // FIXME: See [MemberNotNull] comment on EnsureCreated().
            if (_nativeWindow == null)
                throw new Exception("EnsureCreated() failed to create _nativeWindow. This is a bug.");

            _nativeInput ??= new NativeInput(_nativeWindow);

            if (!IsNativeInputEnabled(_nativeWindow))
            {
                EnableNativeInput(_nativeWindow, true);
            }

            if (Focused || _nativeWindow.IsFocused)
            {
                ForceFocusToCorrectWindow();
            }

            return _nativeInput;
        }

        /// <summary>
        /// Disable native input support, and return to using WinForms for all
        /// keyboard/mouse input.  Any INativeInput interface you may have access
        /// to will no longer work propertly until you call EnableNativeInput() again.
        /// </summary>
        public void DisableNativeInput()
        {
            EnsureCreated();
            // FIXME: See [MemberNotNull] comment on EnsureCreated().
            if (_nativeWindow == null)
                throw new Exception("EnsureCreated() failed to create _nativeWindow. This is a bug.");

            if (IsNativeInputEnabled(_nativeWindow))
            {
                EnableNativeInput(_nativeWindow, false);
            }

            if (Focused || _nativeWindow.IsFocused)
            {
                ForceFocusToCorrectWindow();
            }
        }

    }
}
