using System;

namespace RougeLikeRpg.Graphic.Core
{
    [Serializable]
    public readonly record struct Color(int Red, int Green, int Blue)
    {
        public override string ToString() => $"[{Red};{Green};{Blue}]";
        public override int GetHashCode() => Red.GetHashCode() ^ Green.GetHashCode() ^ Blue.GetHashCode();
    }
}
