using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Engine.Actors;
using RougeLikeRpg.Engine.GameMaps.Dungeon;
using RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonFactory;
using RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonConfiguration;

using System;
using System.Collections.Generic;
using System.Linq;
using RougeLikeRpg.Graphic.Controls;

namespace RougeLikeRpg.Engine
{
    /// <summary>
    /// Класс реализующий логику рaботы игровой карты
    /// </summary>
    internal class Map : Control
    {
        #region Private members

        private const char _mapWalkableCell = '.';
        private const char _mapBorder = '#';

        /// <summary>
        /// Map that player see
        /// </summary>
        private Cell[] _mapBuffer;

        private const int _mapBufferWidth = 75;
        private const int _mapBufferHeight = 40;
        
        private Vector2D _mapBufferOffset;

        private Dungeon _dungeon;
        
        private Stairs _downStairs;
        
        private Int32 _numberOfFloor = 0;

        private IDungeonConfiguration _dungeonConfiguration;
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

        public Int32 CurrentFloor => _numberOfFloor;
        #endregion

        #region Contructors

        public Map(IControlConfiguration configuration)
            : this(configuration, null, new List<Actor>())
        {
        }

        public Map(
                IControlConfiguration configuration,
                Player player,
                IEnumerable<Actor> actors)
        {
            m_configuration = configuration;
            _dungeonConfiguration = new DefaultDungeonConfiguration(
                _mapBufferWidth, _mapBufferHeight, 18, Location, 7,  5, 7, 5);
            ApplyConfiguration();
            Player = player;
            Actors = actors.ToList();
            GenerateDungeon();
        }
        #endregion

        #region Protected Methods
        protected override Cell[] InitBody(int width, int height)
        {
            Cell[] temp = base.InitBody(width, height);
            return temp;
        }
        #endregion

        #region Public Methods

        public Cell CellFromWorldPosition(Vector2D worldPosition)
        {
            float percentX = (worldPosition.X + _mapBufferWidth / 2) / _mapBufferWidth;
            float percentY = (worldPosition.Y + _mapBufferHeight / 2) / _mapBufferHeight;
            percentX = Math.Clamp(percentX, 0.0f, 1.0f);
            percentY = Math.Clamp(percentY, 0.0f, 1.0f);
            
            int x = (int)Math.Round((_mapBufferWidth - 1) * percentX);
            int y = (int)Math.Round((_mapBufferHeight - 1) * percentY);

            return _mapBuffer[x + y * Width];
        }

        public void AddActorToMap(Actor actor)
        {
               Actors.Add(actor);
        }

        public void SetSymbol(int x, int y, char symbol)
        {
            _mapBuffer[x + _mapBufferWidth * y].Symbol = symbol;
        }

        public void SetSymbolWithColor(int x, int y, char symbol, Color color)
        {
            _mapBuffer[x + _mapBufferWidth * y].Symbol = symbol;
            _mapBuffer[x + _mapBufferWidth * y].Color = color;
        }

        internal void Rebuild()
        {
            body = InitBody(Width, Height);
            
            GenerateDungeon();
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
            return (from cell in _mapBuffer where cell.Position.X.Equals(x) && cell.Position.Y.Equals(y) select cell.Symbol.Equals('.') || cell.Symbol.Equals('<')).FirstOrDefault();
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

        public void Move (Vector2D offset)
        {
            foreach (var cell in _mapBuffer)
                cell.Position -= offset;

            FillBodyWithDrawableCells(_mapBuffer, Width, Height);
        }
        
        public async override void Draw()
        {
            //foreach (var cell in from cell in _mapBuffer.AsParallel()
            //                     where cell.Position.X > 0 && cell.Position.Y > 0 && cell.Position.X < Width - 1 && cell.Position.Y < Height - 1
            //                     select cell)
            //{
            //    Render.WithOffset(cell, 0, 0);
            //}
            ////if (Player != null)
            //Render.WithOffset(Player, 0, 0);

            //foreach (var actor in Actors)
            //{
            //    if (actor.Position.X > 0 && actor.Position.Y > 0 && actor.Position.X < Width - 1 && actor.Position.Y < Height - 1)
            //        Render.WithOffset(actor, 0, 0);
            //}
        }
        #endregion

        #region Private Methods
        private void GenerateDungeon()
        {
            Actors = new List<Actor>();
            
            _dungeon = new Dungeon (new TestDungeonConfiguration(Location)); // only for tests
            //_dungeon = new Dungeon (_dungeonConfiguration);

            AbstractDungeonFactory factory = new DefaultDungeonFactory(); 
            //if (_numberOfFloor > 1)
            //    factory = new FireDungeonFactory();

            _mapBuffer = _dungeon.Generate(factory);

            FillBodyWithDrawableCells(_mapBuffer, Width, Height);
            _numberOfFloor++;
        }

        private void FillBodyWithDrawableCells(Cell[] source, int width, int height)
        {
            Cell[] drawableCells = (from cell in source
                                    where cell.Position.X > 1 && cell.Position.Y > 1 
                                    where cell.Position.X < width-1 && cell.Position.Y < height-1
                                    select cell).ToArray();

            for (int i = 0; i < drawableCells.Length; i++)
            {
                body[i] = drawableCells[i];
            }
        }
        #endregion
    }
}
