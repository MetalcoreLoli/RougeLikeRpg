using RougeLikeRPG.Core;
using RougeLikeRPG.Core.Controls;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougeLikeRPG.Engine.GameScreens
{
    internal class CreatePlayerScreen : Screen
    {

        private Screen _statsScreen;
        private Screen _inputPlayerNameScreen;
        private Screen _helpScreen;
        private bool IsAlive = false;
        public CreatePlayerScreen() 
            : base(60, 23,  new Vector2D(0, 0), "Player creating screen", ConsoleColor.White, ConsoleColor.Black)
        {
            _inputPlayerNameScreen = new Screen(25, 3, new Vector2D(29, 1));
            _inputPlayerNameScreen.Title = "Input Player Name";

            _helpScreen = new Screen(30, 18, new Vector2D(29, 4));
            _helpScreen.Title = "Help";
            _helpScreen.AddRange(new List<Control> { 
                new Lable("R     - to roll stats", new Vector2D(1, 1)),
                new Lable("Enter - to confirm roll", new Vector2D(1, 2)),
                new Lable("-> - to change race", new Vector2D(1, 3)),
                new Lable("<- - to change race", new Vector2D(1, 4)),
            });

            _statsScreen = new Screen(25, 21, new Vector2D(1, 1));
            _statsScreen.Title = "Player's Stats";

            IsAlive = true;
        }

        public Player Start()
        {
            Console.Clear();
            Player player = new Player();

            do
            {
                DrawAll();
                Update(player);
            } while (IsAlive);
            return player;
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
            if (player.Name ==  null)
                player.Name = InputName();

            //RollPlayerStats(player);
            
            switch (Input.PlayerKeyInput().Result)
            {
                case ConsoleKey.R: RollPlayerStats(player); break;
                case ConsoleKey.Enter: IsAlive = false;  break;
                case ConsoleKey.RightArrow:
                        player.Race++; 
                    break;
                case ConsoleKey.LeftArrow: 
                    if (player.Race > 0)
                        player.Race--; 
                    break;
            }
            _statsScreen.Items = new List<Control>();
            _statsScreen.AddRange(player.GetStats());
        }

        private void DrawAll()
        { 
           Draw();
            _statsScreen.Draw();
            _inputPlayerNameScreen.Draw();
            _helpScreen.Draw();
        }
        private void RollPlayerStats(Player player)
        {
            player.Symbol = '@';
            player.Color = ConsoleColor.White;
            //player.Hp = 17;
            //player.MaxHp = 17;
            player.Hp = player.MaxHp = DiceManager.CreateDices("4d6").RollAll().Sum();
            player.Mana = player.MaxMana = 2;
            player.Exp = 0;
            player.MaxExp = 20;
            player.Level = 1;
            player.Str = RollStat();
            player.Dex = RollStat();
            player.Intell = RollStat();
            player.Lucky = RollStat();
            player.Chari = RollStat();
        }
        private Int32 RollStat()
        {
            Int32[] values = DiceManager.CreateDices("4d6").RollAll();
            Int32 minDiceValue = values.Min();
            return values.Where(val => val != minDiceValue).Sum();
        }
    }
}
