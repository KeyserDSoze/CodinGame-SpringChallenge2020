using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public abstract class ArtificialIntelligence
    {
        private ArtificialIntelligence Next;
        public abstract ArtificialIntelligenceResult Run(Manager manager, Pac pac);
        public ArtificialIntelligence SetNext(ArtificialIntelligence next)
            => this.Next = next;
        private protected ArtificialIntelligenceResult InvokeNext(Manager manager, Pac pac)
            => Next?.Run(manager, pac) ?? ArtificialIntelligenceResult.Empty;
        private protected void Log(string message)
            => Console.Error.WriteLine($"{this.GetType().Name}=> {message}");
    }
    public class ArtificialIntelligenceResult
    {
        public Position Position { get; } = Position.Default;
        public bool HasPosition => !this.Position.Equals(Position.Default);
        public string Power { get; }
        public bool HasPower => !string.IsNullOrWhiteSpace(this.Power);
        public Move ToMove(Manager manager, Pac pac)
            => new Move(this.Position, manager.Map, pac);
        public ArtificialIntelligenceResult(Position position)
            => this.Position = position;
        public ArtificialIntelligenceResult(string power)
            => this.Power = power;
        public ArtificialIntelligenceResult(Position position, string power) : this(position)
            => this.Power = power;
        public ArtificialIntelligenceResult() { }
        public static readonly ArtificialIntelligenceResult Empty = new ArtificialIntelligenceResult();
    }
}
