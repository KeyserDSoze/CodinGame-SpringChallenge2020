using System;
using System.Collections.Generic;
using System.Linq;

namespace SpringChallenge2020.Core
{
    public class Engine
    {
        public Move Moves { get; private set; }
        private ArtificialIntelligence AI;
        public Engine()
        {
            this.AI = new SuperPelletMoving();
            this.AI
                .SetNext(new StartMoving())
                    .SetNext(new CrashMoving())
                        .SetNext(new AteMoving());
        }
        public string Move(Manager manager, Pac pac)
        {
            ArtificialIntelligenceResult result = this.AI.Run(manager, pac);
            if (result.HasPosition)
            {
                if (pac.AIPositions.Count > 0)
                    pac.AIPositions.Last().Visited = true;
                pac.AIPositions.Add(new UnderVisiting() { Position = manager.Map[result.Position] });
                Moves = result.ToMove(manager, pac);
            }
            if (result.HasPower)
                return result.Power;
            else
                return Moves.Next();
        }
    }
}
