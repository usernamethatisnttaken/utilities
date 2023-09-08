namespace glApp {
    /// <summary>
    /// Represents a single vertex
    /// </summary>
    public class Vertex {
        public float x, y, z, r, g, b;

        /// <param name="x">x position of the vertex</param>
        /// <param name="y">y position of the vertex</param>
        /// <param name="z">z position of the vertex</param>
        /// <param name="r">red channel of the vertex</param>
        /// <param name="g">green channel of the vertex</param>
        /// <param name="b">blue channel of the vertex</param>
        public Vertex(float x, float y, float z = 0, float r = 0, float g = 0, float b = 0) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }
}