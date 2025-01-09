using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace OpenTK.GLControl
{
    /// <summary>
    /// Abstract access to native-input properties, methods, and events.
    /// </summary>
    public interface INativeInput
    {
        /// <summary>
        /// Gets or sets the position of the mouse relative to the content area of this window.
        /// </summary>
        Vector2 MousePosition { get; }

        /// <summary>
        /// Gets the current state of the keyboard as of the last time the window processed
        /// events.
        /// </summary>
        KeyboardState KeyboardState { get; }

        /// <summary>
        /// Gets the current state of the joysticks as of the last time the window processed
        /// events.
        /// </summary>
        IReadOnlyList<JoystickState> JoystickStates { get; }

        /// <summary>
        /// Gets the current state of the mouse as of the last time the window processed
        /// events.
        /// </summary>
        MouseState MouseState { get; }

        /// <summary>
        /// Gets a value indicating whether any key is down.
        /// </summary>
        bool IsAnyKeyDown { get; }

        /// <summary>
        /// Gets a value indicating whether any mouse button is pressed.
        /// </summary>
        bool IsAnyMouseButtonDown { get; }

        /// <summary>
        /// Occurs whenever the mouse cursor is moved
        /// </summary>
        event Action<MouseMoveEventArgs> MouseMove;

        /// <summary>
        /// Occurs whenever a OpenTK.Windowing.GraphicsLibraryFramework.MouseButton is released.
        /// </summary>
        event Action<MouseButtonEventArgs> MouseUp;

        /// <summary>
        /// Occurs whenever a OpenTK.Windowing.GraphicsLibraryFramework.MouseButton is clicked.
        /// </summary>
        event Action<MouseButtonEventArgs> MouseDown;

        /// <summary>
        /// Occurs whenever the mouse cursor enters the window OpenTK.Windowing.Desktop.NativeWindow.Bounds.
        /// </summary>
        event Action MouseEnter;

        /// <summary>
        /// Occurs whenever the mouse cursor leaves the window OpenTK.Windowing.Desktop.NativeWindow.Bounds.
        /// </summary>
        event Action MouseLeave;

        /// <summary>
        /// Occurs whenever a keyboard key is released.
        /// </summary>
        event Action<KeyboardKeyEventArgs> KeyUp;

        /// <summary>
        /// Occurs whenever a Unicode code point is typed.
        /// </summary>
        event Action<TextInputEventArgs> TextInput;

        /// <summary>
        /// Occurs when a joystick is connected or disconnected.
        /// </summary>
        event Action<JoystickEventArgs> JoystickConnected;

        /// <summary>
        /// Occurs whenever a keyboard key is pressed.
        /// </summary>
        event Action<KeyboardKeyEventArgs> KeyDown;

        /// <summary>
        /// Occurs whenever one or more files are dropped on the window.
        /// </summary>
        event Action<FileDropEventArgs> FileDrop;

        /// <summary>
        /// Occurs whenever a mouse wheel is moved.
        /// </summary>
        event Action<MouseWheelEventArgs> MouseWheel;

        /// <summary>
        /// Gets a <see cref="bool" /> indicating whether this key is currently down.
        /// </summary>
        /// <param name="key">The <see cref="Keys">key</see> to check.</param>
        /// <returns><c>true</c> if <paramref name="key"/> is in the down state; otherwise, <c>false</c>.</returns>
        bool IsKeyDown(Keys key);

        /// <summary>
        /// Gets whether the specified key is pressed in the current frame but released in the previous frame.
        /// </summary>
        /// <remarks>
        /// "Frame" refers to invocations of <see cref="NativeWindow.ProcessEvents()"/> here.
        /// </remarks>
        /// <param name="key">The <see cref="Keys">key</see> to check.</param>
        /// <returns>True if the key is pressed in this frame, but not the last frame.</returns>
        bool IsKeyPressed(Keys key);

        /// <summary>
        /// Gets whether the specified key is released in the current frame but pressed in the previous frame.
        /// </summary>
        /// <remarks>
        /// "Frame" refers to invocations of <see cref="NativeWindow.ProcessEvents()"/> here.
        /// </remarks>
        /// <param name="key">The <see cref="Keys">key</see> to check.</param>
        /// <returns>True if the key is released in this frame, but pressed the last frame.</returns>
        bool IsKeyReleased(Keys key);

        /// <summary>
        /// Gets a <see cref="bool" /> indicating whether this button is currently down.
        /// </summary>
        /// <param name="button">The <see cref="MouseButton" /> to check.</param>
        /// <returns><c>true</c> if <paramref name="button"/> is in the down state; otherwise, <c>false</c>.</returns>
        bool IsMouseButtonDown(MouseButton button);

        /// <summary>
        /// Gets whether the specified mouse button is pressed in the current frame but released in the previous frame.
        /// </summary>
        /// <remarks>
        /// "Frame" refers to invocations of <see cref="NativeWindow.ProcessEvents()"/> here.
        /// </remarks>
        /// <param name="button">The button to check.</param>
        /// <returns>True if the button is pressed in this frame, but not the last frame.</returns>
        bool IsMouseButtonPressed(MouseButton button);

        /// <summary>
        /// Gets whether the specified mouse button is released in the current frame but pressed in the previous frame.
        /// </summary>
        /// <remarks>
        /// "Frame" refers to invocations of <see cref="NativeWindow.ProcessEvents()"/> here.
        /// </remarks>
        /// <param name="button">The button to check.</param>
        /// <returns>True if the button is released in this frame, but pressed the last frame.</returns>
        bool IsMouseButtonReleased(MouseButton button);
    }
}
