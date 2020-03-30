using RougeLikeRPG.Graphic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougeLikeRPG.Engine.GameMaps.Dungeon
{
    internal class Room
    {
        public Int32 Width { get; set; }
        public Int32 Height { get; set; }

        public Vector2D Location { get; set; }
        
        public Cell[] Body { get; private set; }

        public List<Actors.Actor> Actors { get; set; }

        public Room(Int32 Width, Int32 Height, Vector2D location)
        {
            this.Width      = Width;
            this.Height     = Height;
            this.Location   = location;
            Body            = Init(this.Width, this.Height);
            Actors = new List<Actors.Actor>();
            PlaceActors();
        }

        private void PlaceActors()
        {
            int countOfActors = 3;
            while (countOfActors-- > 0)
            {
                int x = new Random().Next(1, Width - 1);
                int y = new Random().Next(1, Height - 1);
                Actors.Add(new Actors.Monsters.Goblin() { 
                    Position = new Vector2D(x, y) + Location
                });
            }
        }
        private Cell[] Init(Int32 Width, Int32 Height)
        {
            Cell[] temp = new Cell[Width * Height];

            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    int idx = x + Width * y;
                    temp[idx] = new Cell('.', new Vector2D(x, y) + this.Location);
                }

            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    int idx = x + Width * y;
                    if (x == 0)
                        temp[idx].Symbol = '║';

                    if (y == 0)
                        temp[idx].Symbol = '═';

                    if (y == Height - 1)
                        temp[idx].Symbol = '═';

                    if (x == Width - 1)
                        temp[idx].Symbol = '║';

                    if (x == 0 && y == 0)
                        temp[idx].Symbol = '╔';

                    if (x == 0 && y == Height - 1)
                        temp[idx].Symbol = '╚';

                    if (x == Width - 1 && y == Height - 1)
                        temp[idx].Symbol = '╝';

                    if (x == Width - 1 && y == 0)
                        temp[idx].Symbol = '╗';
                }
             
            return temp;
        }

        public bool Intersect(Room room)
        {
            foreach (Cell cell in room.Body)
            {
                if (Body.Select(c => c.Position).Contains(cell.Position))
                    return true;
            }
            return false;
        }

        internal Vector2D GetCenter()
        {
            int idx = (Width / 2) + Width * (Height / 2);
            return Body[idx].Position;
        }
    }
}
