using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class AteMoving : ArtificialIntelligence
    {
        public override ArtificialIntelligenceResult Run(Manager manager, Pac pac)
        {
            if (pac.Engine.Moves != null)
                //check current is the pac position or the last is ate
                if (pac.Engine.Moves.HasNext() || manager.Map.IsAte(pac.Engine.Moves.Last))
                {
                    var t = manager.Map.GetClosedWay().Where(x => x.MapType >= MapType.Pellet).OrderBy(x => x.Position.TaxicabDistance(pac.Position, manager.Map)).ToList();
                    return new ArtificialIntelligenceResult(t.FirstOrDefault().Position);
                }
            return this.InvokeNext(manager, pac);
        }
    }
}
