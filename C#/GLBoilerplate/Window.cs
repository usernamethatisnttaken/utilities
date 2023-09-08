using System.Diagnostics;

namespace glApp {
    /// <summary>
    /// Main controller for the miniapp window
    /// </summary>
    public class Window {
        private readonly Display display;
        private readonly KeplerCell drawData;
        private readonly ModuleDraw draw;
        private readonly Stopwatch timer;

        /// <param name="title">Miniapp title</param>
        /// <param name="pathToHere">Path to this file</param>
        /// <param name="width">Width of the window</param>
        /// <param name="height">Height of the window</param>
        public Window(string title, string pathToHere, int width = 2000, int height = 2000) {
            display = new(this, width, height, title, pathToHere);
            drawData = new();
            draw = new TestDraw(this, drawData);
            timer = new();
        }

        /// <summary>
        /// Starts the miniapp
        /// </summary>
        public void Run() {
            timer.Start();
            draw.Start();
            display.Run();
        }

        /// <summary>
        /// Triggers on update frames (Should not be accesed)
        /// </summary>
        public void OnUpdateFrame() {
            drawData.Wipe();
            draw.Update();
        }

        /// <summary>
        /// Triggers on render frames (Should not be accesed)
        /// </summary>
        public void OnRenderFrame() {
            display.vertices = drawData.PullVertices();
            display.indices  = drawData.PullIndices();
        }

        /// <summary>
        /// Switches draw modules (used for menuing, etc.)
        /// </summary>
        public void PassDrawModule() {
            throw new NotImplementedException();
        }


        /// <returns>Milliseconds elapsed</returns>
        public long GetTime() {
            return timer.ElapsedMilliseconds;
        }
    }
}