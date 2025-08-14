using System.Windows.Threading;

namespace Exchange.Common
{
    public class DisposableTimer : IDisposable
    {
        private DispatcherTimer _timer;
        private readonly Action _onTick;
        private DisposableTimer _selfRef;
        private readonly object _lock = new object();

        public DisposableTimer(Action onTick, int delayInSec)
        {
            _selfRef = this;
            _onTick = onTick;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(delayInSec) };
            _timer.Tick += OnTick;
            _timer.Start();
        }

        private void OnTick(object sender, EventArgs e)
        {
            lock (_lock)
            {
                _onTick?.Invoke();
                Cleanup();
            }
        }

        public void Cancel() => Cleanup();

        private void Cleanup()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Tick -= OnTick;
                _timer = null;
            }
            _selfRef = null;
        }

        public void Dispose() => Cleanup();
    }
}
