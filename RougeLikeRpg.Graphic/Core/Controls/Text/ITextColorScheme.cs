using System;
using System.Collections.Generic;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Core.Controls.Text 
{
    public interface ITextColorScheme
    {
        Dictionary<string, Color> Scheme { get; } 
        void Apply (ref Cell[] cells);
    }
}
