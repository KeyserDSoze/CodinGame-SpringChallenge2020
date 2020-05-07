using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public struct Score
    {
        public int Mine { get; }
        public int Yours { get; }
        public Score(int mine, int yours)
        {
            this.Mine = mine;
            this.Yours = yours;
        }
    }
}
