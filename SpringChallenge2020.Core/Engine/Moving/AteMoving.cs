using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class AteMoving : IMoving
    {
        public Position Next(Manager manager, Pac pac)
        {
            if (pac.Position.Equals(pac.Engine.Next) || pac.Engine.Next.Equals(Position.Default) || manager.Map.IsAte(pac.Engine.Next))
            {
                return Position.Default;
            }
            return Position.Default;
        }
    }
}
