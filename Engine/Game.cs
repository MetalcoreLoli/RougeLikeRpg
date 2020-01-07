using RougeLikeRPG.Core;
using RougeLikeRPG.Engine.Dices;
using RougeLikeRPG.Core.Controls;
using RougeLikeRPG.Engine.Actors;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        #endregion

        #region Constructors
        /// <summary>
        ///  Коструктор по умолчанию
        /// </summary>
        public Game()
        {
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

        private void Update()
        {
            _map.Player.MoveTo(PlayerInput().Result);
            //_map.Update();
            ////_inventoryScreen.Update();
            //_statusScreen.Update();
            //_messageLogScreen.Update();
        }

        /// <summary>
        /// Ввод 
        /// </summary>
        /// <returns></returns>
        private async Task<Vector2D> PlayerInput()
        {
            return await Task.Run(() => Console.ReadKey().Key switch
            {
                ConsoleKey.UpArrow => Task<Vector2D>.FromResult(new Vector2D(0, -1)),
                ConsoleKey.DownArrow => Task<Vector2D>.FromResult(new Vector2D(0, 1)),
                ConsoleKey.LeftArrow => Task<Vector2D>.FromResult(new Vector2D(-1, 0)),
                ConsoleKey.RightArrow => Task<Vector2D>.FromResult(new Vector2D(1, 0)),
                _ => Task<Vector2D>.FromResult(new Vector2D(0, 0))
            });
        }

        private Player NewPlayer()
        {
            Player player = new Player();
            player.Name = "Test-chan";
            player.Symbol = '@';
            player.Color = ConsoleColor.White;
            //player.Hp = 17;
            //player.MaxHp = 17;
            player.Hp = player.MaxHp = DiceManager.CreateDices("2d8").RollAll().Sum();
            player.Mana = player.MaxMana = 2;
            player.Exp = 0;
            player.MaxExp = 20;
            player.Level = 1;

            player.Race = Actors.Enums.Race.UndeadDrow;

            player.Str = RollStat();
            player.Dex = RollStat();
            player.Intell = RollStat();
            player.Lucky = RollStat();
            player.Chari = RollStat();
            return player;
        }

        private Int32 RollStat()
        {
            Int32[] values = DiceManager.CreateDices("4d6").RollAll();
            Int32 minDiceValue = values.Min();
            return values.Where(val => val != minDiceValue).Sum();
        }
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
