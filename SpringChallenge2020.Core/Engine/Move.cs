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
        public Move(Position position, Map map, Pac pac)
        {
            this.Id = pac.Id;
            this.Path = new PathFinder().FindPath(map, pac.Position, position).Path;
            this.CurrentIndex = this.Path.Count - 2;
            this.Set(map);
        }
        public string Next()
            => this.Path[this.CurrentIndex--].ToMoveString(this.Id);
        public bool HasNext()
            => this.CurrentIndex < 0;
        public void Reset(Map map)
        {
            foreach (Position position in Path)
                map[position].IsOnPath = false;
        }
        private void Set(Map map)
        {
            foreach (Position position in Path)
                map[position].IsOnPath = true;
        }
    }
    public static class MoveExtensions
    {
        public static string ToMoveString(this Position position, int id)
            => $"MOVE {id} {position}";
    }
}
