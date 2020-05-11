using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class AteMoving : IMoving
    {
        public Position Next(Manager manager, Pac pac)
        {
            if (pac.Engine.Moves != null)
                if (pac.Position.Equals(pac.Engine.Moves) || pac.Engine.Moves.Equals(Position.Default) || manager.Map.IsAte(pac.Engine.Moves.Last))
                {
                    return manager.Map.GetEatable().OrderBy(x => Math.Abs(pac.Position.X - x.Position.X) + Math.Abs(pac.Position.Y - x.Position.Y)).FirstOrDefault().Position;
                }
            return Position.Default;
        }
    }
}
