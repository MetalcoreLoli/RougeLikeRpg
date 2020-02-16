using System;
using RougeLikeRPG.Engine.Magick.Events;

namespace RougeLikeRPG.Engine.Magick
{
    internal interface ISpell
    {
        string Name { get; set; }
        void Cast();
        event EventHandler<CastingSpellEventArgs> Casting;
    }
}
