using RougeLikeRpg.Graphic.Core;
using RougeLikeRPG.Graphic.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.GameMaps.Dungeon
{
    internal class Dungeon
    {
        private int width;
        private int height;
        private Cell[] body;
        private Vector2D location;

        public List<Room> Rooms { get; private set; }

        public int MinRoomWidth { get; set; } 
        public int MaxRoomWidth { get; set; }

        public int MinRoomHeight { get; set; }
        public int MaxRoomHeight { get; set; }
        public int CountOfRooms { get; set; }

        public Dungeon(int width, int height, Vector2D location)
        {
            this.width = width;
            this.height = height;
            this.location = location;
            Rooms = new List<Room>();
            body = Init(width, height);
        }

        private Cell[] Init(int width, int height)
        {
            Cell[] temp = new Cell[width * height];
            for(int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    temp[x + width * y] = new Cell(' ', new Vector2D(x, y) + location);
                }
            return temp;
        }

        private bool CanRoomBePlaced(Room room)
        {
            foreach (var r in Rooms)
            {
                if (r.Intersect(room))
                    return false;
            }
            return true;
        }

        internal Cell[] Generate()
        {
            while (CountOfRooms-- > 0)
            {
                int width = new Random().Next(MinRoomWidth, MaxRoomWidth);
                int height = new Random().Next(MinRoomHeight, MaxRoomHeight);
                int x = new Random().Next(0, this.width - width);
                int y = new Random().Next(0, this.height - height);
                Room room = new Room(width, height, new Vector2D(x, y));

                while (!CanRoomBePlaced(room))
                {
                    width = new Random().Next(MinRoomWidth, MaxRoomWidth);
                    height = new Random().Next(MinRoomHeight, MaxRoomHeight);
                    x = new Random().Next(0, this.width - width);
                    y = new Random().Next(0, this.height - height);
                    room = new Room(width, height, new Vector2D(x, y));
                }
                AddRoomToDungeon(room);
            }

            for (int i = 1; i < Rooms.Count; i++)
            {
                Room prev       = Rooms[i - 1];
                Room current    = Rooms[i];

                int path = new Random().Next(0, 2);

                if (path == 0)
                {
                    CreateHorizontalPath(prev.GetCenter().X, current.GetCenter().X, prev.GetCenter().Y, ColorManager.White, ColorManager.Black);
                    CreateVecticalPath(current.GetCenter().Y, prev.GetCenter().Y, current.GetCenter().X, ColorManager.White, ColorManager.Black);
                }
                else
                {
                    CreateHorizontalPath(current.GetCenter().X, prev.GetCenter().X, current.GetCenter().Y, ColorManager.White, ColorManager.Black);
                    CreateVecticalPath(prev.GetCenter().Y, current.GetCenter().Y, prev.GetCenter().X, ColorManager.White, ColorManager.Black);
                }
            }
            Console.Title = "Location was generated";
            return body;
        }

        public void AddRoomToDungeon(Room room)
        {
            Rooms.Add(room);
            Console.Title = $"Generating room number {CountOfRooms}...";
            foreach (Cell cell in room.Body)
                SetCell(cell);
        }


        private void CreateHorizontalPath(int xStart, int xEnd, int yPosition, Color color, Color backColor)
        {
            int min = Math.Min(xStart, xEnd);
            int max = Math.Max(xStart, xEnd);
            
            for (int x = min; x < max + 1; x++)
            {
                SetCell(new Cell 
                {
                    BackColor =backColor,
                    Color = color,
                    Symbol= '.',
                    Position = new Vector2D(x, yPosition)
                });

                if (GetCell(x, yPosition - 1).Symbol != '.')
                    SetCell(new Cell
                    {
                        BackColor = backColor,
                        Color = color,
                        Symbol = '═',
                        Position = new Vector2D(x, yPosition - 1)
                    });

                if (GetCell(x, yPosition + 1).Symbol != '.')
                    SetCell(new Cell
                    {
                        BackColor = backColor,
                        Color = color,
                        Symbol = '═',
                        Position = new Vector2D(x, yPosition + 1)
                    });
            }
        }

        private void CreateVecticalPath(int yStart, int yEnd, int xPosition, Color color, Color backColor)
        {
            int min = Math.Min(yStart, yEnd);
            int max = Math.Max(yStart, yEnd);

            for (int y = min; y < max + 1; y++)
            {
                SetCell(new Cell
                {
                    BackColor = backColor,
                    Color = color,
                    Symbol = '.',
                    Position = new Vector2D(xPosition, y)
                });

                if (GetCell(xPosition - 1, y).Symbol != '.')
                    SetCell(new Cell
                    {
                        BackColor = backColor,
                        Color = color,
                        Symbol = '║',
                        Position = new Vector2D(xPosition - 1, y)
                    });

                if (GetCell(xPosition + 1, y).Symbol != '.')
                    SetCell(new Cell
                    {
                        BackColor = backColor,
                        Color = color,
                        Symbol = '║',
                        Position = new Vector2D(xPosition + 1, y)
                    });
            }
        }

        private void SetCell(Cell cell)
        {
            int idx = cell.Position.X + width * cell.Position.Y;
            body[idx].Symbol    = cell.Symbol;
            body[idx].Color     = cell.Color;
            body[idx].BackColor = cell.BackColor;
        }

        private Cell GetCell(int x, int y)
        {
            int idx = x + width * y;
            return body[idx];
        }
    }
}
