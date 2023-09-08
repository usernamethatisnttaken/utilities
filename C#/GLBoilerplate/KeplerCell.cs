namespace glApp {
    /// <summary>
    /// A data structure to aid in pushing vertices and indices to the GL side of things.
    /// Called "Kepler Cell" because it originally quickly filled up with excess data (ie. Kepler Syndrome)
    /// </summary>
    public class KeplerCell {
        private LinkedList<float> verticesBottom;
        private LinkedList<uint> indicesBottom;
        private uint workingIndex;

        public KeplerCell() {
            verticesBottom = new();
            indicesBottom  = new();
            workingIndex = 0;
        }

        /// <summary>
        /// Push a triangle's vertices to the bottom of the structure
        /// </summary>
        public void Push(Vertex v0, Vertex v1, Vertex v2) {
            VertexPush(v0);
            VertexPush(v1);
            VertexPush(v2);
        }

        /// <summary>
        /// Push a single vertex to the bottom of the structure
        /// </summary>
        private void VertexPush(Vertex vertex) {
            verticesBottom.AddLast(vertex.x);
            verticesBottom.AddLast(vertex.y);
            verticesBottom.AddLast(vertex.z);
            verticesBottom.AddLast(vertex.r);
            verticesBottom.AddLast(vertex.g);
            verticesBottom.AddLast(vertex.b);
            indicesBottom.AddLast(workingIndex++);
        }

        /// <summary>
        /// Get the vertex data stored in the structure
        /// </summary>
        /// <returns>An array representing the data</returns>
        public float[] PullVertices() {
           return verticesBottom.ToArray();
        }

        /// <summary>
        /// Get the index data stored in the structure
        /// </summary>
        /// <returns>An array representing the data</returns>
        public uint[] PullIndices() {
            return indicesBottom.ToArray();
        }

        /// <summary>
        /// Clear the currently stored data
        /// </summary>
        public void Wipe() {
            verticesBottom = new();
            indicesBottom  = new();
            workingIndex   = 0;
        }
    }
}