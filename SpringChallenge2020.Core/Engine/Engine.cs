using System;
using System.Linq;

namespace SpringChallenge2020.Core
{
    public class Engine
    {
        public static Move Move(Manager manager, Pac pac)
        {
            (MapType MapType, Position Position) nextMove = manager.Map.GetAround(pac.Position).OrderByDescending(x => x.MapType).FirstOrDefault();
            return new Move("MOVE", pac.Id, nextMove.Position);
        }
    }
}
