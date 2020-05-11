using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SpringChallenge2020.Core
{
    public class Move
    {
        public int Id { get; }
        public Position Current
            => this.Path[this.CurrentIndex];
        public Position Last
            => this.Path.FirstOrDefault();
        private int CurrentIndex = 0;
        private readonly IList<Position> Path;
        public Move(int id, Position position, Map map, Pac pac)
        {
            this.Id = id;
            this.Path = new PathFinder().FindPath(map, pac.Position, position).Path;
            this.CurrentIndex = this.Path.Count - 1;
        }
        public string Next(string power)
            => this.Path[this.CurrentIndex--].ToMoveString(power, this.Id);
        public bool HasNext()
            => this.CurrentIndex < 0;
    }
    public static class MoveExtensions
    {
        public static string ToMoveString(this Position position, string power, int id)
            => $"{power} {id} {position}";
    }
}
