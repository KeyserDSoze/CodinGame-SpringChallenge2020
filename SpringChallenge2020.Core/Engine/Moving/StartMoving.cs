using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class StartMoving : IIntelligence
    {
        public Position Next(Manager manager, Pac pac)
        {
            if (pac.PreviousPosition.Equals(Position.Default))
                return this.GetDensity(manager.Map, pac.Id).OrderByDescending(x => x.MapType).ThenByDescending(x => Math.Abs(pac.Position.X - x.Position.X) + Math.Abs(pac.Position.Y - x.Position.Y)).First().Position;
            return Position.Default;
        }
        private IEnumerable<Cell> GetDensity(Map map, int skip)
            => map.GetEatable().GroupBy(x => x.Position.X / 4 + x.Position.Y / 4).OrderByDescending(x => x.Sum(t => (int)t.MapType)).Skip(skip).FirstOrDefault();
    }
}
