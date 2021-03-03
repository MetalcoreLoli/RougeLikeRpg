using System;
using System.Linq;
using RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonConfiguration;
using RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonFactory;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Engine.GameMaps.Dungeon
{
    public class DungeonBuilder
    {
        private readonly IDungeonConfiguration _configuration;
        private readonly AbstractDungeonFactory _factory;

        private readonly Cell[] _dungeonFloorBuffer;
        private readonly Dungeon _dungeon;

        public DungeonBuilder(AbstractDungeonFactory factory, IDungeonConfiguration configuration)
        {
            _configuration = configuration;
            _factory = factory;
            _dungeon = new Dungeon(_configuration);

            _dungeonFloorBuffer = CreateDungeonFloorBuffer();
        }

        #region Private Methods

        public Cell[] CreateDungeonFloorBuffer()
        {
            var tmp = new Cell[_configuration.Width * _configuration.Height];

            for (int y = 0; y < _configuration.Height; y++)
            {
                for (int x = 0; x < _configuration.Width; x++)
                {
                    int idx = x + y * _configuration.Width;
                    tmp[idx] = new Cell()
                    {
                        Position = new Vector2D(x, y) + _configuration.Location,
                        Symbol = ' ',
                        Color = ColorManager.White,
                        BackColor =  ColorManager.Black
                    };
                }
            }
            return tmp;
        }

        private void CreateHorizontalPath(int xStart, int xEnd, int yPosition, Color color, Color backColor)
        {
            int min = Math.Min(xStart, xEnd);
            int max = Math.Max(xStart, xEnd);
            
            for (int x = min; x < max + 1; x++)
            {
                _dungeonFloorBuffer[x + yPosition * _configuration.Width]  = new Cell 
                {
                    BackColor =backColor,
                    Color = color,
                    Symbol= '.',
                    Position = new Vector2D(x, yPosition)
                };

                int upIdx = x + (yPosition - 1) * _configuration.Width;
                int downIdx = x + (yPosition + 1) * _configuration.Width;
                if (_dungeonFloorBuffer[upIdx].Symbol != '.')
                    _dungeonFloorBuffer[upIdx] = new Cell
                    {
                        BackColor = backColor,
                        Color = color,
                        Symbol = '═',
                        Position = new Vector2D(x, yPosition - 1)
                    };

                if (_dungeonFloorBuffer[downIdx].Symbol != '.')
                    _dungeonFloorBuffer[downIdx] = new Cell
                    {
                        BackColor = backColor,
                        Color = color,
                        Symbol = '═',
                        Position = new Vector2D(x, yPosition + 1)
                    };
            }
        }

        private void CreateVecticalPath(int yStart, int yEnd, int xPosition, Color color, Color backColor)
        {
            int min = Math.Min(yStart, yEnd);
            int max = Math.Max(yStart, yEnd);

            for (int y = min; y < max + 1; y++)
            {
                _dungeonFloorBuffer[xPosition + y * _configuration.Width] = new Cell
                {
                    BackColor = backColor,
                    Color = color,
                    Symbol = '.',
                    Position = new Vector2D(xPosition, y)
                };


                int upIdx = (xPosition - 1) + y * _configuration.Width;
                int downIdx = (xPosition + 1) + y * _configuration.Width;
                if (_dungeonFloorBuffer[upIdx].Symbol != '.')
                    _dungeonFloorBuffer[upIdx] = new Cell
                    {
                        BackColor = backColor,
                        Color = color,
                        Symbol = '║',
                        Position = new Vector2D(xPosition - 1, y)
                    };

                if (_dungeonFloorBuffer[downIdx].Symbol != '.')
                    _dungeonFloorBuffer[downIdx] = new Cell
                    {
                        BackColor = backColor,
                        Color = color,
                        Symbol = '║',
                        Position = new Vector2D(xPosition + 1, y)
                    };
            }
        }
        
        private void ConnectTwoRooms(Room prev, Room current)
        {
            int path = new Random().Next(0, 2);
            if (path == 0)
            {
                CreateHorizontalPath(prev.GetCenter().X, current.GetCenter().X, prev.GetCenter().Y, ColorManager.White,
                    ColorManager.Black);
                CreateVecticalPath(current.GetCenter().Y, prev.GetCenter().Y, current.GetCenter().X, ColorManager.White,
                    ColorManager.Black);
            }
            else
            {
                CreateHorizontalPath(current.GetCenter().X, prev.GetCenter().X, current.GetCenter().Y, ColorManager.White,
                    ColorManager.Black);
                CreateVecticalPath(prev.GetCenter().Y, current.GetCenter().Y, prev.GetCenter().X, ColorManager.White,
                    ColorManager.Black);
            }
        }
        private Room GenerateRoom(AbstractDungeonFactory f, IDungeonConfiguration configuration)
        {
            int width = new Random().Next(configuration.MinRoomWidth, configuration.MaxRoomWidth);
            int height = new Random().Next(configuration.MinRoomHeight, configuration.MaxRoomHeight);
            int x = new Random().Next(0, configuration.Width - width);
            int y = new Random().Next(0, configuration.Height - height);
            return f.MakeRoom(width, height, new Vector2D(x, y));
        }
        
        private bool CanRoomBePlaced(Room room)
        {
            return _dungeon.Rooms.All(r => !r.Intersect(room));
        }
        private void AddRoom(Room room)
        {
            foreach (var cell in room.Body)
            {
                int idx = cell.Position.X + cell.Position.Y * _configuration.Width;
                _dungeonFloorBuffer[idx] = cell;
            }
            _dungeon.Rooms.Add(room);
        }
        #endregion

        #region Public Methods
        
        public DungeonBuilder GenerateRooms()
        {
            for (int i = 0; i < _configuration.CountOfRooms; i++)
            {
                var room = GenerateRoom(_factory, _configuration);
                while (!CanRoomBePlaced(room))
                    room = GenerateRoom(_factory, _configuration);
                AddRoom(room);
            }
            return this;
        }

        public DungeonBuilder ConnectAllRooms()
        {
            for (int i = 1; i < _configuration.CountOfRooms; i++)
            {
                var prev = _dungeon.Rooms[i - 1];
                var current = _dungeon.Rooms[i];
                ConnectTwoRooms(prev, current);
            }
            return this;
        }
        public Dungeon Construct()
        {
            _dungeon.UpdateBuffer(_dungeonFloorBuffer);
            return _dungeon;
        }

        #endregion
        
    }
}