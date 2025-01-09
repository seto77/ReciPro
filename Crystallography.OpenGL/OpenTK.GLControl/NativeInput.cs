using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;
using NativeWindow = OpenTK.Windowing.Desktop.NativeWindow;

namespace OpenTK.GLControl
{
    /// <summary>
    /// This proxy class provides access to the native input methods and properties
    /// exposed by OpenTK, where those methods and properties are safe to invoke.
    /// In general, you should prefer to use WinForms's keyboard/mouse input, but
    /// if you need access to "raw" device input within a GLControl, this class
    /// provides that access.
    ///
    /// Instances of this class are only instantiated if they are required; we
    /// don't make one of these if we don't need it.
    /// </summary>
    internal class NativeInput : INativeInput
    {
        #region Private/internal data

        /// <summary>
        /// Access to the underlying NativeWindow.
        /// </summary>
        private readonly NativeWindow _nativeWindow;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the position of the mouse relative to the content area of this window.
        /// </summary>
        public Vector2 MousePosition
        {
            get => _nativeWindow.MousePosition;
            set => _nativeWindow.MousePosition = value;
        }

        /// <summary>
        /// Gets the current state of the keyboard as of the last time the window processed
        /// events.
        /// </summary>
        public KeyboardState KeyboardState
            => _nativeWindow.KeyboardState;

        /// <summary>
        /// Gets the current state of the joysticks as of the last time the window processed
        /// events.
        /// </summary>
        public IReadOnlyList<JoystickState> JoystickStates
            => _nativeWindow.JoystickStates;

        /// <summary>
        /// Gets the current state of the mouse as of the last time the window processed
        /// events.
        /// </summary>
        public MouseState MouseState
            => _nativeWindow.MouseState;

        /// <summary>
        /// Gets a value indicating whether any key is down.
        /// </summary>
        public bool IsAnyKeyDown
            => _nativeWindow.IsAnyKeyDown;

        /// <summary>
        /// Gets a value indicating whether any mouse button is pressed.
        /// </summary>
        public bool IsAnyMouseButtonDown
            => _nativeWindow.IsAnyMouseButtonDown;

        #endregion

        #region Public events

        /// <summary>
        /// Occurs whenever the mouse cursor is moved
        /// </summary>
        public event Action<MouseMoveEventArgs> MouseMove
        {
            add => _nativeWindow.MouseMove += value;
            remove => _nativeWindow.MouseMove -= value;
        }

        /// <summary>
        /// Occurs whenever a OpenTK.Windowing.GraphicsLibraryFramework.MouseButton is released.
        /// </summary>
        public event Action<MouseButtonEventArgs> MouseUp
        {
            add => _nativeWindow.MouseUp += value;
            remove => _nativeWindow.MouseUp -= value;
        }

        /// <summary>
        /// Occurs whenever a OpenTK.Windowing.GraphicsLibraryFramework.MouseButton is clicked.
        /// </summary>
        public event Action<MouseButtonEventArgs> MouseDown
        {
            add => _nativeWindow.MouseDown += value;
            remove => _nativeWindow.MouseDown -= value;
        }

        /// <summary>
        /// Occurs whenever the mouse cursor enters the window OpenTK.Windowing.Desktop.NativeWindow.Bounds.
        /// </summary>
        public event Action MouseEnter
        {
            add => _nativeWindow.MouseEnter += value;
            remove => _nativeWindow.MouseEnter -= value;
        }

        /// <summary>
        /// Occurs whenever the mouse cursor leaves the window OpenTK.Windowing.Desktop.NativeWindow.Bounds.
        /// </summary>
        public event Action MouseLeave
        {
            add => _nativeWindow.MouseLeave += value;
            remove => _nativeWindow.MouseLeave -= value;
        }

        /// <summary>
        /// Occurs whenever a keyboard key is released.
        /// </summary>
        public event Action<KeyboardKeyEventArgs> KeyUp
        {
            add => _nativeWindow.KeyUp += value;
            remove => _nativeWindow.KeyUp -= value;
        }

        /// <summary>
        /// Occurs whenever a Unicode code point is typed.
        /// </summary>
        public event Action<TextInputEventArgs> TextInput
        {
            add => _nativeWindow.TextInput += value;
            remove => _nativeWindow.TextInput -= value;
        }

        /// <summary>
        /// Occurs when a joystick is connected or disconnected.
        /// </summary>
        public event Action<JoystickEventArgs> JoystickConnected
        {
            add => _nativeWindow.JoystickConnected += value;
            remove => _nativeWindow.JoystickConnected -= value;
        }

        /// <summary>
        /// Occurs whenever a keyboard key is pressed.
        /// </summary>
        public event Action<KeyboardKeyEventArgs> KeyDown
        {
            add => _nativeWindow.KeyDown += value;
            remove => _nativeWindow.KeyDown -= value;
        }

        /// <summary>
        /// Occurs whenever one or more files are dropped on the window.
        /// </summary>
        public event Action<FileDropEventArgs> FileDrop
        {
            add => _nativeWindow.FileDrop += value;
            remove => _nativeWindow.FileDrop -= value;
        }

        /// <summary>
        /// Occurs whenever a mouse wheel is moved.
        /// </summary>
        public event Action<MouseWheelEventArgs> MouseWheel
        {
            add => _nativeWindow.MouseWheel += value;
            remove => _nativeWindow.MouseWheel -= value;
        }

        #endregion

        #region Construction

        /// <summary>
        /// Construct a new instance of a NativeInput proxy.
        /// </summary>
        /// <param name="nativeWindow">The NativeWindow that this NativeInput is wrapping.</param>
        internal NativeInput(NativeWindow nativeWindow)
        {
            _nativeWindow = nativeWindow;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets a <see cref="bool" /> indicating whether this key is currently down.
        /// </summary>
        /// <param name="key">The <see cref="Keys">key</see> to check.</param>
        /// <returns><c>true</c> if <paramref name="key"/> is in the down state; otherwise, <c>false</c>.</returns>
        public bool IsKeyDown(Keys key)
            => _nativeWindow.IsKeyDown(key);

        /// <summary>
        /// Gets whether the specified key is pressed in the current frame but released in the previous frame.
        /// </summary>
        /// <remarks>
        /// "Frame" refers to invocations of <see cref="NativeWindow.ProcessEvents()"/> here.
        /// </remarks>
        /// <param name="key">The <see cref="Keys">key</see> to check.</param>
        /// <returns>True if the key is pressed in this frame, but not the last frame.</returns>
        public bool IsKeyPressed(Keys key)
            => _nativeWindow.IsKeyPressed(key);

        /// <summary>
        /// Gets whether the specified key is released in the current frame but pressed in the previous frame.
        /// </summary>
        /// <remarks>
        /// "Frame" refers to invocations of <see cref="NativeWindow.ProcessEvents()"/> here.
        /// </remarks>
        /// <param name="key">The <see cref="Keys">key</see> to check.</param>
        /// <returns>True if the key is released in this frame, but pressed the last frame.</returns>
        public bool IsKeyReleased(Windowing.GraphicsLibraryFramework.Keys key)
            => _nativeWindow.IsKeyReleased(key);

        /// <summary>
        /// Gets a <see cref="bool" /> indicating whether this button is currently down.
        /// </summary>
        /// <param name="button">The <see cref="MouseButton" /> to check.</param>
        /// <returns><c>true</c> if <paramref name="button"/> is in the down state; otherwise, <c>false</c>.</returns>
        public bool IsMouseButtonDown(MouseButton button)
            => _nativeWindow.IsMouseButtonDown(button);


        /// <summary>
        /// Gets whether the specified mouse button is pressed in the current frame but released in the previous frame.
        /// </summary>
        /// <remarks>
        /// "Frame" refers to invocations of <see cref="NativeWindow.ProcessEvents()"/> here.
        /// </remarks>
        /// <param name="button">The button to check.</param>
        /// <returns>True if the button is pressed in this frame, but not the last frame.</returns>
        public bool IsMouseButtonPressed(MouseButton button)
            => _nativeWindow.IsMouseButtonPressed(button);

        /// <summary>
        /// Gets whether the specified mouse button is released in the current frame but pressed in the previous frame.
        /// </summary>
        /// <remarks>
        /// "Frame" refers to invocations of <see cref="NativeWindow.ProcessEvents()"/> here.
        /// </remarks>
        /// <param name="button">The button to check.</param>
        /// <returns>True if the button is released in this frame, but pressed the last frame.</returns>
        public bool IsMouseButtonReleased(MouseButton button)
            => _nativeWindow.IsMouseButtonReleased(button);

        #endregion
    }
}
