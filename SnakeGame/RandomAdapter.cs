using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer;

namespace SnakeGame
{
    public sealed class RandomAdapter : IRandom
    {
        private readonly Random _rnd = new();
        public int Next(int maxExclusive) => _rnd.Next(maxExclusive);
    }
}
