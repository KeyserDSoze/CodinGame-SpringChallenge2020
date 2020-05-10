using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringChallenge2020.Core
{
    public sealed class Map
    {
        private readonly Cell[,] InternalMap;
        public int Width { get; }
        public int Height { get; }
        public Cell this[Position position]
            => this.InternalMap[position.X, position.Y];
        public Map(int x, int y)
        {
            this.InternalMap = new Cell[x, y];
            this.Width = x;
            this.Height = y;
        }
        public void AddRow(int y, string row)
        {
            for (int i = 0; i < row.Length; i++)
                this.InternalMap[i, y] = new Cell(FromInt(row[i]), new Position(i, y));

            static MapType FromInt(char a)
            {
                if (a == '#')
                    return MapType.Wall;
                else
                    return MapType.Pellet;
            }
        }
        public void Eat(Position position)
            => this.InternalMap[position.X, position.Y].ChangeType(MapType.Ate);
        public void Current(Position position, bool isMine)
            => this.InternalMap[position.X, position.Y].ChangeType(isMine ? MapType.MyPac : MapType.EnemyPac);
        public void SetSuperPellet(Position position)
        {
            if (this[position].MapType > MapType.Ate)
            {
                this.InternalMap[position.X, position.Y].ChangeType(MapType.SuperPellet);
                Position theOpposite = new Position(this.Width - position.X, position.Y);
                if (this[theOpposite].MapType > MapType.Ate)
                    this.InternalMap[theOpposite.X, theOpposite.Y].ChangeType(MapType.SuperPellet);
            }
        }

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
            if (this.InternalMap[xM, position.Y].MapType >= MapType.Ate)
                yield return new Position(xM, position.Y);
            if (this.InternalMap[xP, position.Y].MapType >= MapType.Ate)
                yield return new Position(xP, position.Y);
            if (yM >= 0 && this.InternalMap[position.X, yM].MapType >= MapType.Ate)
                yield return new Position(position.X, yM);
            if (yP < this.Height && this.InternalMap[position.X, yP].MapType >= MapType.Ate)
                yield return new Position(position.X, yP);
        }
        public IEnumerable<Cell> GetAround(Position position)
        {
            foreach (var t in GetNextPossibles(position))
                yield return this[t];
        }
        public IEnumerable<Cell> GetEatable()
        {
            for (int i = 0; i < this.Width; i++)
                for (int j = 0; j < this.Height; j++)
                    if (this.InternalMap[i, j].MapType >= MapType.Ate)
                        yield return this.InternalMap[i, j];
        }

        public bool IsAte(Position position)
            => this[position].MapType == MapType.Ate;
    }
}
