using System;
using System.Collections.Generic;
using System.Text;

namespace SpringChallenge2020.Core
{
    public sealed class Map
    {
        private readonly MapType[,] InternalMap;
        public MapType this[Position position] => this.InternalMap[position.X, position.Y];
        public Map(int x, int y)
        {
            this.InternalMap = new MapType[x, y];
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
    }


}
