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
            this.Intelligences.Add(new SuperPelletMoving());
            this.Intelligences.Add(new StartMoving());
            this.Intelligences.Add(new CrashMoving());
            this.Intelligences.Add(new AteMoving());
        }
        public string Move(Manager manager, Pac pac)
        {
            foreach (IIntelligence intelligence in this.Intelligences)
            {
                Position move = intelligence.Next(manager, pac);
                if (!move.Equals(Position.Default))
                {
                    Moves = new Move(move, manager.Map, pac);
                    break;
                }
            }
            return CheckTheMove(manager, pac);
        }
        private string CheckTheMove(Manager manager, Pac pac)
        {
            if (pac.AbilityCooldown == 0)
            {
                foreach (Pac enemy in manager.EnemyPacs)
                {
                    if (enemy.Position.TaxicabDistance(pac.Position, manager.Map) <= 1)
                    {
                        switch (pac.Type)
                        {
                            case PacType.Paper:
                                Moves = new Move(enemy.Position, manager.Map, pac);
                                return $"SWITCH {pac.Id} ROCK";
                            case PacType.Rock:
                                Moves = new Move(enemy.Position, manager.Map, pac);
                                return $"SWITCH {pac.Id} SCISSORS";
                            case PacType.Scissor:
                                Moves = new Move(enemy.Position, manager.Map, pac);
                                return $"SWITCH {pac.Id} PAPER";
                        }
                    }
                }
            }
            //if (!pac.PreviousPosition.Equals(Position.Default) && pac.SpeedTurnsLeft == 0)
            //    return $"SPEED {pac.Id}";
            return Moves.Next();
        }
    }
}
