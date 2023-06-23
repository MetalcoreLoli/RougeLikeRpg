using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonFactory;
using RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonConfiguration;

using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;

namespace RougeLikeRpg.Engine.GameMaps.Dungeon
{
    public class Dungeon
    {
        private readonly IDungeonConfiguration _configuration;

        private Cell[] _buffer;
        public List<Room> Rooms { get; private set; }

        public Cell[] Buffer => _buffer;
        /*
        public int Width { get; set; }
        public int Height { get; set; }
        public int CountOfRooms { get; set; }
        public Vector2D Location { get; set; }*/

        public Dungeon(IDungeonConfiguration configuration)
        {
            _configuration = configuration;
            Rooms = new List<Room>();
            _buffer = Init(configuration.Width, configuration.Height);
        }

        private Cell[] Init(int width, int height)
        {
            Cell[] temp = new Cell[width * height];
            for(int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    temp[x + width * y] = new Cell(' ', new Vector2D(x, y) + _configuration.Location);
                }
            return temp;
        }

        public void UpdateBuffer(Cell[] buffer)
        {
            _buffer = buffer;
        }

    }
}
