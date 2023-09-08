namespace glApp {
    /// <summary>
    /// An semi-abstract template for drawing modules (ie. something that is putting something to the screen)
    /// </summary>
    public abstract class ModuleDraw {
        protected readonly Window controller;
        public readonly KeplerCell data;

        /// <param name="controller">Reference to the window controller</param>
        /// <param name="data">Reference to the windows draw data</param>
        public ModuleDraw(Window controller, KeplerCell data) {
            this.controller = controller;
            this.data = data;
        }

        public abstract void Start();
        public abstract void Update();

        /// <summary>
        /// Draws a line between the given vertices
        /// </summary>
        public void DrawLine(Vertex v0, Vertex v1) {
            data.Push(v0, v1, v1);
        }

        /// <summary>
        /// Draws a triangle between the given vertices
        /// </summary>
        public void DrawTriangle(Vertex v0, Vertex v1, Vertex v2) {
            data.Push(v0, v1, v2);
        }

        /// <summary>
        /// Draws a polygon between the given vertices
        /// </summary>
        public void DrawPolygon(params Vertex[] vertices) {
            for(int i = 1; i < vertices.Length - 1; i++) {
                data.Push(vertices[0], vertices[i], vertices[i + 1]);
            }
        }

        /// <summary>
        /// Draws a regular n-gon of n = depth and size = radius, centered at the given vertex
        /// </summary>
        public void DrawCircle(Vertex center, float radius, int depth = 25) {
            for(int i = 0; i < depth; i++) {
                Vertex c_a = new((float)(radius * Math.Cos(2 * Math.PI / depth * i)), (float)(radius * Math.Sin(2 * Math.PI / depth * i)), 0, center.r, center.g, center.b);
                Vertex c_b = new((float)(radius * Math.Cos(2 * Math.PI / depth * (i + 1))), (float)(radius * Math.Sin(2 * Math.PI / depth * (i + 1))), 0, center.r, center.g, center.b);
                data.Push(center, c_a, c_b);
            }
        }
    }
}