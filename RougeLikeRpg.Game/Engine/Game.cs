using RougeLikeRPG.Graphic.Core;
using RougeLikeRPG.Engine.Dices;
using RougeLikeRPG.Graphic.Core.Controls;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Actors.Enums;
using RougeLikeRPG.Engine.Magick;
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
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRPG.Engine
{
    /// <summary>
    /// Класс реaлизущий игровую логику
    /// </summary>
    internal partial class Game
    {
        #region Private Members
        /// <summary>
        /// Игрок
        /// </summary>
        private Player _player;

        #endregion

        #region Constructors
        /// <summary>
        ///  Коструктор по умолчанию
        /// </summary>
        public Game()
        {
            Initialization();

            _player = new Player();
            _player = NewPlayer();
            
            _player.Position    += new Vector2D(_map.Width / 2, _map.Height / 2) + _map.Location;
            _player.LevelUp     += Player_LevelUp;
            _player.Attacking   += Player_Attacking;
            _player.Dying       += Actor_Dying;
            _player.Moving      += Player_Moving;

            _player.BookOfSpells.Casting += Player_CastingSpells;
            _map.Player = _player;
            _statusScreen.AddRange(_player.GetStats().ToList());


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
            _player.BookOfSpells.CastMappedSpell(key);
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
            while (true)
            {
                Update();
                Draw();
                //Thread.Sleep(1000);
            } 
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
                _player.Color = ColorManager.DarkGray;

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
                    line.SetColorToWord(word.Key, word.Value, ColorManager.Black);
                
                line.SetColorToPrase($"+Level of {_player.Name} Upped+", ColorManager.DarkYellow, ColorManager.Black);
                line.SetColorToWord(_player.Name, new Color(255, 102, 0), ColorManager.Black);

                if (_player.LeftArm != null)
                    SetColorToItemInText(_player.LeftArm, line);
                if (_player.RightArm != null)
                    SetColorToItemInText(_player.RightArm, line);
                foreach (Actor actor in _map.Actors)
                {
                    line.SetColorToWord(actor.Name, actor.Color, ColorManager.Black);
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
                    line.SetColorToWord(word, weapon.Rare.Color, ColorManager.Black);
            }
            else
                line.SetColorToWord(weapon.Name, weapon.Rare.Color, ColorManager.Black);
        }

        /// <summary>
        /// Ввод 
        /// </summary>
        /// <returns></returns>
        private Player NewPlayer() => _createPlayerScreen.Start();

        private void OnKeyDown(ConsoleKey key) => KeyDown?.Invoke(this, new KeyDownEventArgs(key));
        
        #endregion
    }
}
