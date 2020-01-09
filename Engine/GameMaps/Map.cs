using RougeLikeRPG.Core;
using RougeLikeRPG.Core.Controls;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Actors.Enums;
using RougeLikeRPG.Engine.GameMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        ///<summary>
        ///Буффер, в котором находится всей картa
        ///</summary>
        private Cell[] _mapBuffer;

        private Int32 _mapBufferHeight;
        private Int32 _mapBufferWidth;

        private Int32 _mapBufferSize;

        private Vector2D _mapBufferOffset;
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

        public Map(int mapWidth, int mapHeight, Core.Vector2D _mapLocation)
            : this(mapWidth, mapHeight, _mapLocation, null, null)
        {
        }

        public Map(
                int mapWidth,
                int mapHeight,
                Core.Vector2D _mapLocation,
                Player player,
                IEnumerable<Actor> actors)
        {
            Width = mapWidth;
            Height = mapHeight;
            Location = _mapLocation;
            Player = player;
            
            body = InitBody(Width, Height);

            _mapBufferWidth = Width;
            _mapBufferHeight = 25;
            _mapBufferSize = _mapBufferWidth * _mapBufferHeight;
            var map = new MapCreator(_mapBufferWidth, _mapBufferHeight).EmptyMap;
            _mapBody = _mapBuffer = Tile.ToCellsArray(map.Body);
            
            if (player != null)
                AddActorToMap(player);
        }
        #endregion

        #region Protected Methods
        protected override Cell[] InitBody(int width, int height)
        {
            Cell[] temp = base.InitBody(width, height);

            temp = DrawLeftRightWalls(temp, Width, Height, '|');
            temp = DrawUpDownWalls(temp, Width, Height, '-');
            temp = DrawCornel(temp, Width, Height, '+');
            return temp;
        }

        #endregion

        #region Public Methods

        public void AddActorToMap(Actor actor)
        {
            if (actor is Player)
            {
                actor.Position += new Vector2D(Width / 2, Height / 2) + Location;
                Player = actor as Player;
            }
            else
                Actors.Add(actor);
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
            Cell cell = _mapBuffer[x + _mapBufferWidth * y];
            return cell != null || cell.Symbol == _mapWalkableCell;
        }

        /// <summary>
        /// Метод проверки ячейки на, то что туда можно пройти
        /// </summary>
        /// <param name="vec">Позиция точки проверки</param>
        /// <returns>True - если на ячейку можно пройти, False - если нельзя</returns>
        public bool IsWalkable(Vector2D vec) => IsWalkable(vec.X, vec.Y);

        public Cell GetCell(Int32 x, Int32 y) => _mapBuffer.FirstOrDefault(cell => cell.Position.X == x && cell.Position.Y == y);
        public Cell GetCell(Vector2D pos) => GetCell(pos.X, pos.Y);
        public void Update()
        {
            if (_mapBuffer != null)
                for (int i = 0; i < _mapBufferSize; i++)
                {
                    Cell cell = _mapBuffer[i];
                    if (cell != null)
                    {
                        if (IsWalkable(Player.Position + _mapBufferOffset))
                            cell.Position += _mapBufferOffset;
                    }
                }
        }

        public void PlayerMoveTo(Vector2D vec)
        {
            Vector2D offset = new Vector2D(vec.X * -1, vec.Y * -1);
            _mapBufferOffset = offset;
        }


        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);

            foreach (Cell cell in _mapBuffer)
            {
                if (cell.Position.X > 0 && cell.Position.Y > 0)
                    if (cell.Position.X < Width - 1
                            && cell.Position.Y < Height - 1)
                        Render.WithOffset(cell, 0, 0);
            }

            //Отрисовка игрока игрокаока игрокагрокаока игрока
            if (Player != null)
                Render.WithOffset(Player, 0, 0);

            //Отрисовка других актеровгих актеров
            if (Actors != null)
                foreach (Actor actor in Actors)
                    Render.WithOffset(actor, 0, 0);
        }
        #endregion

        #region Private Methods 
        private Cell[] MapBufferInit(Int32 mapWidth, Int32 mapHeight)
        {
            Cell[] temp = new Cell[mapWidth * mapHeight];

            for (Int32 x = 0; x < mapWidth; x++)
                for (Int32 y = 0; y < mapHeight; y++)
                {
                    temp[x + mapWidth * y]
                        = new Cell(
                                _mapWalkableCell,
                                new Vector2D(x, y) + Location + 1,
                                ConsoleColor.White,
                                ConsoleColor.Black);
                }

            temp = DrawBordersWithSymbol(temp, mapWidth, mapHeight, _mapBorder);
            return temp;
        }
        #endregion
    }
}
