using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class SnakeBody
    {
        private readonly LinkedList<Position> _segments = new();
        public Direction Direction { get; private set; }
        public IEnumerable<Position> Segments => _segments;
        public Position Head => _segments.First!.Value;

        public SnakeBody(Position start, int initialLength, Direction dir)
        {
            Direction = dir;
            for (int i = 0; i < initialLength; i++)
                _segments.AddLast(new Position(start.X - i, start.Y));
        }

        public void ChangeDirection(Direction newDir)
        {
            if (!DirectionOps.IsOpposite(Direction, newDir))
                Direction = newDir;
        }

        public void Step(bool grow)
        {
            var newHead = Head.Move(Direction);
            _segments.AddFirst(newHead);
            if (!grow) _segments.RemoveLast();
        }

        public bool WouldHitSelf(Position pos, bool grow)
        {
            var tailToIgnore = grow ? (Position?)null : _segments.Last!.Value;

            int idx = 0;
            foreach (var s in _segments)
            {
                if (idx++ == 0) continue;

                if (!grow && tailToIgnore.HasValue && s.Equals(tailToIgnore.Value))
                    continue;

                if (s.Equals(pos)) return true;
            }
            return false;
        }

    }
}
