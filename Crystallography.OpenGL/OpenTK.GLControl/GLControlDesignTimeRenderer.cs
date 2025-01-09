using System;
using System.Windows.Forms;
using OpenTK.Mathematics;

namespace OpenTK.GLControl
{
    /// <summary>
    /// At design-time, we really can't load OpenGL and GLFW and render with it
    /// for real; the WinForms designer is too limited to support such advanced
    /// things without exploding.  So instead, we simply use GDI+ to draw a cube
    /// at design-time.
    /// </summary>
    internal class GLControlDesignTimeRenderer
    {
        /// <summary>
        /// The GLControl that needs to be rendered at design-time.
        /// </summary>
        private readonly GLControl _owner;

        /// <summary>
        /// The angle (yaw) of the design-time cube.
        /// </summary>
        private float _designTimeCubeYaw;

        /// <summary>
        /// The angle (pitch) of the design-time cube.
        /// </summary>
        private float _designTimeCubeRoll;

        /// <summary>
        /// Endpoints that can make a cube.  We only use this in design mode.
        /// </summary>
        private static (Vector3, Vector3)[] CubeLines { get; } = new (Vector3, Vector3)[]
        {
            (new Vector3(-1, -1, -1), new Vector3(+1, -1, -1)),
            (new Vector3(-1, -1, -1), new Vector3(-1, +1, -1)),
            (new Vector3(-1, -1, -1), new Vector3(-1, -1, +1)),

            (new Vector3(+1, -1, -1), new Vector3(+1, +1, -1)),
            (new Vector3(+1, -1, -1), new Vector3(+1, -1, +1)),

            (new Vector3(-1, +1, -1), new Vector3(+1, +1, -1)),
            (new Vector3(-1, +1, -1), new Vector3(-1, +1, +1)),

            (new Vector3(-1, -1, +1), new Vector3(+1, -1, +1)),
            (new Vector3(-1, -1, +1), new Vector3(-1, +1, +1)),

            (new Vector3(+1, +1, +1), new Vector3(+1, +1, -1)),
            (new Vector3(+1, +1, +1), new Vector3(-1, +1, +1)),
            (new Vector3(+1, +1, +1), new Vector3(+1, -1, +1)),
        };

        /// <summary>
        /// Instantiate a new design-timer renderer for the given GLControl.
        /// </summary>
        /// <param name="owner">The GLControl that needs to be rendered at
        /// design-time.</param>
        public GLControlDesignTimeRenderer(GLControl owner)
        {
            _owner = owner;


            _designTimeCubeYaw += -10 * (float)(Math.PI / 97);
        }

        /// <summary>
        /// Draw a simple cube, in an ortho projection, using GDI+.
        /// </summary>
        /// <param name="graphics">The GDI+ Graphics object to draw on.</param>
        /// <param name="color">The color for the cube.</param>
        /// <param name="cx">The X coordinate of the center point of the cube,
        /// in Graphics coordinates.</param>
        /// <param name="cy">The Y coordinate of the center point of the cube,
        /// in Graphics coordinates.</param>
        /// <param name="radius">The radius to the cube's corners from the center point.</param>
        /// <param name="yaw">The yaw (rotation around the Y axis) of the cube.</param>
        /// <param name="pitch">The pitch (rotation around the X axis) of the cube.</param>
        /// <param name="roll">The roll (rotation around the Z axis) of the cube.</param>
        private void DrawCube(System.Drawing.Graphics graphics,
            System.Drawing.Color color,
            float cx, float cy, float radius,
            float yaw = 0, float pitch = 0, float roll = 0)
        {
            // We use matrices to rotate and scale the cube, but we just use a simple
            // center offset to position it.  That saves a lot of extra multiplies all
            // over this code, since we can use Matrix3 and Vector3 instead of Matrix4
            // and Vector4.  And no, quaternions aren't worth the effort here either.
            Matrix3 matrix = Matrix3.CreateRotationZ(roll)
                * Matrix3.CreateRotationY(yaw)
                * Matrix3.CreateRotationX(pitch)
                * Matrix3.CreateScale(radius);

            // Draw the edges of the cube in the given color.  Since it's just a single-
            // color wireframe, the order of the edges doesn't matter at all.
            using System.Drawing.Brush brush = new System.Drawing.SolidBrush(color);
            using System.Drawing.Pen pen = new System.Drawing.Pen(brush);

            foreach ((Vector3 start, Vector3 end) in CubeLines)
            {
                Vector3 projStart = start * matrix;
                Vector3 projEnd = end * matrix;

                graphics.DrawLine(pen, cx + projStart.X, cy - projStart.Y, cx + projEnd.X, cy - projEnd.Y);
            }
        }

        /// <summary>
        /// In design mode, we have nothing to show, so we paint the
        /// background black and put a cube on it so that it's
        /// obvious that it's a 3D renderer.
        /// </summary>
        public void Paint(System.Drawing.Graphics graphics)
        {
            // Since we're always DoubleBuffered = false, we have to do
            // simple double-buffering ourselves, using a bitmap.
            using System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(_owner.Width, _owner.Height, graphics);
            using System.Drawing.Graphics bitmapGraphics = System.Drawing.Graphics.FromImage(bitmap);

            // Other resources we'll need.
            using System.Drawing.Font bigFont = new System.Drawing.Font("Arial", 12);
            using System.Drawing.Font smallFont = new System.Drawing.Font("Arial", 9);
            using System.Drawing.Brush titleBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            using System.Drawing.Brush subtitleBrush = new System.Drawing.SolidBrush(System.Drawing.Color.PaleGoldenrod);

            // Configuration.
            const float cubeRadius = 16;
            const string title = "GLControl";
            int cx = _owner.Width / 2, cy = _owner.Height / 2;
            string subtitle = $"( {_owner.Name} )";

            // These sizes will hold font metrics.
            System.Drawing.SizeF titleSize;
            System.Drawing.SizeF subtitleSize;
            System.Drawing.SizeF totalTextSize;

            // Start with a black background.
            bitmapGraphics.Clear(System.Drawing.Color.Black);
            bitmapGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Measure and the title (fixed) and the subtitle (the control's Name).
            titleSize = bitmapGraphics.MeasureString(title, bigFont);
            subtitleSize = bitmapGraphics.MeasureString(subtitle, smallFont);
            totalTextSize = new System.Drawing.SizeF(
                Math.Max(titleSize.Width, subtitleSize.Width),
                titleSize.Height + subtitleSize.Height
            );

            // Draw both of the title and subtitle centered, now that we know how big they are.
            bitmapGraphics.DrawString(title, bigFont, titleBrush,
                new System.Drawing.PointF(cx - totalTextSize.Width / 2 + cubeRadius + 2, cy - totalTextSize.Height / 2));
            bitmapGraphics.DrawString(subtitle, smallFont, subtitleBrush,
                new System.Drawing.PointF(cx - totalTextSize.Width / 2 + cubeRadius + 2, cy - totalTextSize.Height / 2 + titleSize.Height));

            // Draw a cube beside the title and subtitle.
            DrawCube(bitmapGraphics, System.Drawing.Color.Red,
                cx - totalTextSize.Width / 2 - cubeRadius - 2, cy, cubeRadius,
                _designTimeCubeYaw, (float)(Math.PI / 8), _designTimeCubeRoll);

            // Draw the resulting bitmap on the real window canvas.
            graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
