using RougeLikeRPG.Graphic.Core;
using RougeLikeRPG.Graphic.Core.Controls;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Actors.Enums;
using RougeLikeRPG.Engine.Actors.Monsters;
using RougeLikeRPG.Engine.GameMaps;
using RougeLikeRPG.Engine.GameMaps.Dungeon.DungeonFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RougeLikeRpg.Graphic.Core;
using RougeLikeRPG.Engine.GameMaps.Dungeon;

namespace RougeLikeRPG.Engine
{
    /// <summary>
    /// Класс реализующий логику рaботы игровой карты
    /// </summary>
    internal class Map : Control
    {
        #region Private members

        private char _mapWalkableCell = '.';
        private char _mapBorder = '#';

        private Cell[] _mapBody;
        private Cell[] _mapBodyClean;

        private int _mapBufferWidth = 75;
        private int _mapBufferHeight = 40;
        Dungeon dungeon;
        private Vector2D _mapBufferOffset;

        
        private Stairs downStairs;
        
        private Int32 _numberOfFloor = 0;
        #endregion

        #region Public Properties
        /// <summary>
        /// Игрок
        /// </summary>
        public Player Player { get; internal set; }

        ///<summary>
        /// Актеры, которые находятся на карте
        ///</summary>
        public List<Actors.Actor> Actors { get; set; }
        #endregion

        #region Contructors

        public Map(int mapWidth, int mapHeight, Graphic.Core.Vector2D _mapLocation)
            : this(mapWidth, mapHeight, _mapLocation, null, new List<Actor>())
        {
        }

        public Map(
                int mapWidth,
                int mapHeight,
                Graphic.Core.Vector2D _mapLocation,
                Player player,
                IEnumerable<Actor> actors)
        {
            Width = mapWidth;
            Height = mapHeight;
            Location = _mapLocation;
            Player = player;
            Actors = actors.ToList();


            _mapBodyClean = MapBufferInit(Width, Height);
            foreach (var cell in _mapBodyClean)
                cell.Symbol = ' ';
            GenerateUndDrawDungeon();
        }
        #endregion

        #region Protected Methods
        protected override Cell[] InitBody(int width, int height)
        {
            Cell[] temp = base.InitBody(width, height);
            //Cell[] temp = GenerateDungeon(Width, Height, Location);

            //temp = DrawLeftRightWalls(temp, Width, Height, '|');
            //temp = DrawUpDownWalls(temp, Width, Height, '-');
            //temp = DrawCornel(temp, Width, Height, '+');

            return temp;
        }
        #endregion

        #region Public Methods

        public void AddActorToMap(Actor actor)
        {
            if (actor is Player player)
            {
                player.Position += dungeon.Rooms.FirstOrDefault().GetCenter() + Location;
                Player = player;
                Actors.Add(Player);
            }
            else
            {
                actor.Position -= Location;
                Actors.Add(actor);
            }
        }

        public void SetSymbol(int x, int y, char symbol)
        {
            _mapBody[x + _mapBufferWidth * y].Symbol = symbol;
        }

        public void SetSymbolWithColor(int x, int y, char symbol, Color color)
        {
            _mapBody[x + _mapBufferWidth * y].Symbol = symbol;
            _mapBody[x + _mapBufferWidth * y].Color = color;
        }

        public void AddRangeOfActorToMap(IEnumerable<Actor> actors)
        {
            foreach (Actor actor in actors)
                AddActorToMap(actor);
        }

        /// <summary>
        /// Метод проверки ячейки на, то что туда можно пройти
        /// </summary>
        /// <param name="x">Х</param>
        /// <param name="y">У</param>
        /// <returns>True - если на ячейку можно пройти, False - если нельзя</returns>
        public bool IsWalkable(Int32 x, Int32 y)
        {
            bool flag = false;
            foreach (Cell cell in _mapBody)
            {
                if (cell.Position.X.Equals(x) && cell.Position.Y.Equals(y))
                {
                    if (cell.Symbol.Equals('.') || cell.Symbol.Equals('<')) 
                        flag = true;
                }
            }
            /*
            foreach (Actor actor in Actors)
            {
                if (actor.Position.X.Equals(x) && actor.Position.Y.Equals(y))
                {
                    flag = false;
                }
            }*/
            return flag;
        }

        /// <summary>
        /// Метод проверки ячейки на, то что туда можно пройти
        /// </summary>
        /// <param name="vec">Позиция точки проверки</param>
        /// <returns>True - если на ячейку можно пройти, False - если нельзя</returns>
        public bool IsWalkable(Vector2D vec) => IsWalkable(vec.X, vec.Y);

        public Cell GetCell(Int32 x, Int32 y) => _mapBody.FirstOrDefault(cell => cell.Position.X == x && cell.Position.Y == y);
        public Cell GetCell(Vector2D pos) => GetCell(pos.X, pos.Y);
        public Actor GetActor(Int32 x, Int32 y) => Actors.FirstOrDefault(actor => actor.Position.X == x && actor.Position.Y == y);
        public Actor GetActor(Vector2D pos) => GetActor(pos.X, pos.Y);

        public void GoDown()
        {
            if (Player.Position == downStairs.Position)
            {
                Console.Title ="Yay"; 
                GenerateUndDrawDungeon();
            }
            else 
                Console.Title = $"Player position is {Player.Position}; Stairs Position is {downStairs.Position}";
        }

        public void Update()
        {
            foreach (var cell in _mapBody)
            {
                cell.Position = cell.Position + _mapBufferOffset;
            }
            downStairs.Position += _mapBufferOffset;
            //if (Player != null)
            Player.Position += _mapBufferOffset;

            Actors.RemoveAll(actor => actor.IsDead);
            foreach (var actor in Actors)
            {
                actor.Position = actor.Position + _mapBufferOffset;
            }

            _mapBufferOffset = new Vector2D(0, 0);
        }
         
        public void PlayerMoveTo(Vector2D vec)
        {
            Vector2D offset = new Vector2D(vec.X * -1, vec.Y * -1);
            _mapBufferOffset = offset;
        }

        public async override void Draw()
        {
            foreach (var cell in from cell in _mapBody.AsParallel()
                                 where cell.Position.X > 0 && cell.Position.Y > 0 && cell.Position.X < Width - 1 && cell.Position.Y < Height - 1
                                 select cell)
            {
                Render.WithOffset(cell, 0, 0);
            }
            //if (Player != null)
            Render.WithOffset(Player, 0, 0);

            foreach (var actor in Actors)
            {
                if (actor.Position.X > 0 && actor.Position.Y > 0 && actor.Position.X < Width - 1 && actor.Position.Y < Height - 1)
                    Render.WithOffset(actor, 0, 0);
            }
        }
        #endregion

        #region Private Methods

        private void GenerateUndDrawDungeon()
        {
            _mapBody = MapBufferInit(_mapBufferWidth, _mapBufferHeight);
            Actors = new List<Actor>();
            dungeon = new Dungeon(_mapBufferWidth, _mapBufferHeight, Location) 
            {
                MaxRoomHeight = 10,
                MinRoomHeight = 7,
                MaxRoomWidth = 10,
                MinRoomWidth = 7,
                CountOfRooms = 18
            };
            AbstractFactory factory = new DefaultDungeonFactory(); 
            if (_numberOfFloor > 1)
                factory = new FireDungeonFactory();
            dungeon.Generate(factory).AsParallel().ForAll(cell => SetSymbolWithColor(cell.Position.X, cell.Position.Y, cell.Symbol, cell.Color));
            var offset = dungeon.Rooms.First().GetCenter() - new Vector2D(Width >> 1, Height >> 1) + Location;

            var downStair_pos = dungeon.Rooms.Last().GetCenter();
            
            downStairs = new Stairs(
                    '<',
                    downStair_pos,
                    ColorManager.White,
                    ColorManager.Black);

            SetSymbol(downStair_pos.X, downStair_pos.Y, downStairs.Symbol); 
            _numberOfFloor++;
            foreach (var room in dungeon.Rooms)
                foreach (var actor in room.Actors)
                {
                    actor.Position = actor.Position - offset;
                    AddActorToMap(actor);
                }

            downStairs.Position -= offset;
            foreach (var cell in _mapBody)
            {
                cell.Position = cell.Position - offset;
            }
            Console.Title = $"Floor {_numberOfFloor}";
        }


        private Cell[] MapBufferInit(Int32 mapWidth, Int32 mapHeight)
        {
            Cell[] temp = new Cell[mapWidth * mapHeight];

            for (Int32 x = 0; x < mapWidth; x++)
                for (Int32 y = 0; y < mapHeight; y++)
                {
                    temp[x + mapWidth * y]
                        = new Cell(
                                _mapWalkableCell,
                                new Vector2D(x, y) + Location,
                                ColorManager.White,
                                ColorManager.Black);
                }

            //temp = DrawBordersWithSymbol(temp, mapWidth, mapHeight, _mapBorder);
            return temp;
        }
        #endregion
    }
}
