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
using RougeLikeRPG.Engine.Events;
using RougeLikeRPG.Engine.Actors.Monsters;
using RougeLikeRPG.Engine.GameItems.Items;

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
            _player = new Player();
            _player = NewPlayer();
            Initialization();
            KeyDown += Game_KeyDown;
            _map.AddActorToMap(_player);

            _player.LevelUp     += _player_LevelUp;
           // _player.Attacking   += _hit_Attacking;
            _player.Dying       += _actor_Dying;
            _player.Moving      += _player_Moving;

            foreach (var actor in _map.Actors)
            {
                actor.Attacking += _hit_Attacking;
                actor.Dying += _actor_Dying;
                actor.Moving += Actor_Moving;
            }
        }

        private void Actor_Moving(object sender, Actors.Events.MovingEventArgs e)
        {
            Actor actor = sender as Actor;
            if (!_map.IsWalkable(actor.Position))
                actor.Position -= e.MovingPosition;

            if (actor is Monster)
            {
                var mon = (actor as Monster);
                //if ((mon.Position + e.MovingPosition).Equals(_player.Position))
                mon.IfDo(_player, 
                    (monster, player) => (monster.Position + e.MovingPosition).Equals(player.Position), 
                    (monster, player) => monster.Attack(player));
            }
        }

        private void _player_Moving(object sender, Actors.Events.MovingEventArgs e)
        {
            Player pl = sender as Player;
            if (_map.IsWalkable(pl.Position))
                _map.PlayerMoveTo(e.MovingPosition);
            else
                pl.Position -= e.MovingPosition;

            foreach (Actor actor in _map.Actors)
            {
                if (actor is Monster)
                {
                    var mon = (actor as Monster);
                    //if ((_player.Position + e.MovingPosition).Equals(mon.Position))
                    //    _player.Attack(mon);
                    _player.IfDo(mon, 
                        (player, monster) => (player.Position + e.MovingPosition).Equals(monster.Position), 
                        (player, monster) => player.Attack(monster));
                }
            }
        }

        private void _hit_Attacking(object sender, Actors.Events.AttackingEventArgs e)
        {
            Actor actor = sender as Actor;
            if (!e.IsMissed)
                HitMessages(actor, e.Enemy);
        }

        private void _actor_Dying(object sender, Actors.Events.ActorDyingEventArgs e)
        {
            (_messageLogScreen as MessageLogScreen).Add(e.Name + $" was killed by {((sender as Actor).Name)} and drop exp: {e.DropExp}");
            SetColorsToText();
        }

        private void _player_Attacking(object sender, Actors.Events.AttackingEventArgs e)
        {
            Player actor = sender as Player;
            if (!e.IsMissed)
                HitMessages(actor, e.Enemy);
        }

        private void _player_LevelUp(object sender, Actors.Events.LevelUpEventArgs e)
        {
            string levelUpMessage = $"+Level of {e.Actor.Name} was Upped+";
            (_messageLogScreen as MessageLogScreen).Add(levelUpMessage);
            var line = ((_messageLogScreen as MessageLogScreen).Items.Last() as Lable);
            line.SetColorToWord(levelUpMessage, ConsoleColor.Yellow);
            SetColorsToText();
            //_levelUpMenuScreen.Show();
        }

        private void Game_KeyDown(object sender, KeyDownEventArgs e)
        {
            //if ((playersInput + _map.Player.Position).X == _map.Width - 1)
            //    playersInput.X--;

            //if ((playersInput + _map.Player.Position).Y == _map.Height - 1)
            //    playersInput.Y--;

            //if ((playersInput + _map.Player.Position).X == 0)
            //    playersInput.X++;

            //if ((playersInput + _map.Player.Position).Y == 0)
            //    playersInput.Y++;

            //_map.Player.MoveTo(playersInput);


            Vector2D playersInput = PlayerMoveTo(e.Key);
            Console.Title = _map.Player.Direction.ToString();

            PlayerMove(playersInput);
        }

        private void HitMessages(Actor actor, Actor enemy)
        {
            string leftArm = "";
            string rightArm = "";

            if (actor.LeftArm != null)
                leftArm = $"{enemy.Name} was hitten by {actor.Name} with {actor.LeftArm.Name} at {actor.LeftArm.RolledDamage}";

            if (actor.RightArm != null)
                rightArm = $"{enemy.Name} was hitten by {actor.Name} with {actor.RightArm.Name} at {actor.RightArm.RolledDamage}";
            (_messageLogScreen as MessageLogScreen).Add(leftArm + $"{(rightArm == "" ? "" : " and ")}" + rightArm);
            SetColorsToText();
          
        }

        private void PlayerMove(Vector2D playersInput)
        {

            //var mapCell = _map.GetCell(playersInput + _map.Player.Position);
            //var actor = _map.GetActor(playersInput + _map.Player.Position);
            //if (mapCell.Symbol == '.' && actor == null)
            //{ 
            _player.MoveTo(playersInput);
            //}
            //else if (actor != null)
            //{
            //    if (actor is Monster)
            //    {
            //        _map.Player.Attack(actor);
            //    }
            //}
            //else
            //    _map.PlayerMoveTo(new Vector2D(0, 0));
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
            Vector2D vec = new Vector2D(0, 0);
            switch (key)
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
                    Vector2D move = new Vector2D(0, 0);

                    if (mon.IsActorInUpFov(_player))
                    {
                        move += mon.Move(Direction.Up);
                    }

                    if (mon.IsActorInDownFov(_player))
                    {
                        move += mon.Move(Direction.Down);
                    }

                    if (mon.IsActorInLeftFov(_player))
                    {
                        move += mon.Move(Direction.Left);
                    }

                    if (mon.IsActorInRigthFov(_player))
                    {
                        move += mon.Move(Direction.Right);
                    }

                    if (!mon.IsActorInFov(_player))
                        move += mon.Move();

                    //var pos = move + actor.Position;

                    //if (pos.X == _player.Position.X && pos.Y == _player.Position.Y)
                    //    mon.Attack(_player);
                    //else
                    //{
                    //    var mapCell = _map.GetCell(move + actor.Position);
                    //    var ac = _map.GetActor(move + actor.Position);
                    //    if (mapCell != null)
                    //    {
                    //        if (mapCell.Symbol == '.' && ac == null)
                    //            actor.Position += move;
                    //    }
                    //}
                }
            });
        }

        private void SetColorsToText()
        {
            foreach (var line in _messageLogScreen.Items)
            {
                if (line is Lable)
                {
                    (line as Lable).SetColorToWord(_player.Name, ConsoleColor.DarkGray);
                    if (_player.LeftArm != null)
                        SetColorToItemInText(_player.LeftArm, line as Lable);

                    if (_player.RightArm != null)
                        SetColorToItemInText(_player.RightArm, line as Lable);
                    
                    foreach (Actor actor in _map.Actors)
                    {
                        (line as Lable).SetColorToWord(actor.Name, actor.Color);
                        if (actor.LeftArm != null)
                            SetColorToItemInText(actor.LeftArm, line as Lable);

                        if (actor.RightArm != null)
                            SetColorToItemInText(actor.RightArm, line as Lable);
                    }
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
