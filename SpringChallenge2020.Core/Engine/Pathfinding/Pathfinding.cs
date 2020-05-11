using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class PathItem
    {
        public int CumulativeLength { get; set; }
        public int TotalPrevisionalLength { get; set; }
        public PathItem Precedent { get; set; }
        public Position Position { get; set; }
    }
    public class PathFinderResult
    {
        public static readonly PathFinderResult Empty = new PathFinderResult();
        public List<Position> Path = new List<Position>();
        public int WeightedLength = -1;
        public bool IsNearest = false;

        public bool HasNextCoord()
            => Path.Any();

        public Position GetNextCoord()
            => Path.First();

        public bool HasNoPath()
            => WeightedLength == -1;
    }
    public class PathFinder
    {
        public PathFinderResult FindPath(Map map, Position from, Position to)
        {
            AStar a = new AStar(map, from, to, x => (int)map[x].MapType);
            List<PathItem> pathItems = a.Find();
            PathFinderResult pfr = new PathFinderResult();

            if (!pathItems.Any())
            {
                pfr.IsNearest = true;
                pathItems = new AStar(map, from, a.Nearest, x => (int)map[x].MapType).Find();
            }

            pfr.Path = pathItems.Select(x => x.Position).ToList();
            pfr.WeightedLength = pathItems.FirstOrDefault().CumulativeLength;
            return pfr;
        }
    }
    public class AStar
    {
        private readonly Dictionary<int, PathItem> ClosedList = new Dictionary<int, PathItem>();
        private readonly List<PathItem> OpenList = new List<PathItem>();
        private readonly List<PathItem> Path = new List<PathItem>();
        private readonly Map Map;
        private readonly Position From;
        private readonly Position Target;
        public Position Nearest { get; private set; }
        private readonly Func<Position, int> WeightFunction;

        public AStar(Map map, Position from, Position target, Func<Position, int> weightFunction)
        {
            this.Map = map;
            this.From = from;
            this.Target = target;
            this.WeightFunction = weightFunction;
            this.Nearest = from;
        }

        public List<PathItem> Find()
        {
            PathItem item = GetPathItemLinkedList();
            Path.Clear();
            if (item != null)
                CalculatePath(item);
            return Path;
        }

        private void CalculatePath(PathItem item)
        {
            PathItem i = item;
            while (i != null)
            {
                Path.Add(i);
                i = i.Precedent;
            }
        }

        PathItem GetPathItemLinkedList()
        {
            PathItem root = new PathItem
            {
                Position = this.From
            };
            OpenList.Add(root);

            while (OpenList.Any())
            {
                PathItem visiting = OpenList.OrderBy(x => x.TotalPrevisionalLength).FirstOrDefault();
                OpenList.Remove(visiting);
                Position visitingCoord = visiting.Position;
                if (visitingCoord.Equals(Target))
                    return visiting;

                int key = visitingCoord.GetHashCode();
                if (ClosedList.ContainsKey(key))
                    continue;

                ClosedList.Add(key, visiting);

                IEnumerable<Cell> neighbors = this.Map.GetAround(visitingCoord).Where(x => x.MapType > MapType.Wall);
                foreach (Cell neighbor in neighbors)
                    AddToOpenList(visiting, visitingCoord, neighbor.Position);

                if (visitingCoord.TaxicabDistance(Target, Map) < Nearest.TaxicabDistance(Target, Map))
                    this.Nearest = visitingCoord;
            }
            return null;
        }

        private void AddToOpenList(PathItem visiting, Position fromCoord, Position toCoord)
        {
            int key = toCoord.GetHashCode();
            if (ClosedList.ContainsKey(key))
                return;
            PathItem pi = new PathItem
            {
                Position = toCoord,
                CumulativeLength = visiting.CumulativeLength + WeightFunction.Invoke(toCoord)
            };
            pi.TotalPrevisionalLength = pi.CumulativeLength + fromCoord.TaxicabDistance(toCoord, Map);
            pi.Precedent = visiting;
            OpenList.Add(pi);
        }
    }
}
