using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class Manager
    {
        public List<Pac> Pacs { get; } = new List<Pac>();
        public List<Pac> EnemyPacs { get; private set; } = new List<Pac>();
        public Map Map { get; }
        public Score Score { get; set; }
        public Manager(int x, int y)
        {
            this.Map = new Map(x, y);
        }
        public void SetScore(int mine, int yours)
            => this.Score = new Score(mine, yours);
        public void NewTurn()
        {
            this.EnemyPacs = new List<Pac>();
        }
        public void SetPac(int id, bool isMine, int x, int y, string type, int speedTurnsLeft, int abilityCooldown)
        {
            Set(isMine ? this.Pacs : this.EnemyPacs);
            void Set(List<Pac> pacs)
            {
                if (!pacs.Any(x => x.Id == id))
                    pacs.Add(new Pac(id));
                Pac pac = pacs.FirstOrDefault(x => x.Id == id);
                pac.IsMine = isMine;
                pac.Type = GetType(type);
                pac.SpeedTurnsLeft = speedTurnsLeft;
                pac.AbilityCooldown = abilityCooldown;
                pac.PreviousPosition = pac.Position;
                pac.Position = new Position(x, y);
                if (!pac.PreviousPosition.Equals(Position.Default))
                    this.Map.Eat(pac.PreviousPosition);
                if (isMine)
                    this.Map.Current(pac.Position, isMine);
            }
            static PacType GetType(string type)
            {
                switch (type)
                {
                    default:
                    case "PAPER":
                        return PacType.Paper;
                    case "SCISSOR":
                        return PacType.Scissor;
                    case "ROCK":
                        return PacType.Rock;
                }
            }
        }
        public void MoveMine()
        {
            Console.WriteLine(string.Join(" | ", this.Pacs.Select(pac => pac.Move(this))));
        }
    }
}