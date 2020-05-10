using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public interface IMoving
    {
        Position Next(Manager manager, Pac pac);
    }
}
