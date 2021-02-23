using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using RougeLikeRpg.Engine.Core;
using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Engine.Dices;
using RougeLikeRpg.Engine.Actors;
using RougeLikeRpg.Engine.Events;
using RougeLikeRpg.Engine.GameScreens;
using RougeLikeRpg.Engine.Actors.Enums;
using RougeLikeRpg.Engine.Actors.Monsters;
using RougeLikeRpg.Engine.GameItems.Items;
using RougeLikeRpg.Engine.Core.Configuration;
using RougeLikeRpg.Graphic.Controls;

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
        private Vector2D _mapLocation = new Vector2D(0, 0);

        /// <summary>
        /// Карта
        /// </summary>
        private Map _map;

        private CreatePlayerScreen _createPlayerScreen;
        private MessageLogScreen _messageLogScreen;
        private Screen _statusScreen;
        private MapScreen _mapScreen;
        #endregion

        #region Events
        public event EventHandler<KeyDownEventArgs> KeyDown;

        #endregion
        private void Initialization()
        {
            Console.Write("\x1b[?25l");
            KeyDown             += Game_KeyDown;

            var mapScreenConfig = new MapScreenConfiguration();
            var mapConfig       = new MapScreenConfiguration(
                mapScreenConfig.Width - 2, 
                mapScreenConfig.Height - 2, 
                mapScreenConfig.Location + 1, 
                mapScreenConfig.ForegroundColor,
                mapScreenConfig.BackgroundColor);

            _createPlayerScreen = new CreatePlayerScreen();
            _messageLogScreen   = new MessageLogScreen("Message Log", new MessageLogScreenConfiguration());
            _statusScreen       = new Screen("Status", new StatusScreenConfiguration());

            _map        = new Map(mapConfig);
            _mapScreen  = new MapScreen("Dungeon Map | Floor: " + _map.CurrentFloor, mapScreenConfig);
            _mapScreen.Add(_map);
        }
    }
}
