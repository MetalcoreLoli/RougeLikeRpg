using System;

namespace RougeLikeRPG.Graphic.Core
{
    [Serializable]
    public struct Color
    {
        public Int32 Red {get; set;}
        public Int32 Green {get; set;}
        public Int32 Blue {get; set;}

        public Color(int red, int green, int blue)
            => (Red, Green, Blue) = (red, green, blue);


        public override bool Equals(object other)
        {
            if (other is Color otherColor)
            {
                return 
                    Red   == otherColor.Red &&
                    Green == otherColor.Green &&
                    Blue  == otherColor.Blue;
            }
            else 
                throw new Exception("Type Error");
        }
    }
}
