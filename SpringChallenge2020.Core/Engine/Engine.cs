using System;
using System.Collections.Generic;
using System.Linq;

namespace SpringChallenge2020.Core
{
    public class Engine
    {
        public Position Next = Position.Default;
        public Move Move(Manager manager, Pac pac)
        {
            if (pac.Position.Equals(Next) || Next.Equals(Position.Default) || manager.Map.IsAte(Next))
            {
                Next = this.GetDensity(manager.Map).OrderByDescending(x => x.MapType).ThenByDescending(x => Math.Abs(pac.Position.X - x.Position.X) + Math.Abs(pac.Position.Y - x.Position.Y)).First().Position;
            }
            return new Move("MOVE", pac.Id, Next);
        }
        private IEnumerable<(MapType MapType, Position Position)> GetDensity(Map map)
            => map.GetEatable().GroupBy(x => x.Position.X / 4 + x.Position.Y / 4).OrderByDescending(x => x.Sum(t => (int)t.MapType)).FirstOrDefault();
    }
}
