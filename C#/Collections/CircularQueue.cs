namespace collections {
    /// <summary>
    /// Sort of like a queue, but is circular 
    /// </summary>
    public class CircularQueue<T> : Queue<T> {
        public CircularQueue() : base() {}

        /// <summary>
        /// Returns the object at the beginning of the Queue along with moving it to the end.
        /// </summary>
        /// <returns>The object that is removed from the beginning of the Queue.</returns>
        public T Cycle() {
            T value = Dequeue();
            Enqueue(value);
            return value;
        }
    }
}