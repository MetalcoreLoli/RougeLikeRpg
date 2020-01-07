using RougeLikeRPG.Core;
using RougeLikeRPG.Core.Controls;
using RougeLikeRPG.Engine.Actors;
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

        ///<summary>
        ///Буффер, в котором находится вся карта
        ///</summary>
        private Cell[] _mapBuffer;
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
            :this(mapWidth, mapHeight, _mapLocation, null, null)
        {
        }

        public Map(int mapWidth, int mapHeight, Core.Vector2D _mapLocation, Player player, IEnumerable<Actor> actors)
        {
            Width       = mapWidth;
            Height      = mapHeight;
            Location    = _mapLocation;
            body        = InitBody(Width, Height);
            if (player != null)
                AddActorToMap(player);
        }
        #endregion

        #region Protected Methods
        protected override Cell[] InitBody(int width, int height)
        {
            Cell[] temp = base.InitBody(width, height);
            
            for (int x = 1; x < width - 1; x++)
                for (int y = 1; y < height - 1; y++)
                    temp[x + width * y].Symbol = _mapCell;
            
            temp = DrawLeftRightWalls(temp, Width, Height, '|');
            temp = DrawUpDownWalls(temp, Width, Height, '-');
            temp = DrawCornel(temp, Width, Height, '+');
            return temp;
        }

        #endregion

        #region Public Methods

        public void AddActorToMap(Actor actor)
        { 
           if(actor is Player)
           {
                actor.Position += Location + 1;
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

        ///<summary>
        /// Метод, обновляющий карту
        ///</summary>
        public void Update()
        {
            
        }

        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);
            
            //Отрисовка игрока
            if (Player != null)
                Render.WithOffset(Player, 0, 0);
            
            //Отрисовка других актеров
            if (Actors != null)
                foreach (Actor actor in Actors)
                    Render.WithOffset(actor, 0, 0);
        }
        #endregion

        #region Private Methods 

        #endregion
    }
}
