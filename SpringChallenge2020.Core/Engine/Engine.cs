using System;
using System.Collections.Generic;
using System.Linq;

namespace SpringChallenge2020.Core
{
    public class Engine
    {
        public Move Moves { get; private set; }
        public IList<IIntelligence> Intelligences { get; } = new List<IIntelligence>();
        public Engine()
        {
            this.Intelligences.Add(new StartMoving());
            this.Intelligences.Add(new CrashMoving());
            this.Intelligences.Add(new SuperPelletMoving());
            this.Intelligences.Add(new AteMoving());
        }
        public string Move(Manager manager, Pac pac)
        {
            foreach (IIntelligence intelligence in this.Intelligences)
            {
                Position move = intelligence.Next(manager, pac);
                if (!move.Equals(Position.Default))
                {
                    Moves = new Move(pac.Id, move, manager.Map, pac);
                    break;
                }
            }
            return Moves.Next("MOVE");
        }
    }
}
