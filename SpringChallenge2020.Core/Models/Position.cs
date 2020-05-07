using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public struct Position
    {
        public int X { get; }
        public int Y { get; }
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public override string ToString()
            => $"{this.X} {this.Y}";
    }
}
