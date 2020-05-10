using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public sealed class Map
    {
        private readonly MapType[,] InternalMap;
        public int Width { get; }
        public int Height { get; }
        public MapType this[Position position] => this.InternalMap[position.X, position.Y];
        public (MapType Type, Position Position) GetFloor(Position position)
            => (this[position], position);
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
        public void Current(Position position, bool isMine)
            => this.InternalMap[position.X, position.Y] = isMine ? MapType.MyPac : MapType.EnemyPac;
        public void SetSuperPellet(Position position)
            => this.InternalMap[position.X, position.Y] = MapType.SuperPellet;
        public IEnumerable<Position> GetNextPossibles(Position position)
        {
            int xM = position.X - 1;
            int xP = position.X + 1;
            int yM = position.Y - 1;
            int yP = position.Y + 1;
            if (xM < 0)
                xM = this.Width - 1;
            if (xP >= this.Width)
                xP = 0;
            if (this.InternalMap[xM, position.Y] >= 0)
                yield return new Position(xM, position.Y);
            if (this.InternalMap[xP, position.Y] >= 0)
                yield return new Position(xP, position.Y);
            if (yM >= 0)
                yield return new Position(position.X, yM);
            if (yP < this.Height)
                yield return new Position(position.Y, yP);
        }
        public IEnumerable<(MapType MapType, Position Position)> GetAround(Position position)
        {
            foreach (var t in GetNextPossibles(position))
                yield return (this[t], t);
        }
        public IEnumerable<(MapType MapType, Position Position)> GetEatable()
        {
            for (int i = 0; i < this.Width; i++)
                for (int j = 0; j < this.Height; j++)
                    if (this.InternalMap[i, j] >= 0)
                        yield return (this.InternalMap[i, j], new Position(i, j));
        }

        public bool IsAte(Position position)
            => this[position] == MapType.Ate;
    }
}
