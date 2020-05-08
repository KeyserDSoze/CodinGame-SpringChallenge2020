using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public sealed class Map
    {
        private readonly MapType[,] InternalMap;
        private readonly int Width;
        private readonly int Height;
        public MapType this[Position position] => this.InternalMap[position.X, position.Y];
        public Map(int x, int y)
        {
            this.InternalMap = new MapType[x, y];
            this.Width = x;
            this.Height = y;
        }
        public void AddRow(int y, string row)
        {
            for (int i = 0; i < row.Length; i++)
                this.InternalMap[i, y] = FromInt(row[i]);

            static MapType FromInt(char a)
            {
                if (a == '#')
                    return MapType.Wall;
                else
                    return MapType.Pellet;
            }
        }
        public void Eat(Position position)
            => this.InternalMap[position.X, position.Y] = MapType.Ate;
        public void SetSuperPellet(Position position)
            => this.InternalMap[position.X, position.Y] = MapType.SuperPellet;
        public IEnumerable<(MapType MapType, Position Position)> GetAround(Position position)
        {
            int xM = position.X - 1;
            int xP = position.X + 1;
            int yM = position.Y - 1;
            int yP = position.Y + 1;
            if (xM < 0)
                xM = this.Width - 1;
            if (yM < 0)
                yM = this.Height - 1;
            if (xP >= this.Width)
                xP = 0;
            if (yP >= this.Height)
                yP = 0;
            yield return (this[new Position(xM, yM)], new Position(xM, yM));
            yield return (this[new Position(xM, yP)], new Position(xM, yP));
            yield return (this[new Position(xP, yM)], new Position(xP, yM));
            yield return (this[new Position(xP, yP)], new Position(xP, yP));
        }
    }


}
