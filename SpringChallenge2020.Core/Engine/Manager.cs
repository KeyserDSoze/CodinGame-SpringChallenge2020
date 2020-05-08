using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class Manager
    {
        public List<Pac> Pacs { get; } = new List<Pac>();
        public List<Pac> EnemyPacs { get; } = new List<Pac>();
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
            Set(isMine ? this.Pacs : this.EnemyPacs);
            void Set(List<Pac> pacs)
            {
                if (pacs.Count <= id)
                    pacs.Add(new Pac(id));
                pacs[id].IsMine = isMine;
                pacs[id].Type = GetType(type);
                pacs[id].SpeedTurnsLeft = speedTurnsLeft;
                pacs[id].AbilityCooldown = abilityCooldown;
                pacs[id].Position = new Position(x, y);
                this.Map.Eat(pacs[id].Position);
            }
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