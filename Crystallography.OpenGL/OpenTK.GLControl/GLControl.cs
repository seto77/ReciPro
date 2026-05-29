#nullable enable //260317Cl 追加
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
        /// <summary>The OpenGL configuration of this control.</summary>
        private GLControlSettings _glControlSettings;

        /// <summary>
        /// The underlying native window.  This will be reparented to be a child of
        /// this control.
        /// </summary>
        private NativeWindow? _nativeWindow = null;

        // 260529Cl 追加: GLFW 子ウィンドウの Win32 HWND をキャッシュする。
        // Windows on ARM (x64 エミュ) では GLFW 経由のリサイズが親と
        // 一致しないことがあるため、Win32 API で直接サイズ強制するのに使う。
        private IntPtr _nativeHwnd = IntPtr.Zero;

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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
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

        /// <summary>Gets the <see cref="IGraphicsContext"/> instance that is associated with the <see cref="GLControl"/>.</summary>
        [Browsable(false)]
        public IGLFWGraphicsContext? Context => _nativeWindow?.Context;

        /// <summary>
        /// Gets or sets a value indicating whether or not this window is event-driven.
        /// An event-driven window will wait for events before updating/rendering. It is useful for non-game applications,
        /// where the program only needs to do any processing after the user inputs something.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //260405Cl 追加
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

        /// <summary>Gets the aspect ratio of this GLControl.</summary>
        [Description("The aspect ratio of the client area of this GLControl.")]
        [Category("Layout")]
        public float AspectRatio
            => Width / (float)Height;

        // Remove the Text property from the WinForms editor.
        [Browsable(false)]
#pragma warning disable CS8764, CS8765 //260317Cl 基底クラス(Control.Text)がnullable非対応のため抑制
        public override string? Text { get => base.Text; set => base.Text = value; }
#pragma warning restore CS8764, CS8765

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

        /// <summary>Constructs a new instance with the specified GLControlSettings.</summary>
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
            GLDebugLog.Log(LogName, "OnHandleCreated/enter", SnapshotSize());
            CreateNativeWindow(_glControlSettings);
            GLDebugLog.Log(LogName, "OnHandleCreated/afterCreate", SnapshotSize());

            base.OnHandleCreated(e);

            if (_resizeEventSuppressed)
            {
                GLDebugLog.Log(LogName, "OnHandleCreated/suppressedResize", SnapshotSize());
                OnResize(EventArgs.Empty);
                _resizeEventSuppressed = false;
            }
            GLDebugLog.Log(LogName, "OnHandleCreated/exit", SnapshotSize());

            if (IsDesignMode)
            {
                _designTimeRenderer = new GLControlDesignTimeRenderer(this);
            }

            if (Focused || (_nativeWindow?.IsFocused ?? false))
            {
                ForceFocusToCorrectWindow();
            }

            IComponentChangeService? changeService = (IComponentChangeService?)GetService(typeof(IComponentChangeService)); //260317Cl nullable化
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
        private void ChangeService_ComponentChanged(object? sender, ComponentChangedEventArgs e) //260317Cl nullable化
        {
            if (e.Component == this && DesignMode)
            {
                Invalidate();
            }
        }

        /// <summary>Construct the child NativeWindow that will wrap the underlying GLFW instance.</summary>
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
            GLDebugLog.Log(LogName, "CreateNW/settings", $"settings.ClientSize=({nativeWindowSettings.ClientSize.X},{nativeWindowSettings.ClientSize.Y}) StartVisible={nativeWindowSettings.StartVisible}");

            _nativeWindow = new NativeWindow(nativeWindowSettings);
            _nativeWindow.FocusedChanged += OnNativeWindowFocused;
            _nativeWindow.FramebufferResize += OnNativeFramebufferResize;
            GLDebugLog.Log(LogName, "CreateNW/nwCreated", SnapshotSize());

            NonportableReparent(_nativeWindow);
            GLDebugLog.Log(LogName, "CreateNW/reparented", SnapshotSize());

            // Force the newly child-ified GLFW window to be resized to fit this control.
            ResizeNativeWindow();
            GLDebugLog.Log(LogName, "CreateNW/afterResize", SnapshotSize());

            // And now show the child window, since it hasn't been made visible yet.
            _nativeWindow.IsVisible = true;
            GLDebugLog.Log(LogName, "CreateNW/afterShow", SnapshotSize());

            // 260529Cl 追加: 一部環境 (Windows on ARM x64 エミュ等) で、hidden 状態の MoveWindow が
            // OpenGL の back buffer 再確保まで波及せず、GL 既定の初期サイズ (NativeWindowSettings 既定 = 800×600 等) で
            // back buffer が固定されてしまう現象が確認されている。Anchor=Top|Right などサイズ追随しない GLControl だと
            // 初回ハンドル作成時の ResizeNativeWindow 以降一切 WM_SIZE が来ないため、この初期不整合がそのまま固定化し、
            // アスペクト比のズレや黒帯やはみ出しとして可視化する。ここで明示的に "違うサイズ → 目標サイズ" を 2 段送って
            // WM_SIZE を確実に発火させ、表示済み window の back buffer を実寸に同期させる。
            // (同サイズ MoveWindow は Windows が WM_SIZE を抑制するため、必ず一旦別サイズを経由する)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && _nativeHwnd != IntPtr.Zero)
            {
                int w = Math.Max(1, Width);
                int h = Math.Max(1, Height);
                Win32.MoveWindow(_nativeHwnd, 0, 0, Math.Max(1, w - 1), Math.Max(1, h - 1), false);
                GLDebugLog.Log(LogName, "CreateNW/forced1", SnapshotSize());
                Win32.MoveWindow(_nativeHwnd, 0, 0, w, h, false);
                GLDebugLog.Log(LogName, "CreateNW/forced2", SnapshotSize());
            }
        }

        // 260529Cl 追加: GLFW の framebuffer_size_callback が発火した時のログ。
        // この値が現在の WinForms Width/Height と一致しているかを追跡することで、ARM 環境での同期不整合を可視化する。
        private void OnNativeFramebufferResize(OpenTK.Windowing.Common.FramebufferResizeEventArgs e)
        {
            GLDebugLog.Log(LogName, "FramebufferResize", $"event=({e.Width},{e.Height}) | {SnapshotSize()}");
        }

        // 260529Cl 追加: ログ用の識別名。GLControlAlpha (UserControl) が親で Name に "glControlReciProObjects" 等を持つので、
        // Parent.Name を優先することで FormRotation から振った名前を捕まえる。Parent がいなければ自身の Name にフォールバック。
        // 内部 glControl 自身の Name (= "glControl{N}") も message 側で残せるよう、こちらでは Parent.Name のみ返す。
        private string LogName => Parent?.Name ?? Name;

        // 260529Cl 追加: 各時点の WinForms / GLFW / HWND のサイズを 1 行で記録する診断ヘルパー。
        private string SnapshotSize()
        {
            if (!GLDebugLog.Enabled) return ""; // 260529Cl: 診断無効時は Win32 呼び出しを省く (通常起動のホットパス保護)
            string fb = "FB=null", ncSize = "", ncClient = "";
            if (_nativeWindow != null)
            {
                var fbs = _nativeWindow.FramebufferSize;
                fb = $"FB=({fbs.X},{fbs.Y})";
                var ns = _nativeWindow.Size;
                ncSize = $" NW.Size=({ns.X},{ns.Y})";
                var cs = _nativeWindow.ClientSize;
                ncClient = $" NW.CSize=({cs.X},{cs.Y})";
            }
            string rect = "rect=N/A";
            string pos = "pos=N/A";
            if (_nativeHwnd != IntPtr.Zero)
            {
                if (Win32.GetClientRect(_nativeHwnd, out var rc))
                    rect = $"rect=({rc.Width},{rc.Height})";
                // 260529Cl 追加: GLFW HWND の screen 上の絶対位置と、親 HWND との相対位置を記録
                if (Win32.GetWindowRect(_nativeHwnd, out var wr))
                {
                    var parentHwnd = Win32.GetParent(_nativeHwnd);
                    if (parentHwnd != IntPtr.Zero && Win32.GetWindowRect(parentHwnd, out var pr))
                    {
                        // 親 HWND の WindowRect は外枠なので、親の ClientRect 内オフセットを正確にはこれだけでは出せないが、
                        // child の Left/Top と parent の Left/Top の差が child の screen 内 vs parent screen 内位置差。
                        // child は SetParent で WS_CHILD になっているので、その screen 位置 = 親の screen 位置 + child の親内位置 (おおむね)
                        pos = $"absPos=({wr.Left},{wr.Top}) deltaFromParent=({wr.Left - pr.Left},{wr.Top - pr.Top})";
                    }
                    else
                    {
                        pos = $"absPos=({wr.Left},{wr.Top})";
                    }
                }
            }
            return $"[inner={Name}] WinForms=({Width},{Height}) {fb}{ncSize}{ncClient} HWND-{rect} {pos} DPI={DeviceDpi}";
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

        /// <summary>Ensure that the required underlying GLFW window has been created.</summary>
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
                _nativeHwnd = hWnd; // 260529Cl 追加: 後段 Win32 リサイズで使うため HWND を保持

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

        /// <summary>Enable/disable NativeInput for the given NativeWindow.</summary>
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

        /// <summary>Determine if native input is enabled for the given NativeWindow.</summary>
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
            for (Control? control = this; control != null; control = control.Parent) //260317Cl nullable化
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

        /// <summary>Destroy the child NativeWindow that wraps the underlying GLFW instance.</summary>
        private void DestroyNativeWindow()
        {
            if (_nativeWindow != null)
            {
                _nativeWindow.Dispose();
                _nativeWindow = null!;
            }
            _nativeHwnd = IntPtr.Zero; // 260529Cl 追加: HWND キャッシュも破棄
        }

        /// <summary>
        /// This private object is used as the reference for the 'Load' handler in
        /// the Events collection, and is only needed if you use the 'Load' event.
        /// </summary>
        private static readonly object EVENT_LOAD = new();

        /// <summary>An event hook, triggered when the control is created for the first time.</summary>
        [Category("Behavior")]
        [Description("Occurs when the GLControl is first created.")]
        public event EventHandler Load
        {
            add => Events.AddHandler(EVENT_LOAD, value);
            remove => Events.RemoveHandler(EVENT_LOAD, value);
        }

        /// <summary>Raises the CreateControl event.</summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            OnLoad(EventArgs.Empty);
        }

        /// <summary>The Load event is fired before the control becomes visible for the first time.</summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnLoad(EventArgs e)
        {
            // There is no good way to explain this event except to say
            // that it's just another name for OnControlCreated.
            ((EventHandler?)Events[EVENT_LOAD])?.Invoke(this, e); //260317Cl nullable化
        }

        /// <summary>This is raised by WinForms to paint this instance.</summary>
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
                GLDebugLog.Log(LogName, "OnResize/suppressed", $"WinForms=({Width},{Height})");
                return;
            }

            GLDebugLog.Log(LogName, "OnResize/enter", SnapshotSize());

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                BeginInvoke(new Action(ResizeNativeWindow)); // Need the native window to resize first otherwise our control will be in the wrong place.
            }
            else
            {
                ResizeNativeWindow();
            }

            GLDebugLog.Log(LogName, "OnResize/exit", SnapshotSize());
            base.OnResize(e);
        }

        /// <summary>Resize the native window to fit this control.</summary>
        private void ResizeNativeWindow()
        {
            if (IsDesignMode)
                return;

            if (_nativeWindow != null)
            {
                // 260529Cl 修正: Windows では GLFW の glfwSetWindowSize 経由 (_nativeWindow.ClientRectangle) を使うと、
                // PerMonitorV2 DPI 認識アプリ + Windows on ARM (x64 エミュ) の組合せで DPI 計算が食い違い、
                // HWND と back buffer が親 WinForms と異なる物理ピクセルサイズになる。
                // Win32.MoveWindow で物理ピクセル単位の指定にすると、WM_SIZE が GLFW の wndproc に同期で届き
                // _nativeWindow.FramebufferSize も同じ値に更新される。
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && _nativeHwnd != IntPtr.Zero)
                {
                    // 260529Cl 追加: Windows on ARM (x64 エミュ) では「片方の寸法だけ」が変化する WM_SIZE では
                    // OpenGL の back buffer が正しく再確保されない症状が確認された (例: FormMain の glControlAxes は
                    // Dock=Top で width のみ→height のみの 2 段 resize になり、片次元のみの WM_SIZE で driver が back buffer を
                    // 旧サイズのまま残し、Viewport だけ新サイズで描画されて「上にはみ出す」描画になる)。
                    // 必ず両次元が変化する中間サイズを経由する dual-MoveWindow にすることで、driver に確実に
                    // 両次元の再確保を要求する。同サイズ MoveWindow は Windows が WM_SIZE を抑制するため、わざと
                    // (w-1, h-1) を経由する。
                    int w = Math.Max(1, Width);
                    int h = Math.Max(1, Height);
                    Win32.MoveWindow(_nativeHwnd, 0, 0, Math.Max(1, w - 1), Math.Max(1, h - 1), false);
                    Win32.MoveWindow(_nativeHwnd, 0, 0, w, h, false);
                }
                else
                {
                    _nativeWindow.ClientRectangle = new Box2i(0, 0, Width, Height);
                }
            }
        }

        // 260529Cl 追加: GLFW が管理している実 framebuffer (OpenGL back buffer) のピクセルサイズ。
        // GLFW は wndproc で WM_SIZE を受信した時に framebuffer_size_callback を同期発火し、
        // この値を更新する。Win32.GetClientRect を直接呼ぶより、GLFW が認識している back buffer
        // サイズと完全一致するため、GL.Viewport のサイズ源として用いる方が安全。
        /// <summary>
        /// Pixel size of the actual GL back buffer as known to GLFW.
        /// Returns <see cref="System.Drawing.Size.Empty"/> when the native window has not been created.
        /// </summary>
        [Browsable(false)]
        public System.Drawing.Size FramebufferPixelSize
        {
            get
            {
                if (_nativeWindow == null)
                    return System.Drawing.Size.Empty;
                var sz = _nativeWindow.FramebufferSize;
                return new System.Drawing.Size(sz.X, sz.Y);
            }
        }

        // 260529Cl 追加 (診断用): GLFW に毎回 glfwGetFramebufferSize を問い合わせて取得する live 値。
        // FramebufferPixelSize は wndproc 同期で更新される cached 値 (_nativeWindow.FramebufferSize) を返すが、
        // Windows on ARM (x64 エミュ) で「cached 値は正しいが driver 内部の drawable/swapchain は旧サイズ」という
        // 不一致が疑われるため、cached と live の差を切り分けるための観測専用プロパティ。
        // ARM の GL 不具合再発時の診断用に温存 (RECIPRO_GLDIAG 診断ツールの一部)。詳細: ReciPro_WindowsOnARM_OpenGL調査.md
        /// <summary>GL back buffer size queried live from GLFW (glfwGetFramebufferSize); diagnostic counterpart of <see cref="FramebufferPixelSize"/>.</summary>
        [Browsable(false)]
        public System.Drawing.Size FramebufferPixelSizeLive
        {
            get
            {
                if (_nativeWindow == null)
                    return System.Drawing.Size.Empty;
                int w, h;
                unsafe { GLFW.GetFramebufferSize(_nativeWindow.WindowPtr, out w, out h); }
                return new System.Drawing.Size(w, h);
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

        /// <summary>Swaps the front and back buffers, presenting the rendered scene to the user.</summary>
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
