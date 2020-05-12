using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class SuperPelletMoving : ArtificialIntelligence
    {
        public override ArtificialIntelligenceResult Run(Manager manager, Pac pac)
        {
            var eatable = manager.Map.GetEatable().Where(x => x.MapType > MapType.Pellet && !x.IsOnPath).OrderBy(x => x.Position.TaxicabDistance(pac.Position, manager.Map)).FirstOrDefault();
            if (eatable != null && !pac.AreVisitingSuperPellet())
                return new ArtificialIntelligenceResult(eatable.Position);
            return this.InvokeNext(manager, pac);
        }
    }
}
