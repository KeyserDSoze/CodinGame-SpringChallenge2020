using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class CrashMoving : ArtificialIntelligence
    {
        public override ArtificialIntelligenceResult Run(Manager manager, Pac pac)
        {
            if (manager.Pacs.Where(x => x != pac).Any(x => x.Position.TaxicabDistance(pac.Position, manager.Map) <= 2))
            {
                var eatable = manager.Map.GetEatable().ToList();
                return new ArtificialIntelligenceResult(eatable[new Random().Next(eatable.Count)].Position);
            }
            return this.InvokeNext(manager, pac);
        }
    }
}
