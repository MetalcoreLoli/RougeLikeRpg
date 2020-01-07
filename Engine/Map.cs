using RougeLikeRPG.Core;
using RougeLikeRPG.Core.Controls;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Actors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine
{
    /// <summary>
    /// Класс реализующий игровую карту
    /// </summary>
    internal class Map : Control
    {
        #region Private members

        private char _mapCell = '.';

        private Cell[] _mapBody;
        ///<summary>
        ///Буффер, в котором находится всей картa
        ///</summary>
        private Cell[] _mapBuffer;

        private Int32 _mapBufferHeight;
        private Int32 _mapBufferWidth;

        private Int32 _mapBufferSize;
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
            _mapBuffer = MapBufferInit(_mapBufferWidth, _mapBufferHeight);
            //_mapBody    = new Cell[] 

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


        public bool IsWalkable(Int32 x, Int32 y)
        {
            foreach (Cell cell in _mapBuffer)
                if (cell != null)
                    if (cell.Position.X == x && cell.Position.Y == y)
                        if (cell.Symbol == _mapCell)
                            return true;
            return false;
        }

        public void Update()
        {
            if (_mapBuffer != null)
                for (int i = 0; i < _mapBufferSize; i++)
                {
                    Cell cell = _mapBuffer[i];
                    if (cell != null)
                    {
                        if (Player.Direction == Direction.Up)
                        {
                            if (Player.Position.Y + 1 > cell.Position.Y + 1)
                            cell.Position = new Vector2D(cell.Position.X, cell.Position.Y + 1);
                        }
                        if (Player.Direction == Direction.Down)
                        {
                            if (Player.Position.Y < _mapBufferHeight)
                            cell.Position = new Vector2D(cell.Position.X, cell.Position.Y - 1);
                            //Player.MoveTo(new Vector2D(0, -1));
                        }

                        if (Player.Direction == Direction.Left)
                        {
                            cell.Position = new Vector2D(cell.Position.X + 1, cell.Position.Y);
                        }
                        if (Player.Direction == Direction.Right)
                        {
                            cell.Position = new Vector2D(cell.Position.X - 1, cell.Position.Y);
                        }

                    }
                }
        }

        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);

            foreach (Cell cell in _mapBuffer)
            {
                if (cell != null)
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

            for (Int32 x = 1; x < mapWidth; x++)
                for (Int32 y = 1; y < mapHeight; y++)
                {
                    temp[x + mapWidth * y]
                        = new Cell(
                                _mapCell,
                                new Vector2D(x, y) + Location,
                                ConsoleColor.White,
                                ConsoleColor.Black);
                }

            return temp;
        }
        #endregion
    }
}
