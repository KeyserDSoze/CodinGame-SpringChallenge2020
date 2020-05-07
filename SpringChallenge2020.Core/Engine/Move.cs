using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class Move
    {
        public string Power { get; }
        public int Id { get; }
        public Position Position { get; }
        public Move(string power, int id, Position position)
        {
            this.Position = position;
            this.Power = power;
            this.Id = id;
        }
        public override string ToString()
            => $"{this.Power} {this.Id} {this.Position}";
    }
}
