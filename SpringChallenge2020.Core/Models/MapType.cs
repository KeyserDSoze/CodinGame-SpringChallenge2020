using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SpringChallenge2020.Core
{
    public enum MapType
    {
        Wall = -3,
        EnemyPac = -2,
        MyPac = -1,
        Ate = 0,
        Pellet = 1,
        SuperPellet = 10,
    }
}
