using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class CrashMoving : IMoving
    {
        public Position Next(Manager manager, Pac pac)
        {
            if (pac.Position.Equals(pac.PreviousPosition))
            {
                var eatable = manager.Map.GetEatable().ToList();
                return eatable[new Random().Next(eatable.Count)].Position;
            }
            return Position.Default;
        }
    }
}
