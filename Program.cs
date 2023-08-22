using progressBar;

ProgressBar bar = new(100, dispTime:true, dispProgress:true);
bar.Start();
for(int i = 0; i < 100; i++) {
    bar++;
    Thread.Sleep(50);
}