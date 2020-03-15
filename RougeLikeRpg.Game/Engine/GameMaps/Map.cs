﻿using RougeLikeRPG.Graphic.Core;
using RougeLikeRPG.Graphic.Core.Controls;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Actors.Enums;
using RougeLikeRPG.Engine.Actors.Monsters;
using RougeLikeRPG.Engine.GameMaps;
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
        ///<summary>
        ///Буффер, в котором находится всей картa
        ///</summary>
        private Cell[] _mapBuffer;

        private Int32 _mapBufferHeight;
        private Int32 _mapBufferWidth;

        private Int32 _mapBufferSize;

        private Vector2D _mapBufferOffset;

        Dungeon dungeon;
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
            : this(mapWidth, mapHeight, _mapLocation, null, null)
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
            }
            else
            {
                actor.Position += Location;
                Actors.Add(actor);
            }
        }

        public void SetSymbol(int x, int y, char symbol)
        {
            _mapBody[x + Width * y].Symbol = symbol;
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
                    if (cell.Symbol.Equals('.')) flag = true;
                }
            }
            foreach (Actor actor in Actors)
            {
                if (actor.Position.X.Equals(x) && actor.Position.Y.Equals(y))
                {
                    flag = false;
                }
            }
            return flag;
        }
        public bool IsWalkable(Actor actor)
        {
            bool flag = false;
            foreach (Cell cell in _mapBody)
            {
                if (cell.Position.Equals(actor.Position))
                    if (cell.Symbol.Equals('.')) flag = true;
            }
            if (actor.Position.Equals(Player.Position))
                flag = false;
            foreach (Actor actr in Actors)
            {
                if (!actor.GetHashCode().Equals(actr.GetHashCode()))
                    if (actr.Position.Equals(actor.Position))
                        flag = false;
            }
            return flag;
        }
        /// <summary>
        /// Метод проверки ячейки на, то что туда можно пройти
        /// </summary>
        /// <param name="vec">Позиция точки проверки</param>
        /// <returns>True - если на ячейку можно пройти, False - если нельзя</returns>
        public bool IsWalkable(Vector2D vec) => IsWalkable(vec.X, vec.Y);

        public Cell GetCell(Int32 x, Int32 y) => _mapBuffer.FirstOrDefault(cell => cell.Position.X == x && cell.Position.Y == y);
        public Cell GetCell(Vector2D pos) => GetCell(pos.X, pos.Y);
        public Actor GetActor(Int32 x, Int32 y) => Actors.FirstOrDefault(actor => actor.Position.X == x && actor.Position.Y == y);
        public Actor GetActor(Vector2D pos) => GetActor(pos.X, pos.Y);

        public void Update()
        {
            
        }

        public void PlayerMoveTo(Vector2D vec)
        {
            Vector2D offset = new Vector2D(vec.X * -1, vec.Y * -1);
            _mapBufferOffset = offset;
        }

        public async override void Draw()
        {
            
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
                                ColorManager.White,
                                ColorManager.Black);
                }

            temp = DrawBordersWithSymbol(temp, mapWidth, mapHeight, _mapBorder);
            return temp;
        }
        #endregion
    }
}
