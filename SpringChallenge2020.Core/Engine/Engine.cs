using System;

namespace SpringChallenge2020.Core
{
    public class Engine
    {
        public static Move Move(Manager manager, Pac pac)
        {
            return new Move("MOVE", pac.Id, new Position(0, 0));
        }
    }
}
