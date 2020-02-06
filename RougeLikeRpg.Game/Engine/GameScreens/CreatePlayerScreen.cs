using RougeLikeRPG.Graphic.Core;
using RougeLikeRPG.Graphic.Core.Controls;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Actors.Builders;
using RougeLikeRPG.Engine.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougeLikeRPG.Engine.GameScreens
{
    internal class CreatePlayerScreen : Screen
    {
        private PlayerBuilder builder;

        private Screen _statsScreen;
        private Screen _inputPlayerNameScreen;
        private Screen _helpScreen;

        private Render _render;
        private bool IsAlive = false;
        public CreatePlayerScreen() 
            : base(60, 23,  new Vector2D(0, 0), "Player creating screen", ConsoleColor.White, ConsoleColor.Black)
        {
            builder = new PlayerBuilder();

            builder.SetFovX(15);
            builder.SetFovY(4);

            _render = new Render();
            _inputPlayerNameScreen = new Screen(25, 3, new Vector2D(29, 1));
            _inputPlayerNameScreen.Title = "Input Player Name";

            _helpScreen = new Screen(30, 18, new Vector2D(29, 4));
            _helpScreen.Title = "Help";
            _helpScreen.AddRange(new List<Control> { 
                new Lable("R     - to roll stats",      new Vector2D(1, 1)),
                new Lable("Enter - to confirm roll",    new Vector2D(1, 2)),
                new Lable("->    - to change race",     new Vector2D(1, 3)),
                new Lable("<-    - to change race",     new Vector2D(1, 4)),
            });

            _statsScreen = new Screen(25, 21, new Vector2D(1, 1));
            _statsScreen.Title = "Player's Stats";
            _statsScreen.AddRange(builder.Get().GetStats());

            Items.AddRange(new List<Control> { 
                _inputPlayerNameScreen,
                _helpScreen,
                _statsScreen
            });

            PlayersSymbolAndColors();
            
            IsAlive = true;
        }

        public Player Start()
        {
            do
            {
                Draw();
                Update(builder.Get());
            } while (IsAlive);
            return builder.Get();
        }

        //private new void Draw()
        //{
        //    Console.Clear();
        //    List<string> frame = new List<string>();
        //    string line = "";
        //    List<Cell> cells = new List<Cell>();

        //    foreach (Cell cell in GetCells())
        //        cells.Add(cell);

        //    foreach (Cell cell in cells)
        //    {
        //        line += cell.Symbol;
        //        if (cell.Position.X.Equals(Width - 1))
        //        {
        //            frame.Add(line);
        //            line = "";
        //        }
        //    }
        //    foreach (Cell cell in GetCells())
        //        Render.WithOffset(cell, 0, 0);
        //    //foreach (var l in frame)
        //    //    Console.WriteLine(l);
        //}

        private void PlayersSymbolAndColors()
        {
            builder.SetColor(ConsoleColor.White);
            builder.SetSymbol('@');
        }

        private string InputName()
        {
            (int left, int top) cursor = (Console.CursorLeft, Console.CursorTop);
            Console.SetCursorPosition(_inputPlayerNameScreen.Location.X + 1, _inputPlayerNameScreen.Location.Y + 1);
            string name = Console.ReadLine();
            Console.SetCursorPosition(cursor.left, cursor.top);
            return name;
        }

        private void Update(Player player)
        {
            //Console.Clear();
            if (builder.Get().Name == null)
                builder.SetName(InputName());

            switch (Input.PlayerKeyInput().Result)
            {
                case ConsoleKey.R: builder.RollStats(); break;
                case ConsoleKey.Enter: IsAlive = false;  break;
                case ConsoleKey.RightArrow:
                    if (builder.Get().Race + 1 != Actors.Enums.Race.None)
                        builder.Get().Race++; 
                    break;
                case ConsoleKey.LeftArrow: 
                    if (builder.Get().Race > 0)
                        builder.Get().Race--; 
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
