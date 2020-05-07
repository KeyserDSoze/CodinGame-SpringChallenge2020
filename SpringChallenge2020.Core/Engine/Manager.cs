using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class Manager
    {
        public List<Pac> Pacs { get; } = new List<Pac>();
        public Map Map { get; }
        public Score Score { get; set; }
        public Manager(int x, int y)
        {
            this.Map = new Map(x, y);
        }
        public void SetScore(int mine, int yours)
            => this.Score = new Score(mine, yours);
        public void SetPac(int id, bool isMine, int x, int y, string type, int speedTurnsLeft, int abilityCooldown)
        {
            if (this.Pacs.Count <= id)
                this.Pacs.Add(new Pac(id));
            this.Pacs[id].IsMine = isMine;
            this.Pacs[id].Type = GetType(type);
            this.Pacs[id].SpeedTurnsLeft = speedTurnsLeft;
            this.Pacs[id].AbilityCooldown = abilityCooldown;
            this.Pacs[id].Position = new Position(x, y);
            this.Map.Eat(this.Pacs[id].Position);

            static PacType GetType(string type)
            {
                switch (type)
                {
                    default:
                        return PacType.Paper;
                }
            }
        }
        public void MoveMine()
        {
            foreach (Pac pac in this.Pacs.Where(x => x.IsMine))
                Console.WriteLine(pac.Move(this));
        }
    }
}