﻿using RougeLikeRPG.Graphic.Core;
using RougeLikeRPG.Graphic.Core.Controls;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Actors.Builders;
using RougeLikeRPG.Engine.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRPG.Engine.GameScreens
{
    internal class CreatePlayerScreen : Screen
    {
        private PlayerBuilder builder;

        private Screen _statsScreen;
        private Screen _inputPlayerNameScreen;
        private Screen _helpScreen;

        private bool IsAlive = false;
        public CreatePlayerScreen() 
            : base(60, 23,  new Vector2D(0, 0), "Player creating screen", ColorManager.Black, ColorManager.White)
        {
            builder = new PlayerBuilder();

            builder.SetFovX(15);
            builder.SetFovY(4);
            builder.SetColor(new Color(255, 102, 0));
            _inputPlayerNameScreen = new Screen(25, 3, new Vector2D(29, 1))
            {
                Title = "Input Player Name"
            };

            _helpScreen = new Screen(30, 18, new Vector2D(29, 4))
            {
                Title = "Help"
            };
            _helpScreen.AddRange(new List<Control> {
                new Lable("R     - to roll stats",      new Vector2D(1, 1)),
                new Lable("Enter - to confirm roll",    new Vector2D(1, 2)),
                new Lable("->    - to change race",     new Vector2D(1, 3)),
                new Lable("<-    - to change race",     new Vector2D(1, 4)),
            }) ;

            _statsScreen = new Screen(25, 21, new Vector2D(1, 1)) { Title = "Player's Stats" };
            _statsScreen.AddRange(builder.Get().GetStats());

            Add(_helpScreen);
            Add(_statsScreen);
            Add(_inputPlayerNameScreen);
            PlayersSymbolAndColors();
            
            IsAlive = true;
        }

        public Player Start()
        {
            Draw();
            while (IsAlive)
            {
                Clear(this.BackgroundColor);
                Update();
                Draw();
            } 
            return builder.Get();
        }

        private void PlayersSymbolAndColors()
        {
            builder.SetColor(ColorManager.White);
            builder.SetSymbol('@');
        }

        private string InputName()
        {
            (int left, int top) cursor = (Console.CursorLeft, Console.CursorTop);
            Vector2D curslocation = _inputPlayerNameScreen.Location + Location;
            Console.SetCursorPosition(curslocation.X + 1, curslocation.Y + 1);
            string name = Console.ReadLine();
            Console.SetCursorPosition(cursor.left, cursor.top);
            return name;
        }

        public override void Update()
        {
            base.Update();
            if (builder.Get().Name == null)
                builder.SetName(InputName());

            switch (Input.PlayerKeyInput().Result)
            {
                case ConsoleKey.R: builder.RollStats(); break;
                case ConsoleKey.Enter: IsAlive = false;  break;
                case ConsoleKey.RightArrow:
                    if (builder.Get().Race + 1 != Actors.Enums.Race.None)
                    {
                        builder.Get().Race++;
                        builder.SetRace(builder.Get().Race);
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (builder.Get().Race > 0)
                    {
                        builder.Get().Race--;
                        builder.SetRace(builder.Get().Race);
                    }
                    break;
            }
            StartItems(builder.Get());

            _statsScreen.Items = new List<Control>();
            var playerStats = builder.Get().GetStats();
            _statsScreen.AddRange(playerStats);
        }

        private void StartItems(Player player)
        {
            player.ArmorClass = 0;
            player.Equip(new GameItems.Items.Weapon.ShortSword());
            player.Equip(new GameItems.Items.Weapon.Dagger());
            player.Equip(new GameItems.Items.Armor.LeatherArmor());
            player.Equip(new GameItems.Items.Armor.LeatherBoots());
        }
    }
}
