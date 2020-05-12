﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class Pac
    {
        public int Id { get; }
        public bool IsMine { get; set; }
        public PacType Type { get; set; }
        public Position Position { get; set; } = Position.Default;
        public int AbilityCooldown { get; set; }
        public int SpeedTurnsLeft { get; set; }
        public Position PreviousPosition { get; set; } = Position.Default;
        public List<UnderVisiting> AIPositions { get; } = new List<UnderVisiting>();
        public Position ForescastPosition()
        {
            return default;
        }

        public Pac(int id)
            => this.Id = id;
        public Engine Engine { get; } = new Engine();
        public string Move(Manager manager)
            => Engine.Move(manager, this);
    }
    public class UnderVisiting
    {
        public Position Position { get; set; }
        public bool Visited { get; set; }
    }
}
