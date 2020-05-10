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
        public Position Current => this.Path[this.CurrentIndex];
        public Position Last => this.Path.Last();
        private int CurrentIndex = 0;
        private readonly IList<Position> Path = new List<Position>();
        public Move(int id, Position position, Map map, Pac pac)
        {
            this.Id = id;
            Path = GetNext(map, pac.Position, position, 0, position.TaxicabDistance(pac.Position) * 2);
        }
        private static readonly List<Position> Empty = new List<Position>();
        private readonly Dictionary<int, List<Position>> Results = new Dictionary<int, List<Position>>();
        private List<Position> GetNext(Map map, Position current, Position last, int step, int maxStep)
        {
            if (step > maxStep)
                return Empty;
            int key = current.GetHashCode();
            if (Results.ContainsKey(key))
                return Results[key];
            List<Position> positions = new List<Position>();
            positions.Add(current);
            List<Position> maxer = new List<Position>();
            int max = 0;
            foreach (var point in map.GetAround(current).Where(x => x.MapType >= MapType.Wall))
            {
                if (!point.Position.Equals(last))
                {
                    maxer = new List<Position>() { point.Position };
                    break;
                }
                else
                {
                    List<Position> possibleMax = GetNext(map, point.Position, last, step + 1, maxStep);
                    if (possibleMax.Count == 0)
                        continue;
                    int sum = possibleMax.Sum(x => (int)map[x].MapType);
                    if (sum > max)
                    {
                        max = sum;
                        maxer = possibleMax;
                    }
                }
            }
            positions.AddRange(maxer);
            Results.Add(key, positions);
            return positions;
        }
        public string ToString(string power)
            => this.Path[this.CurrentIndex++].ToMoveString(power, this.Id);
    }
    public static class MoveExtensions
    {
        public static string ToMoveString(this Position position, string power, int id)
            => $"{power} {id} {position}";
    }
}
