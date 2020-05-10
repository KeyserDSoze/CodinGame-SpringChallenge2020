using System;
using System.Collections.Generic;
using System.Linq;

namespace SpringChallenge2020.Core
{
    public class Engine
    {
        public Move Next { get; private set; }
        public IList<IMoving> Movings;
        public Engine()
        {
            this.Movings = new List<IMoving>();
            this.Movings.Add(new StartMoving());
            this.Movings.Add(new CrashMoving());
            this.Movings.Add(new SuperPelletMoving());
            this.Movings.Add(new AteMoving());
        }
        public string Move(Manager manager, Pac pac)
        {
            foreach (IMoving moving in this.Movings)
            {
                Position move = moving.Next(manager, pac);
                if (!move.Equals(Position.Default))
                {
                    Next = new Move(pac.Id, move, manager.Map, pac);
                    break;
                }
            }
            return Next.ToString("MOVE");
        }
    }
}
