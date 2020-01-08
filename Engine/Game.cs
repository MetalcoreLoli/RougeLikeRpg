using RougeLikeRPG.Core;
using RougeLikeRPG.Engine.Dices;
using RougeLikeRPG.Core.Controls;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Actors.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RougeLikeRPG.Engine.GameScreens;

namespace RougeLikeRPG.Engine
{
    /// <summary>
    /// Класс реaлизущий игровую логику
    /// </summary>
    internal class Game
    {
        #region Private Members
        /// <summary>
        /// Игрок
        /// </summary>
        private Player _player;

        /// <summary>
        /// Высота карты
        /// </summary>
        private Int32 _mapHeight = 11;
        /// <summary>
        /// Ширина Карты
        /// </summary>
        private Int32 _mapWidth = 30;

        /// <summary>
        /// Расположени карты на экране
        /// </summary>
        private Vector2D _mapLocation;

        /// <summary>
        /// Карта
        /// </summary>
        private Map _map;

        ///<summary>
        /// Высота экрана инвенторя
        ///</summary>
        private Int32 _invetoryScreenHeight = 10;

        ///<summary>
        /// Ширина экрана инвенторя
        ///</summary>
        private Int32 _invetoryScreenWidth = 50;

        ///<summary>
        ///расположение экрана инвенторя 
        ///</summary>
        private Vector2D _invetoryScreenLocation;

        ///<summary>
        ///Экран инвенторя
        ///</summary>
        private Screen _inventoryScreen;


        private Int32 _statusScreenHeight;
        private Int32 _statusScreenWidth;

        private Vector2D _statusScreenLocation;

        private Screen _statusScreen;


        private Int32 _messageLogScreenHeight;
        private Int32 _messageLogScreenWidth;

        private Vector2D _messageLogScreenLocation;

        private Screen _messageLogScreen;

        //Экран создания персонажа
        private CreatePlayerScreen _createPlayerScreen;
        #endregion

        #region Constructors
        /// <summary>
        ///  Коструктор по умолчанию
        /// </summary>
        public Game()
        {
            _createPlayerScreen = new CreatePlayerScreen();
            _player = new Player();
            _player = NewPlayer();
            Initialization();
            
           _map.AddActorToMap(_player);
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
                Update();
                //Thread.Sleep(1000);
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
            _messageLogScreen.Draw();
            // _inventoryScreen.Draw();
            _statusScreen.Draw();
        }

        private Vector2D PlayerMoveTo()
        {
            Vector2D vec = new Vector2D();
            switch(Input.PlayerKeyInput().Result)
            {
                case ConsoleKey.UpArrow: 
                    _map.Player.Direction = Direction.Up;
                    vec = new Vector2D(0, -1); 
                    break;
                
                case ConsoleKey.DownArrow: 
                    _map.Player.Direction = Direction.Down;
                    vec = new Vector2D(0, 1); 
                    break;
                
                case ConsoleKey.LeftArrow: 
                    _map.Player.Direction = Direction.Left;
                    vec = new Vector2D(-1, 0); 
                    break;
                
                case ConsoleKey.RightArrow: 
                    _map.Player.Direction = Direction.Right;
                    vec = new Vector2D(1, 0); 
                    break;
                
                default: 
                    vec = new Vector2D(0, 0); 
                    break;
            }
            return vec;
        }
        private void Update()
        {
           // Vector2D playersInput = Input.PlayerInput().Result;
            Vector2D playersInput = PlayerMoveTo();

            if ((playersInput + _map.Player.Position).X == _map.Width - 1)
                playersInput.X--;

            if ((playersInput + _map.Player.Position).Y == _map.Height- 1)
                playersInput.Y--;

            if((playersInput + _map.Player.Position).X == 0)
                playersInput.X++;

            if((playersInput + _map.Player.Position).Y == 0)
                playersInput.Y++;

            Console.Title = _map.Player.Direction.ToString();
            //_map.Player.MoveTo(playersInput);
            _map.PlayerMoveTo(playersInput);
            _map.Update();
        }

        /// <summary>
        /// Ввод 
        /// </summary>
        /// <returns></returns>
        private Player NewPlayer() => _createPlayerScreen.Start();

      
        private void Initialization()
        {
            _statusScreenWidth          = 25;
            _statusScreenHeight         = _invetoryScreenHeight + _mapHeight;

            _mapWidth += _statusScreenWidth;

            _messageLogScreenWidth      = /*_statusScreenWidth + */_mapWidth;
            _messageLogScreenHeight     = _invetoryScreenHeight;

            _statusScreenLocation       = new Vector2D(_mapWidth, 0);
            _invetoryScreenLocation     = new Vector2D(0, 0);
            _mapLocation                = new Vector2D(0, 0);
            _messageLogScreenLocation   = new Vector2D(
                                                        0,
                                                        _mapHeight);
            
            _messageLogScreen       = new Screen(
                                                _messageLogScreenWidth,
                                                _messageLogScreenHeight,
                                                _messageLogScreenLocation,
                                                "Message Log",
                                                ConsoleColor.DarkCyan,
                                                ConsoleColor.Black);

            _statusScreen           = new Screen(
                                                 _statusScreenWidth, 
                                                 _statusScreenHeight,
                                                 _statusScreenLocation);
           
           
            _inventoryScreen        = new Screen(
                                                _invetoryScreenWidth, 
                                                _invetoryScreenHeight,
                                                _invetoryScreenLocation,
                                                "",
                                                ConsoleColor.DarkYellow,
                                                ConsoleColor.Black); 
           
            _map                    = new Map(_mapWidth, _mapHeight, _mapLocation);
            

            string title = "Status";
            _statusScreen.TitleLocation = new Vector2D(
                    (_statusScreen.Width - title.Length - 1) / 4, 2);
            _statusScreen.Title = title; 
            _statusScreen.AddRange(_player.GetStats().ToList());

            _messageLogScreen.Title = "Message Log";

        }
        #endregion
    }
}
