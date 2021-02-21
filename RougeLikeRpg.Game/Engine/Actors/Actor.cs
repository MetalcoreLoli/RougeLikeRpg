using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Graphic.Core.Controls;
using RougeLikeRpg.Engine.Actors.Enums;
using RougeLikeRpg.Engine.Actors.Events;
using RougeLikeRpg.Engine.Dices;
using RougeLikeRpg.Engine.GameItems;
using RougeLikeRpg.Engine.GameItems.Enums;
using RougeLikeRpg.Engine.GameItems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using RougeLikeRpg.Engine.Magick;
using RougeLikeRpg.Graphic.Controls;

namespace RougeLikeRpg.Engine.Actors
{
    /// <summary>
    /// Абсктракный класс обозначаюший ирока/моба/нпс
    /// </summary>
    internal abstract class Actor : IActor
    {
        #region Private Members

        private Color _color;
        // private Color _colorNormal;
        #region Stats
        private Int32 _str;
        private Int32 _dex;
        private Int32 _intell;
        private Int32 _lucky;
        private Int32 _char;
        #endregion

        private Int32 _lvl = 1;
        private Int32 _exp = 0;
        private Int32 _hp;
        #endregion

        #region Public Properties
        public string Name { get; set; }
        public int Level
        {
            get => _lvl;
            set
            {
                _lvl = value;
            }
        }
        public int Hp 
        {
            get => _hp;
            set 
            {
                Int32 damage = _hp - value;
                _hp = value;
                if ((float)_hp <= ((float)MaxHp  * 0.35f) % 100.0f)
                    Color = ColorManager.Red;
                else 
                    Color = NormalColor;
                
                if (_hp <= 0)
                { 
                    IsDead = true;
                   // OnDying(Name, damage, DropExp);
                }
            }
        }
        public int MaxHp { get; set; }
        public int Exp
        {
            get => _exp;
            set
            {
                _exp = value;
                if (_exp >= MaxExp)
                    UpLevel();
            }
        }
        public int MaxExp { get; set; }
        public char Symbol { get; set; }
        public Vector2D Position { get; set; }
        public Color Color 
        { 
            get => _color; 
            set
            {
                _color = value;
            }
        }

        public Color NormalColor { get; set; }

        public Color BackColor { get; set; }
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
        public Boolean IsMoving { get; set; } = false;
        public Race Race { get; set; }
        public BookOfSpells BookOfSpells { get; set; }
        public Inventory Inventory { get; set; }
        public ArmorItem Head { get; set; }
        public ArmorItem Armor { get; set; }
        public ArmorItem Foots { get; set; }
        public WeaponItem LeftArm { get; set; }
        public WeaponItem RightArm { get; set; }
        public int ArmorClass { get; set; }
        public bool IsDead { get; set; }
        public Int32 DropExp { get; set; }
        public int FovX { get; set; }
        public int FovY { get; set; }

        #endregion

        #region Events
        public event EventHandler<LevelUpEventArgs> LevelUp;
        public event EventHandler<AttackingEventArgs> Attacking;
        public event EventHandler<ActorDyingEventArgs> Dying;
        public event EventHandler<MovingEventArgs> Moving;
        #endregion

        #region Public Methods

        public void IfDo(Actor actor, Func<Actor, Actor, bool> canAction, Action<Actor, Actor> action)
        {
            if (canAction(this, actor))
                action(this, actor);
        }
        public void Attack(Actor enemy)
        {
            if (LeftArm != null)
                HitByWeapon(enemy, LeftArm);
           
            if (RightArm != null)
                HitByWeapon(enemy, RightArm);
        }

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
            IsMoving = true;
            OnMoving(position);
        }

        public bool IsActorInFovXY(Actor actor, Int32 fovX, Int32 fovY, out Direction direction)
        {
            var mon_position = this.Position;
            direction = Direction.None;
            if (IsActorInFov(actor, mon_position, fovX, Direction.Up))
            {
                direction = Direction.Up;
                return true;
            }
            if (IsActorInFov(actor, mon_position, fovX, Direction.Down))
            {
                direction = Direction.Down;
                return true;
            }
            if (IsActorInFov(actor, mon_position, fovY, Direction.Left))
            {
                direction = Direction.Left;
                return true;
            }
            if (IsActorInFov(actor, mon_position, fovY, Direction.Right))
            {
                direction = Direction.Right;
                return true;
            }
            return false;
        }
        
        public bool IsActorInFov(Actor actor, Vector2D start, Int32 fovRange, Direction direction)
        {
            Vector2D pos = start;
            for (int i = 0; i < fovRange; i++)
            {
                pos += MoveDirectionVector(direction);
                if (pos.Equals(actor.Position))
                    return true;
            }
            return false;
        }

        public bool IsActorInFov(Actor actor)
        {
            if ((this.Position + MoveDirectionVector(Direction.Up)).Equals(actor.Position))
                return true;

            else if ((this.Position + MoveDirectionVector(Direction.Down)).Equals(actor.Position))
                return true;

            else if ((this.Position + MoveDirectionVector(Direction.Right)).Equals(actor.Position))
                return true;

            else if ((this.Position + MoveDirectionVector(Direction.Left)).Equals(actor.Position))
                return true;

            else return false;
        }

        public static Vector2D MoveDirectionVector(Direction dir)
        {
            Vector2D vec = new Vector2D(0, 0);
            switch (dir)
            {
                case Direction.Up:
                    vec = new Vector2D(0, -1);
                    break;

                case Direction.Down:
                    vec = new Vector2D(0, 1);
                    break;

                case Direction.Left:
                    vec = new Vector2D(-1, 0);
                    break;

                case Direction.Right:
                    vec = new Vector2D(1, 0);
                    break;
            }
            return vec;
        }
        public static Vector2D MoveRandomDirectionVector()
        {
            Direction dir = (Direction)new Random().Next(1, 5);
            return MoveDirectionVector(dir);
        }

        public Int32 RollStat()
        {
            Int32[] values = DiceManager.CreateDices("4d6").RollAll();
            Int32 minDiceValue = values.Min();
            return values.Where(val => val != minDiceValue).Sum();
        }
        public IEnumerable<Control> GetStats()
        {
            var temp            = new List<Control>(); 
            var nameLable       = new Lable($"Name: {Name}", new Vector2D(1, 1));
            var raceLable       = new Lable($"Race: {Race}", new Vector2D(1, 2));
            var lvlLable        = new Lable($"Level: {Level}", new Vector2D(1, 5));
            
            var lvlProgerss     = new Progressbar(23, 1, ColorManager.DarkYellow,   ColorManager.DarkGray,      new Vector2D(1, 6), Exp,    MaxExp);
            var hpProgerss      = new Progressbar(23, 1, ColorManager.DarkRed,      ColorManager.DarkGray,      new Vector2D(1, 3), Hp,     MaxHp);
            var mpProgerss      = new Progressbar(23, 1, ColorManager.DarkBlue,     ColorManager.DarkGray,      new Vector2D(1, 4), Mana,   MaxMana);

            var lineLable       = new Lable("-----------------------", new Vector2D(1, 7));
            var strLable        = new Lable($"Str:    {Str} ({((StrMod > 0)? $"+{StrMod}": $"{StrMod}")})", new Vector2D(1, 8));
            var dexLable        = new Lable($"Dex:    {Dex} ({((DexMod > 0) ? $"+{DexMod}" : $"{DexMod}")})", new Vector2D(1, 9));
            var intelLable      = new Lable($"Int:    {Intell} ({((IntellMod > 0) ? $"+{IntellMod}" : $"{IntellMod}")})", new Vector2D(1, 10));
            var lckLable        = new Lable($"Lck:    {Lucky} ({((LuckyMod > 0) ? $"+{LuckyMod}" : $"{LuckyMod}")})", new Vector2D(1, 11));
            var chrLable        = new Lable($"Chr:    {Chari} ({((ChariMod > 0) ? $"+{ChariMod}" : $"{ChariMod}")})", new Vector2D(1, 12));
            var lineOneLable    = new Lable("-----------------------", new Vector2D(1, 13));

            var headLable           = new Lable($"Head:     {(Head != null? Head.Name : "Nothing")}", new Vector2D(1, 14));
            var armorLable          = new Lable($"Armor:    {(Armor != null ? Armor.Name : "Nothing")}", new Vector2D(1, 15));
            var footsLable          = new Lable($"Foots:    {(Foots != null ? Foots.Name : "Nothing")}", new Vector2D(1, 16));
            var lArmLable           = new Lable($"Lt Hand:  {(LeftArm != null? LeftArm.Name : "Nothing")}", new Vector2D(1, 17));
            var rArmLable           = new Lable($"Rh Hand:  {(RightArm != null ? RightArm.Name : "Nothing")}", new Vector2D(1, 18));
            var armorClassLable     = new Lable($"Ar Class: {ArmorClass}", new Vector2D(1, 19));

            hpProgerss.Text         = $"Hp:     {Hp} / {MaxHp}";
            mpProgerss.Text         = $"Mana:   {Mana} / {MaxMana}";
            lvlProgerss.Text        = $"Exp:    {Exp} / {MaxExp}";
            hpProgerss.TextColor    = mpProgerss.TextColor = lvlProgerss.TextColor = ColorManager.White;

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
            return temp;
        }
        #endregion

        #region Private Methods

        private void HitByWeapon(Actor actor, WeaponItem weapon)
        {
            Int32 isHit = DiceManager.CreateDice("1d20").Roll() + GetValueOfModificatorByWeapon(weapon.Modificator);
            if (isHit > 10)
            {
                Int32 damage = isHit != 20 ? weapon.RolledDamage : weapon.RolledDamage + weapon.RolledDamage;
                if (actor.Hp > 0)
                {
                    actor.Hp -= damage;
                    OnAttack(false, damage, actor, weapon);
                }
                if (actor.Hp <= 0)
                {
                    actor.IsDead = true;
                    Exp += actor.DropExp;
                    OnDying(actor.Name, damage, actor.DropExp);
                }
            }
            else
                OnAttack(true, 0, actor, weapon);
        }
        private Int32 GetValueOfModificatorByWeapon(WeaponItemModificator modificator)
        {
            return modificator switch
            {
                WeaponItemModificator.Str => StrMod,
                WeaponItemModificator.Dex => DexMod,
                WeaponItemModificator.Int => IntellMod,
                WeaponItemModificator.Lck => LuckyMod,
                _ => 0
            };
        }
        private Int32 CalculateModificator(Int32 statValue)
        {
            Int32 modificator = (statValue - 10 != 0) ? (statValue - 10) / 2 : 0;
            return modificator;
        }

        protected virtual void OnMoving(Vector2D vector) => Moving?.Invoke(this, new MovingEventArgs(vector));
        protected virtual void OnLevelUp(Actor actor) => LevelUp?.Invoke(this, new LevelUpEventArgs(actor));
        protected virtual void OnAttack(bool isMissed, Int32 damage, Actor actor, WeaponItem weapon) => Attacking?.Invoke(this, new AttackingEventArgs(isMissed, damage, actor, weapon));
        protected virtual void OnDying(String name, Int32 damage, Int32 dropExp) => Dying?.Invoke(this, new ActorDyingEventArgs(name, damage, dropExp));
        #endregion

        #region Public Properties

        public void UpLevel()
        {
            OnLevelUp(this);
            Level++;
            MaxExp  *= 2;
            MaxHp   += DiceManager.CreateDices("2d6").RollAll().Sum();
            Hp      = MaxHp;
            Mana    = (Level % 2 == 0 ? MaxMana += 1: MaxMana);
        }


        /// <summary>
        /// Метожд для экипирование шмотки
        /// </summary>
        /// <param name="item">Шмотка</param>
        public void Equip(Item item)
        {
            if (item is ArmorItem)
            {
                switch (item.EquipType)
                {
                    case ItemEquipType.Head:
                        Head = item as ArmorItem;
                        ArmorClass += Head.ArmorClass;
                        break;

                    case ItemEquipType.Armor:
                        Armor = item as ArmorItem;
                        ArmorClass += Armor.ArmorClass;
                        break;

                    case ItemEquipType.Foots:
                        Foots = item as ArmorItem;
                        ArmorClass += Foots.ArmorClass;
                        break;
                }
            }

            if (item is WeaponItem)
            {
                if (LeftArm == null)
                    LeftArm = item as WeaponItem;
                else if (RightArm == null)
                    RightArm = item as WeaponItem;
            }
        }
        #endregion
    }
}
