using RougeLikeRPG.Core;
using RougeLikeRPG.Engine.Actors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RougeLikeRPG.Engine
{
    /// <summary>
    /// Класс раелизюущий игровую логику
    /// </summary>
    internal class Game
    {
        #region Private Members
        /// <summary>
        /// Игрок
        /// </summary>
        private IActor _player;
        
        /// <summary>
        /// Высота карты
        /// </summary>
        private Int32 _mapHeight = 15;
        /// <summary>
        /// Ширина Карты
        /// </summary>
        private Int32 _mapWidth = 30;

        /// <summary>
        /// Расположени карты на экране
        /// </summary>
        private Vector2D _mapLocation = new Vector2D(1, 1);

        /// <summary>
        /// Карта
        /// </summary>
        private Map _map;
        #endregion

        #region Constructors
        /// <summary>
        ///  Коструктор по умолчанию
        /// </summary>
        public Game()
        {
            _map = new Map(_mapWidth, _mapHeight, _mapLocation);
        }
        #endregion

        #region Public Methods
        
        /// <summary>
        /// Метод, который содержит игровой цикл
        /// </summary>
        public void Start()
        {
            do
            {
                Draw();
                Thread.Sleep(1000);
            } while (true);
        }
        #endregion

        #region Private Methods

        /// <summary>
        ///  Метод, в который занимается отрисовка интрефейса
        /// </summary>
        private void Draw()
        {
            Console.Clear();
            _map.Draw();
        }
        #endregion
    }
}