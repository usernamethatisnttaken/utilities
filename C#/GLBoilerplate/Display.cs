using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace glApp {
    /// <summary>
    /// The GL side of the miniapp
    /// </summary>
    public class Display : GameWindow {
        private readonly Window controller;
        private Shader shader;
        private string pathToHere;

        private int vertexArray;
        private int vertexBuffer;
        private int indexBuffer;

        public float[] vertices;
        public uint[] indices;

        /// <param name="controller">The window controlling the GL-side display</param>
        /// <param name="width">Width of the window</param>
        /// <param name="height">Height of the window</param>
        /// <param name="title">Title for the miniapp</param>
        /// <param name="pathToHere">Path to this file</param>
        #pragma warning disable CS8618
        public Display(Window controller, int width, int height, string title, string pathToHere) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) {
            this.controller = controller;
            vertices = Array.Empty<float>();
            indices = Array.Empty<uint>();
        }
        #pragma warning restore CS8618

        /// <inheritdoc/>
        protected override void OnLoad() {
            base.OnLoad();
            GL.ClearColor(1f, 1f, 1f, 1f);
            shader = new(pathToHere + "/shaders/shader.vert", pathToHere + "/shaders/shader.frag");

            //Do vertex array
            vertexArray = GL.GenVertexArray();
            GL.BindVertexArray(vertexArray);

            //Do buffers
            vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            indexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);
            DoBuffers();

            //Assign vertices
            int vertexLocation = shader.GetAttribLocation("position");
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(vertexLocation);

            //Assign colors
            int colorLocation = shader.GetAttribLocation("color");
            GL.VertexAttribPointer(colorLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(colorLocation);
        }

        /// <summary>
        /// (Re-)Assigns the buffers
        /// </summary>
        private void DoBuffers() {
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.DynamicDraw);
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args) {
            base.OnUpdateFrame(args);
            if(KeyboardState.IsKeyDown(Keys.Escape)) Close();
            controller.OnUpdateFrame();
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args) {
            controller.OnRenderFrame();
            DoBuffers();
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            shader.Use();

            //Bind and draw
            GL.BindVertexArray(vertexArray);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        /// <inheritdoc/>
        protected override void OnResize(ResizeEventArgs e) {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        /// <inheritdoc/>
        protected override void OnUnload() {
            base.OnUnload();
            shader?.Dispose();
        }
    }
}