namespace glApp {
    /// <summary>
    /// A draw module tester
    /// </summary>
    public class TestDraw : ModuleDraw {
        public TestDraw(Window controller, KeplerCell data) : base(controller, data) {}

        public override void Start() {
            Update();
        }

        public override void Update() {
            long time = controller.GetTime();
            DrawCircle(new Vertex(0, 0, 0, 0, 1, 0), 0.5f);
        }
    }
}