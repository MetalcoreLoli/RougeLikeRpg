using RougeLikeRPG.Graphic.Core;
using RougeLikeRPG.Engine.Dices;
using RougeLikeRPG.Graphic.Core.Controls;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Actors.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RougeLikeRPG.Engine.GameScreens;
using RougeLikeRPG.Engine.Events;
using RougeLikeRPG.Engine.Actors.Monsters;
using RougeLikeRPG.Engine.GameItems.Items;
using RougeLikeRPG.Engine.Core;

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

        private LevelUpMenuScreen _levelUpMenuScreen;
        #endregion

        #region Events

        public event EventHandler<KeyDownEventArgs> KeyDown;
        #endregion

        #region Constructors
        /// <summary>
        ///  Коструктор по умолчанию
        /// </summary>
        public Game()
        {
            _createPlayerScreen = new CreatePlayerScreen();
           
            Initialization();
            KeyDown += Game_KeyDown;

            _player.Position    += new Vector2D(_map.Width / 2, _map.Height / 2) + _map.Location;
            _player.LevelUp     += Player_LevelUp;
            _player.Attacking   += Player_Attacking;
            _player.Dying       += Actor_Dying;
            _player.Moving      += Player_Moving;

            _map.Player = _player;


            foreach (var actor in _map.Actors)
            {
                actor.Attacking += Hit_Attacking;
                actor.Dying += Actor_Dying;
                actor.Moving += Actor_Moving;
            }
        }

        private void Actor_Moving(object sender, Actors.Events.MovingEventArgs e)
        {
            Actor actor = sender as Actor;
            //actor.IsMoving = true;
            if (!(_map.IsWalkable(actor)))
            {
                actor.Position -= e.MovingPosition;
                if (actor is Monster)
                {
                    var mon = (actor as Monster);
                    if (mon.IsActorInFov(_player))
                        mon.Attack(_player);
                }
                actor.IsMoving = false;
            }
        }

        private void Player_Moving(object sender, Actors.Events.MovingEventArgs e)
        {
            Player pl = sender as Player;
            if (_map.IsWalkable(pl.Position))
                _map.PlayerMoveTo(e.MovingPosition);
            else
            {
                pl.Position -= e.MovingPosition;
                pl.IsMoving = false;
                foreach (Actor actor in _map.Actors)
                {
                    if (actor is Monster)
                    {
                        var mon = (actor as Monster);
                        _player.IfDo(mon,
                            (player, monster) => (player.Position + e.MovingPosition).Equals(monster.Position),
                            (player, monster) => player.Attack(monster));
                    }
                }
            }
        }

        private void Hit_Attacking(object sender, Actors.Events.AttackingEventArgs e)
        {
            Actor actor = sender as Actor;
            if (!e.IsMissed)
                HitMessages(actor, e.Enemy, e.Weapon);
            else MissMessages(actor, e.Weapon);
        }

        private void Actor_Dying(object sender, Actors.Events.ActorDyingEventArgs e)
        {
            (_messageLogScreen as MessageLogScreen).Add(e.Name + $" was killed by {((sender as Actor).Name)} and drop exp: {e.DropExp}");
            SetColorsToText();
        }

        private void Player_Attacking(object sender, Actors.Events.AttackingEventArgs e)
        {
            Player actor = sender as Player;
            if (!e.IsMissed)
                HitMessages(actor, e.Enemy, e.Weapon);
            else MissMessages(actor, e.Weapon);

        }

        private void Player_LevelUp(object sender, Actors.Events.LevelUpEventArgs e)
        {
            string levelUpMessage = $"+Level of {e.Actor.Name} was Upped+  ";
            (_messageLogScreen as MessageLogScreen).Add(levelUpMessage); 
            var line = ((_messageLogScreen as MessageLogScreen).Items.Last() as Lable);
            line.SetColorToWord(levelUpMessage, ConsoleColor.Yellow);
            SetColorsToText();
        }

        private void Game_KeyDown(object sender, KeyDownEventArgs e)
        {
            Vector2D playersInput = PlayerMoveTo(e.Key);
            Console.Title = _map.Player.Direction.ToString();
            PlayerCastSpells(e.Key);
            PlayerMove(playersInput);
        }

        private void HitMessages(Actor actor, Actor enemy, WeaponItem weapon)
        {
            string messsage =  $"{enemy.Name} was hitten by {actor.Name} with {weapon.Name} at {weapon.RolledDamage}";
            (_messageLogScreen as MessageLogScreen).Add(messsage);
            SetColorsToText();
        }

        private void MissMessages(Actor actor, WeaponItem weapon)
        {
            string messsage = $"{actor.Name} is miss by {weapon.Name} ";
            (_messageLogScreen as MessageLogScreen).Add(messsage);
            SetColorsToText();
        }
        
        private void PlayerCastSpells(ConsoleKey key)
        {
            switch(key)
            {
                case ConsoleKey.Q: 
                    new Magick.HealingSpell(_player).Cast();
                    break;
            }
        }
        
        private void PlayerMove(Vector2D playersInput)
        {
            _player.MoveTo(playersInput);
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Метод, который содержит игровой цикл
        /// </summary>
        public void Start()
        {
            Console.Clear();
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
            //Thread.Sleep(250);
            //Console.Clear();
            _map.Draw();
            _messageLogScreen.Draw();
            _statusScreen.Draw();
        }


        private Vector2D PlayerMoveTo(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _map.Player.Direction = Direction.Up;
                    return Actor.MoveDirectionVector(Direction.Up);

                case ConsoleKey.DownArrow:
                    _map.Player.Direction = Direction.Down;
                    return Actor.MoveDirectionVector(Direction.Down);

                case ConsoleKey.LeftArrow:
                    _map.Player.Direction = Direction.Left;
                    return Actor.MoveDirectionVector(Direction.Left);

                case ConsoleKey.RightArrow:
                    _map.Player.Direction = Direction.Right;
                    return Actor.MoveDirectionVector(Direction.Right);

                default:
                    return new Vector2D(0, 0);
            }
        }

        private async void PlayerInput()
        {
            OnKeyDown(Input.PlayerKeyInput().Result);
        }
        
        private void Update()
        {
            if (_player.Hp > 0)
                PlayerInput();
            else
                _player.Color = ConsoleColor.DarkGray;

            SetColorsToText();
             
            MonstersMove();
            _map.Update();
            
            _statusScreen.Items = new List<Control>();
            _statusScreen.AddRange(_player.GetStats());
        }
        private void MonstersMove()
        {
            _map.Actors.ForEach(actor =>
            {
                if (actor is Monster)
                {
                    var mon = actor as Monster;
                    Direction moveDir = Direction.None;
                    if (mon.IsActorInFovXY(_player, 5, 5, out moveDir))
                        mon.MoveTo(Actor.MoveDirectionVector(moveDir));
                    else
                        mon.MoveTo(Actor.MoveRandomDirectionVector());
                }
            });
        }

        private void SetColorsToText()
        {
            foreach (Lable line in from line in _messageLogScreen.Items
                                 where line is Lable
                                 select line)
            {
                foreach (var word in KeyWords.Words)
                    line.SetColorToWord(word.Key, word.Value);
                
                line.SetColorToPrase($"+Level of {_player.Name} Upped+", ConsoleColor.DarkYellow);
                line.SetColorToWord(_player.Name, ConsoleColor.DarkGray);

                if (_player.LeftArm != null)
                    SetColorToItemInText(_player.LeftArm, line);
                if (_player.RightArm != null)
                    SetColorToItemInText(_player.RightArm, line);
                foreach (Actor actor in _map.Actors)
                {
                    line.SetColorToWord(actor.Name, actor.Color);
                    if (actor.LeftArm != null)
                        SetColorToItemInText(actor.LeftArm, line);

                    if (actor.RightArm != null)
                        SetColorToItemInText(actor.RightArm, line);
                }
            }
        }

        private void SetColorToItemInText(WeaponItem weapon, Lable line)
        {
            if (weapon.Name.Split(' ').Count() > 1)
            {
                var name = weapon.Name.Split(' ');
                foreach (var word in name)
                    line.SetColorToWord(word, (ConsoleColor)weapon.Rare.Color);
            }
            else
                line.SetColorToWord(weapon.Name, (ConsoleColor)weapon.Rare.Color);
        }

        /// <summary>
        /// Ввод 
        /// </summary>
        /// <returns></returns>
        private Player NewPlayer() => _createPlayerScreen.Start();

        private void OnKeyDown(ConsoleKey key) => KeyDown?.Invoke(this, new KeyDownEventArgs(key));

        private void Initialization()
        {
            _player = new Player();
            _player = NewPlayer();

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
            
            _messageLogScreen       = new MessageLogScreen(
                                                _messageLogScreenWidth,
                                                _messageLogScreenHeight,
                                                _messageLogScreenLocation,
                                                "Message Log",
                                                ConsoleColor.DarkCyan,
                                                ConsoleColor.Black);

            _levelUpMenuScreen = new LevelUpMenuScreen(
                                               50,
                                               25,
                                               new Vector2D(0, 0),
                                               "Level Up Menu",
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
