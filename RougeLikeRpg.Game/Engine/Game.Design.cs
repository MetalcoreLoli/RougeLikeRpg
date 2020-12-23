using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Engine.Dices;
using RougeLikeRpg.Graphic.Core.Controls;
using RougeLikeRpg.Engine.Actors;
using RougeLikeRpg.Engine.Actors.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RougeLikeRpg.Engine.GameScreens;
using RougeLikeRpg.Engine.Events;
using RougeLikeRpg.Engine.Actors.Monsters;
using RougeLikeRpg.Engine.GameItems.Items;
using RougeLikeRpg.Engine.Core;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Engine
{
    /// <summary>
    /// Класс реaлизущий игровую логику
    /// </summary>
    internal partial class Game
    {

        #region Private Members
        /// <summary>
        /// Высота карты
        /// </summary>
        private Int32 _mapHeight = 20;
        /// <summary>
        /// Ширина Карты
        /// </summary>
        private Int32 _mapWidth = 37;

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

        private void Player_CastingSpells(object sender, Magick.Events.CastingSpellEventArgs e)
        {
            (_messageLogScreen as MessageLogScreen).Add($"{_player.Name} casting {(e.Name)}");
        }

        private void Actor_Moving(object sender, Actors.Events.MovingEventArgs e)
        {
            Actor actor = sender as Actor;
            //actor.IsMoving = true;
            if (!(_map.IsWalkable(actor.Position)) || 
                    actor.Position == _player.Position)
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
            if (_map.IsWalkable(pl.Position) && _map.Actors.FirstOrDefault(a => a.Position == pl.Position) == null)
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
        }

        private async void Player_Attacking(object sender, Actors.Events.AttackingEventArgs e)
        {
            Player actor = sender as Player;
            var color = e.Enemy.Color;
            if (!e.IsMissed)
            {
                HitMessages(actor, e.Enemy, e.Weapon);
            }
            else
            {
                MissMessages(actor, e.Weapon);
            }
        }

        private void Player_LevelUp(object sender, Actors.Events.LevelUpEventArgs e)
        {
            string levelUpMessage = $"+Level of {e.Actor.Name} was Upped+  ";
            (_messageLogScreen as MessageLogScreen).Add(levelUpMessage);
            var line = ((_messageLogScreen as MessageLogScreen).Items.Last() as Lable);
        }

        private void Game_KeyDown(object sender, KeyDownEventArgs e)
        {
            //(_messageLogScreen as MessageLogScreen).Add($"{e.Key} was pressed");
            Vector2D playersInput = PlayerMoveTo(e.Key);
            //Console.Title = _map.Player.Direction.ToString();
            PlayerCastSpells(e.Key);

            if (e.Key == ConsoleKey.F)
            {
                _map.GoDown();
                foreach (var actor in _map.Actors)
                {
                    if (!(actor is Player))
                    {

                        actor.Attacking += Hit_Attacking;
                        actor.Dying += Actor_Dying;
                        actor.Moving += Actor_Moving;
                    }
                }
            }

            PlayerMove(playersInput);
        }
        #endregion
        private void Initialization()
        {
            _createPlayerScreen = new CreatePlayerScreen();

            KeyDown += Game_KeyDown;

            _statusScreenWidth = 25;
            _statusScreenHeight = _invetoryScreenHeight + _mapHeight;

            _mapWidth += _statusScreenWidth;

            _messageLogScreenWidth = /*_statusScreenWidth + */_mapWidth;
            _messageLogScreenHeight = _invetoryScreenHeight;

            _statusScreenLocation = new Vector2D(_mapWidth, 0);
            _invetoryScreenLocation = new Vector2D(0, 0);
            _mapLocation = new Vector2D(0, 0);
            _messageLogScreenLocation = new Vector2D(
                                                        0,
                                                        _mapHeight);

            _messageLogScreen = new MessageLogScreen(
                                                _messageLogScreenWidth,
                                                _messageLogScreenHeight,
                                                _messageLogScreenLocation,
                                                "Message Log",
                                                ColorManager.Black,
                                                ColorManager.White);

            _levelUpMenuScreen = new LevelUpMenuScreen(
                                               50,
                                               25,
                                               new Vector2D(0, 0),
                                               "Level Up Menu",
                                               ColorManager.Black,
                                               ColorManager.White);

            _statusScreen = new Screen(
                                                 _statusScreenWidth,
                                                 _statusScreenHeight,
                                                 _statusScreenLocation);


            _inventoryScreen = new Screen(
                                                _invetoryScreenWidth,
                                                _invetoryScreenHeight,
                                                _invetoryScreenLocation,
                                                "",
                                                ColorManager.DarkYellow,
                                                ColorManager.Black);

            _map = new Map(_mapWidth, _mapHeight, _mapLocation);


            string title = "Status";
            _statusScreen.TitleLocation = new Vector2D(
                    (_statusScreen.Width - title.Length - 1) / 4, 2);
            _statusScreen.Title = title;

            _messageLogScreen.Title = "Message Log";


            foreach (var actor in _map.Actors)
            {
                actor.Attacking += Hit_Attacking;
                actor.Dying += Actor_Dying;
                actor.Moving += Actor_Moving;
            }
        }
    }
}
