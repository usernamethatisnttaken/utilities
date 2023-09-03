namespace progressBar {
    /// <summary>
    /// Provides a loading bar to play around with.
    /// </summary>
    public class ProgressBar {
        public int consoleAccessMs;
        private int capacity;
        private int units;
        private int progress;
        private BarType barType;

        private bool dispTime;
        private bool dispProgress;
        private readonly int slidePauseFrames = 8;

        private string[] caps = new string[2];

        /// <param name="capacity">The max progress of the bar</param>
        /// <param name="units">How many progress is equal to one bar segment (-1 = default/25)</param>
        /// <param name="barType">The type of bar to display</param>
        /// <param name="dispTime">Display the time elapsed on the slides?</param>
        /// <param name="dispProgress">Display the progress on the slides?</param>
        /// <param name="startCap">String to display before the loading bar</param>
        /// <param name="endCap">String to display after the loading bar</param>
        public ProgressBar(int capacity, int units = -1, BarType barType = BarType.BAR, bool dispTime = false, bool dispProgress = false, string startCap = "", string endCap = "") {
            this.capacity = capacity;
            this.units = units > 0 ? units : capacity / 25;
            this.barType = barType;
            this.dispTime = dispTime;
            this.dispProgress = dispProgress;
            
            consoleAccessMs = 125;
            progress = 0;
            caps[0] = startCap; caps[1] = endCap;
        }

        /// <summary>
        /// Starts the progress bar
        /// </summary>
        public void Start() {
            Action action = Bar;
            // switch(barType) {} // Suppresses an error
            Thread bar = new(new ThreadStart(action));
            bar.Start();
        }

        /// <summary>
        /// Standard bar progress bar
        /// </summary>
        private void Bar() {
            string push;
            int accessTick = 0;
            int slideIndex = 0;
            DateTime start = DateTime.Now;

            while(progress < capacity) {
                push = "";
                for(int i = 0; i < capacity; i += units) {
                    push += i <= progress ? '█' : ' ';
                }
                Console.Write("\r{0}", caps[0] + string.Join("", push) + caps[1] + GetSuffix(slideIndex, start, accessTick));
                accessTick++;
                Thread.Sleep(consoleAccessMs);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Returns the suffix/slide to the loading bar (basically all of the extra info)
        /// </summary>
        /// <param name="slideIndex">The currently displayed slide</param>
        /// <param name="start">The start time of the process</param>
        /// <param name="accessTick">How many times the progress bar has updated</param>
        /// <returns></returns>
        private string GetSuffix(int slideIndex, DateTime start, int accessTick) {
            string result = "    ";
            string[] slides = new string[]{"", ""};

            if(dispTime) slides[0] = string.Format("{0:0.00}", (DateTime.Now - start).TotalSeconds) + "s elapsed";
            if(dispProgress) slides[1] = progress + "/" + capacity + " done";
            int size = Math.Max(slides[0].Length, slides[1].Length);
            if(accessTick >= size + slidePauseFrames) {
                slideIndex++;
                accessTick %= size + slidePauseFrames;
            }

            for(int i = 0; i < size; i++) {
                try {
                    if(i < accessTick) {result += slides[slideIndex % 2][i];} else
                    if(i > accessTick) {result += slides[(slideIndex + 1) % 2][i];}
                    else result += "█";
                } catch(IndexOutOfRangeException) {
                    result += " ";
                }
            }

            return result;
        }

        public void Increment(int amount = 1) {
            progress += amount;
        }

        public void SetSize(int? capacity = default, int? units = default) {
            if(capacity != null) this.capacity = (int)capacity;
            if(units != null) this.units = (int)units;
        }

        public void SetFps(int fps) {
            consoleAccessMs = 1000 / fps;
        }

        public void SetBarType(BarType barType) {
            this.barType = barType;
        }

        public void DisplayTime(bool value = true) {
            dispTime = value;
        }

        public void DisplayProgress(bool value = true) {
            dispProgress = value;
        }

        public void SetBarCaps(string startCap, string endCap) {
            caps[0] = startCap;
            caps[1] = endCap;
        }

        public static ProgressBar operator ++(ProgressBar current) {
            current.Increment(1);
            return current;
        }
    }
}