using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public readonly record struct Position(int X, int Y)
    {
        public Position Move(Direction dir) => dir switch
        {
            Direction.Up => this with { Y = Y - 1 },
            Direction.Down => this with { Y = Y + 1 },
            Direction.Left => this with { X = X - 1 },
            Direction.Right => this with { X = X + 1 },
            _ => this
        };
    }
}
