using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Engine.Dices;
using RougeLikeRpg.Graphic.Core.Controls;
using RougeLikeRpg.Engine.Actors;
using RougeLikeRpg.Engine.Actors.Enums;
using RougeLikeRpg.Engine.Magick;
using RougeLikeRpg.Engine.GameScreens;
using RougeLikeRpg.Engine.Events;
using RougeLikeRpg.Engine.Actors.Monsters;
using RougeLikeRpg.Engine.GameItems.Items;
using RougeLikeRpg.Engine.Core;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RougeLikeRpg.Engine
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

            _player = NewPlayer();
            
            _player.Position    += new Vector2D(_map.Width >> 1, _map.Height >> 1) + _map.Location;
            //_player.LevelUp     += Player_LevelUp;
            //_player.Attacking   += Player_Attacking;
            //_player.Dying       += Actor_Dying;
            //_player.Moving      += Player_Moving;

            //_player.BookOfSpells.Casting += Player_CastingSpells;
            _statusScreen.AddRange(_player.GetStats().ToList());
        }

        private void HitMessages(Actor actor, Actor enemy, WeaponItem weapon)
        {
            string messsage = 
                $"{enemy.Name} was hitten by {actor.Name} with {weapon.Name} at {weapon.RolledDamage}";
            _messageLogScreen.Add(messsage);
        }

        private void MissMessages(Actor actor, WeaponItem weapon)
        {
            string messsage = $"{actor.Name} is miss by {weapon.Name} ";
            _messageLogScreen.Add(messsage);
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
            Draw();
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
            _mapScreen.Draw();
            _messageLogScreen.Draw();
            _statusScreen.Draw();
        }
        private void Game_KeyDown(object sender, KeyDownEventArgs e)
        {
            switch (e.Key)
            {
                case ConsoleKey.R:
                    _map.Rebuild();
                    _mapScreen.Title = "Dungeon Map | Floor: " + _map.CurrentFloor;
                    break;
                default:
                    _map.Move (PlayerMoveTo (e.Key));
                    break;
            }
        }
        private Vector2D PlayerMoveTo(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    return Actor.MoveDirectionVector(Direction.Up);

                case ConsoleKey.DownArrow:
                    return Actor.MoveDirectionVector(Direction.Down);

                case ConsoleKey.LeftArrow:
                    return Actor.MoveDirectionVector(Direction.Left);

                case ConsoleKey.RightArrow:
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
            /*
            if (_player.Hp > 0)
                PlayerInput();
            else
                _player.Color = ColorManager.DarkGray;

            MonstersMove();
            _map.Update();
            */
            PlayerInput();
            _mapScreen.Clear(_mapScreen.BackgroundColor);
            _mapScreen.Update();
            _statusScreen.Items = new List<Control>();
            _statusScreen.Clear(_statusScreen.BackgroundColor);
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

        /// <summary>
        /// Ввод 
        /// </summary>
        /// <returns></returns>
        private Player NewPlayer() => _createPlayerScreen.Start();

        private void OnKeyDown(ConsoleKey key) => KeyDown?.Invoke(this, new KeyDownEventArgs(key));
        #endregion
    }
}
