using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public interface IIntelligence
    {
        Position Next(Manager manager, Pac pac);
    }
}
