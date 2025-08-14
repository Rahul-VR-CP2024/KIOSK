using System.Windows;
using System.Windows.Threading;

namespace Exchange
{
    public class InactivityMonitor
    {
        private readonly DispatcherTimer _inactivityTimer;
        private readonly TimeSpan _inactivityThreshold;
        private readonly Action _onInactivityDetected;

        public InactivityMonitor(TimeSpan inactivityThreshold, Action onInactivityDetected)
        {
            _inactivityThreshold = inactivityThreshold;
            _onInactivityDetected = onInactivityDetected;

            // Initialize the timer with the inactivity threshold
            _inactivityTimer = new DispatcherTimer
            {
                Interval = _inactivityThreshold
            };
            _inactivityTimer.Tick += InactivityTimer_Tick;
        }

        public void StartMonitoring()
        {
            // Start monitoring activity
            ResetTimer();
            AttachEventHandlers();
        }

        public void StopMonitoring()
        {
            // Stop monitoring activity
            _inactivityTimer.Stop();
            DetachEventHandlers();
        }

        private void ResetTimer()
        {
            //MessageBox.Show("ResetTimer");
            // Reset the timer whenever there is activity
            _inactivityTimer.Stop();
            _inactivityTimer.Start();
        }

        private void InactivityTimer_Tick(object sender, EventArgs e)
        {
            // Inactivity threshold reached, invoke the action
            _inactivityTimer.Stop();
            _onInactivityDetected?.Invoke();
        }

        private void AttachEventHandlers()
        {
            // Attach event handlers for user input
            Application.Current.MainWindow.PreviewMouseMove += OnActivityDetected;
            Application.Current.MainWindow.PreviewKeyDown += OnActivityDetected;
        }

        private void DetachEventHandlers()
        {
            // Detach event handlers when stopping monitoring
            Application.Current.MainWindow.PreviewMouseMove -= OnActivityDetected;
            Application.Current.MainWindow.PreviewKeyDown -= OnActivityDetected;
        }

        private void OnActivityDetected(object sender, EventArgs e)
        {
            //MessageBox.Show("OnActivityDetected");
            // Reset timer on any user input
            ResetTimer();
        }
    }
}
