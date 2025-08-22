using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class Game
    {
        public int Width { get; }
        public int Height { get; }
        public SnakeBody Snake { get; }

        public Position Food { get; private set; }
        public bool IsOver { get; private set; }
        public int Score { get; private set; }
        private readonly Func<int, int> _rand;

        public Game(int width, int height, Func<int, int> rand)
        {
            Width = width; Height = height; _rand = rand;

            Snake = new SnakeBody(new Position(width / 2, height / 2), initialLength: 4, Direction.Right);
            Food = RandomEmptyCell();
        }

        public void ChangeDirection(Direction d) => Snake.ChangeDirection(d);

        public void Tick()
        {
            if (IsOver) return;

            var nextHead = Snake.Head.Move(Snake.Direction);

            if (nextHead.X < 0 || nextHead.Y < 0 || nextHead.X >= Width || nextHead.Y >= Height)
            {
                IsOver = true; return;
            }

            bool grow = nextHead.Equals(Food);

            if (Snake.WouldHitSelf(nextHead, grow))
            {
                IsOver = true; return;
            }

            Snake.Step(grow);

            if (grow)
            {
                Score += 10;
                Food = RandomEmptyCell();
            }
        }

        private Position RandomEmptyCell()
        {
            var occupied = new HashSet<Position>(Snake.Segments);
            Position p;
            int guard = 0;
            do
            {
                p = new Position(_rand(Width), _rand(Height));
                if (++guard > 10_000) throw new InvalidOperationException("No empty cells.");
            } while (occupied.Contains(p));
            return p;
        }

    }
}
