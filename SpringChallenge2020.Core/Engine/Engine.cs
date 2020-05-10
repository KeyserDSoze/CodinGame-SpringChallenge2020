using System;
using System.Collections.Generic;
using System.Linq;

namespace SpringChallenge2020.Core
{
    public class Engine
    {
        public Position Next = Position.Default;
        public IList<IMoving> Movings;
        public Engine()
        {
            this.Movings = new List<IMoving>();
            this.Movings.Add(new StartMoving());
            this.Movings.Add(new CrashMoving());
        }
        public Move Move(Manager manager, Pac pac)
        {
            foreach (IMoving moving in this.Movings)
            {
                Position move = moving.Next(manager, pac);
                if (!move.Equals(Position.Default))
                {
                    Next = move;
                    break;
                }
            }
            return new Move("MOVE", pac.Id, Next);
        }
    }
}
