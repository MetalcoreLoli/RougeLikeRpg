using RougeLikeRPG.Engine.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine
{
    /// <summary>
    /// Класс раелизюущий игровую логику
    /// </summary>
    internal class Game
    {
        #region Private Members

        private IActor _player;

        private Int32 _mapHeight;
        private Int32 _mapWidth;

        private Map _map;
        #endregion

        #region Constructors
        /// <summary>
        ///  Коструктор по умолчанию
        /// </summary>
        public Game()
        {
            _map = new Map(_mapWidth, _mapHeight);
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
