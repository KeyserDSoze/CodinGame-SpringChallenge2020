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
        public static Position Default = new Position(-1, -1);
        public override bool Equals(object obj)
            => (obj?.GetHashCode() ?? -1) == this.GetHashCode();
        public override int GetHashCode()
            => X + Y * 10000;
        public int TaxicabDistance(Position b, Map map)
        {
            int dh = Math.Abs(this.Y - b.Y);
            int dv = Math.Min(
                Math.Abs(this.X - b.X),
                Math.Min(this.X + map.Width - b.X, b.X + map.Width - this.X)
            );
            return dv + dh;
        }
    }
}
