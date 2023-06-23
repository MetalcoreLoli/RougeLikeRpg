using System;
using RougeLikeRpg.Engine.Magick.Events;

namespace RougeLikeRpg.Engine.Magick
{
    public interface ISpell
    {
        string Name { get; set; }
        void Cast();
        event EventHandler<CastingSpellEventArgs> Casting;
    }
}
