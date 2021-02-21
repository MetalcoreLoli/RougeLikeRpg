using System;
using System.Linq;
using System.Collections.Generic;

using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Engine.GameItems;
using RougeLikeRpg.Engine.GameItems.Items;
using RougeLikeRpg.Engine.GameItems.Items.Weapon;
using RougeLikeRpg.Graphic.Controls.Text;

namespace RougeLikeRpg.Engine.Core
{
    public class DungeonColorScheme : ITextColorScheme
    {

        static List<Item> m_items = new List<Item>
        {
            new Dagger(), new ShortSword()
        };

        public DungeonColorScheme()
        {

            /*
             *
             * 
             * "common"    =>  WeaponRareColor.Common,
                "trash"     =>  WeaponRareColor.Trash,
                "rare" =>       WeaponRareColor.Rare,
                "legendary" =>  WeaponRareColor.Legendary,
                Trash        = ColorManager.DarkGray;
                Common       = ColorManager.DarkBlue;               *
                Rare         = ColorManager.DarkMagenta;            *
                Legendary    = ColorManager.DarkYellow;             *
             * */
            Scheme =  new Dictionary<string, Color>
            {
                ["Goblin"]      = ColorManager.Green,
                ["trash"]       = ColorManager.DarkGray,
                ["common"]      = ColorManager.DarkBlue,
                ["rare"]        = ColorManager.DarkMagenta,
                ["legendary"]   = ColorManager.DarkYellow,
                ["miss"]        = ColorManager.Red,
            };

            foreach (var item in m_items)
            {
                if (item.Name.Split(' ').Count() > 1)
                    foreach (var name in item.Name.Split(' '))
                        Scheme.Add (name, item.Rare.Color);
                else 
                    Scheme.Add (item.Name, item.Rare.Color);
            }
        }

        public Dictionary<string, Color> Scheme { get; } 
        
        public void Apply (ref Cell[] cells)
        {
            List<Word> text = Word.CellsToText(cells);
            foreach (var word in text)
            {
                if (Scheme.ContainsKey(word.Text))
                {
                    word.Color = Scheme[word.Text]; 
                }
            }
 
        }
    }
}
