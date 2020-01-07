using RougeLikeRPG.Core;
using RougeLikeRPG.Core.Controls;
using RougeLikeRPG.Engine.Actors.Enums;
using System;
using System.Collections.Generic;

namespace RougeLikeRPG.Engine.Actors
{
    /// <summary>
    /// Абсктракный класс обозначаюший ирока/моба/нпс
    /// </summary>
    internal abstract class Actor : IActor
    {
        #region Private Members
        #region Stats
        private Int32 _str;
        private Int32 _dex;
        private Int32 _intell;
        private Int32 _lucky;
        private Int32 _char;
        #endregion
        #endregion

        #region Public Properties
        public string       Name { get; set; }
        public int          Level { get; set; }
        public int          Hp { get; set; }
        public int          MaxHp { get; set; }
        public int          Exp { get; set; }
        public int          MaxExp { get; set; }
        public char         Symbol { get; set; }
        public Vector2D     Position { get; set; }
        public ConsoleColor Color { get; set; }
        public ConsoleColor BackColor { get; set; }
        public int Mana { get; set; }
        public int MaxMana { get; set; }

        #region Stats
        public int Str 
        {
            get => _str; 
            set
            {
                _str = value;
                StrMod = CalculateModificator(_str);
            }
        }
        public int Dex
        { 
            get => _dex; 
            set
            {
                _dex = value;
                DexMod = CalculateModificator(_dex);
            }
        }
        public int Intell 
        { 
            get => _intell; 
            set
            {
                _intell = value;
                IntellMod = CalculateModificator(_intell);
            }
        }
        public int Chari 
        { 
            get => _char; 
            set
            {
                _char = value;
                ChariMod = CalculateModificator(_char);
            }
        }
        public int Lucky 
        { 
            get => _lucky; 
            set
            {
                _lucky = value;
                LuckyMod = CalculateModificator(_lucky);
            }
        }
        #endregion

        #region Modificators
        public int StrMod { get; set; }
        public int DexMod { get; set; }
        public int IntellMod { get; set; }
        public int ChariMod { get; set; }
        public int LuckyMod { get; set; }
        #endregion
        public Race Race { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///  Методо отвечающий за перемещение актера
        /// </summary>
        /// <param name="position">позиция кторой смещается актер</param>
        public virtual void MoveTo(Int32 x, Int32 y)
        {
            MoveTo(new Vector2D(x, y));
        }
        /// <summary>
        ///  Методо отвечающий за перемещение актера
        /// </summary>
        /// <param name="position">позиция кторой смещается актер</param>
        public virtual void MoveTo(Vector2D position)
        {
            Position += position;
        }

        public IEnumerable<Control> GetStats()
        {
            var temp        = new List<Control>(); 
            var nameLable   = new Lable($"Name: {Name}", new Vector2D(1, 1));
            var raceLable   = new Lable($"Race: {Race}", new Vector2D(1, 2));
            var lvlLable    = new Lable($"Level: {Level}", new Vector2D(1, 5));
            
            var lvlProgerss = new Progressbar(23, 1, ConsoleColor.DarkYellow,   ConsoleColor.DarkGray,      new Vector2D(1, 6), Exp,    MaxExp);
            var hpProgerss  = new Progressbar(23, 1, ConsoleColor.DarkRed,      ConsoleColor.DarkGray,      new Vector2D(1, 3), Hp,     MaxHp);
            var mpProgerss  = new Progressbar(23, 1, ConsoleColor.DarkBlue,     ConsoleColor.DarkGray,      new Vector2D(1, 4), Mana,   MaxMana);

            var lineLable = new Lable("-----------------------", new Vector2D(1, 7));
            var strLable    = new Lable($"Str:    {Str} ({((StrMod > 0)? $"+{StrMod}": $"{StrMod}")})", new Vector2D(1, 8));
            var dexLable    = new Lable($"Dex:    {Dex} ({((DexMod > 0) ? $"+{DexMod}" : $"{DexMod}")})", new Vector2D(1, 9));
            var intelLable  = new Lable($"Int:    {Intell} ({((IntellMod > 0) ? $"+{IntellMod}" : $"{IntellMod}")})", new Vector2D(1, 10));
            var lckLable    = new Lable($"Lck:    {Lucky} ({((LuckyMod > 0) ? $"+{LuckyMod}" : $"{LuckyMod}")})", new Vector2D(1, 11));
            var chrLable    = new Lable($"Chr:    {Chari} ({((ChariMod > 0) ? $"+{ChariMod}" : $"{ChariMod}")})", new Vector2D(1, 12));
            var lineOneLable = new Lable("-----------------------", new Vector2D(1, 13));

            var headLable           = new Lable("Head:", new Vector2D(1, 14));
            var armorLable          = new Lable("Armor:", new Vector2D(1, 15));
            var footsLable          = new Lable("Foots:", new Vector2D(1, 16));
            var lArmLable           = new Lable("Lt Hand:", new Vector2D(1, 17));
            var rArmLable           = new Lable("Rh Hand:", new Vector2D(1, 18));
            var armorClassLable     = new Lable("Ar Class:", new Vector2D(1, 19));

            hpProgerss.Text     = $"Hp:     {Hp} / {MaxHp}";
            mpProgerss.Text     = $"Mana:   {Mana} / {MaxMana}";
            lvlProgerss.Text    = $"Exp:    {Exp} / {MaxExp}";
            hpProgerss.TextColor = mpProgerss.TextColor = ConsoleColor.White;

            temp.Add(lineLable);
            temp.Add(strLable);
            temp.Add(dexLable);
            temp.Add(intelLable);
            temp.Add(lckLable);
            temp.Add(chrLable);
            temp.Add(lineOneLable);

            temp.Add(nameLable);
            temp.Add(raceLable);
            temp.Add(lvlLable);
            temp.Add(hpProgerss);
            temp.Add(mpProgerss);
            temp.Add(lvlProgerss);

            temp.Add(headLable);
            temp.Add(armorLable);
            temp.Add(footsLable);
            temp.Add(lArmLable);
            temp.Add(rArmLable);
            temp.Add(armorClassLable);

            //temp.Add(hpLable);
            //temp.Add(mpLable);
            return temp;
        }
        #endregion

        #region Private Methods
        private Int32 CalculateModificator(Int32 statValue)
        {
            Int32 modificator = (statValue - 10 != 0) ? (statValue - 10) / 2 : 0;
            return modificator;
        }
        #endregion

    }
}
