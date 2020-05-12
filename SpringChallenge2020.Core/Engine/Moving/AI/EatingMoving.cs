using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class EatingMoving : ArtificialIntelligence
    {
        public override ArtificialIntelligenceResult Run(Manager manager, Pac pac)
        {
            foreach (Pac enemy in manager.EnemyPacs)
            {
                if (enemy.Position.TaxicabDistance(pac.Position, manager.Map) <= 2)
                {
                    if (pac.AbilityCooldown == 0)
                    {
                        //insert the possibility to hunt the pac
                        switch (pac.Type)
                        {
                            case PacType.Paper:
                                return new ArtificialIntelligenceResult(enemy.Position, $"SWITCH {pac.Id} ROCK");
                            case PacType.Rock:
                                return new ArtificialIntelligenceResult(enemy.Position, $"SWITCH {pac.Id} SCISSORS");
                            case PacType.Scissor:
                                return new ArtificialIntelligenceResult(enemy.Position, $"SWITCH {pac.Id} PAPER");
                        }
                    }
                    else
                    {
                        //todo: runaway if i changed my type in last ten turns
                    }
                }
            }
            return this.InvokeNext(manager, pac);
        }
    }
}
