using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public class GameService
    {
        private readonly IRandom _random;
        private readonly IClock _clock;
        private Game _game = null!;
        private IDisposable? _timer;

        public event Action<GameState>? StateChanged;
        public GameService(IRandom random, IClock clock)
        {
            _random = random;
            _clock = clock;
            NewGame();
        }

        public void NewGame(int width = 32, int height = 24, TimeSpan? tick = null)
        {
            _timer?.Dispose();
            _game = new Game(width, height, n => _random.Next(n));
            Emit();

            _timer = _clock.Start(tick ?? TimeSpan.FromMilliseconds(120), () =>
            {
                _game.Tick();
                Emit();
            });
        }

        private void Emit() => StateChanged?.Invoke(GetState());

        public GameState GetState() => new()
        {
            Width = _game.Width,
            Height = _game.Height,
            Snake = _game.Snake.Segments,
            Food = _game.Food,
            IsOver = _game.IsOver,
            Score = _game.Score
        };

        public void ChangeDirection(Direction dir)
        {
            _game.ChangeDirection(dir);
        }

    }
}
