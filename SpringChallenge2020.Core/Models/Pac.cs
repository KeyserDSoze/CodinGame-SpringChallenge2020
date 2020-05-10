using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class Pac
    {
        public int Id { get; }
        public bool IsMine { get; set; }
        public PacType Type { get; set; }
        public Position Position { get; set; }
        public int AbilityCooldown { get; set; }
        public int SpeedTurnsLeft { get; set; }
        public Pac(int id)
            => this.Id = id;
        private readonly Engine Engine = new Engine();
        public string Move(Manager manager)
            => Engine.Move(manager, this).ToString();
    }
}
