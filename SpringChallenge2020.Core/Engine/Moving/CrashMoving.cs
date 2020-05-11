using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class CrashMoving : IIntelligence
    {
        public Position Next(Manager manager, Pac pac)
        {
            if (manager.Pacs.Where(x => x != pac).Any(x => x.Position.TaxicabDistance(pac.Position, manager.Map) <= 2))
            {
                var eatable = manager.Map.GetEatable().ToList();
                return eatable[new Random().Next(eatable.Count)].Position;
            }
            return Position.Default;
        }
    }
}
