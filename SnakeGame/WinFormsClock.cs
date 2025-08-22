using ApplicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace SnakeGame
{
    public sealed class WinFormsClock : IClock
    {
        public IDisposable Start(TimeSpan interval, Action onTick)
        {
            var timer = new Timer { Interval = (int)interval.TotalMilliseconds, Enabled = true };
            timer.Tick += (_, __) => onTick();
            timer.Start();
            return new DisposableTimer(timer);
        }

        private sealed class DisposableTimer : IDisposable
        {
            private readonly Timer _timer;
            public DisposableTimer(Timer timer) => _timer = timer;
            public void Dispose()
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }
    }
}
