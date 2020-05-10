using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class SuperPelletMoving : IMoving
    {
        public Position Next(Manager manager, Pac pac)
        {
            var eatable = manager.Map.GetEatable().Where(x => x.MapType > MapType.Pellet).Skip(pac.Id).FirstOrDefault();
            if (eatable != null)
            {
                return eatable.Position;
            }
            return Position.Default;
        }
    }
}
