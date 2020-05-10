using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public class Cell
    {
        public MapType MapType { get; private set; }
        public Position Position { get; }
        public Cell(MapType mapType, Position position)
        {
            this.MapType = mapType;
            this.Position = position;
        }
        public void ChangeType(MapType mapType)
            => this.MapType = mapType;
    }
}
