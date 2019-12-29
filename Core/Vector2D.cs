using System;


namespace RougeLikeRPG.Core
{
    internal struct Vector2D
    {
        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public Vector2D(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
        }


        public static Vector2D operator +(Vector2D a, Vector2D b)
            => new Vector2D(a.X + b.X, a.Y + b.Y);

        public static Vector2D operator -(Vector2D a, Vector2D b)
            => new Vector2D(a.X - b.X, a.Y - b.Y);

        public static Vector2D operator +(Vector2D a, Int32 b)
            => new Vector2D(a.X + b, a.Y + b);
        
        public static Vector2D operator -(Vector2D a, Int32 b)
            => new Vector2D(a.X - b, a.Y - b);
    }
}
