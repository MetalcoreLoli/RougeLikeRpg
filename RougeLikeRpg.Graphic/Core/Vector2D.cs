using System;
using System.Runtime.CompilerServices;

namespace RougeLikeRPG.Graphic.Core
{
 
    public struct Vector2D
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

        public static Int32 operator *(Vector2D a, Vector2D b)
           => (b.X * a.X) + (b.Y * a.Y);

        public static Vector2D operator *(Vector2D a, Int32 b)
          => new Vector2D(a.X * b, a.Y * b);

        public static Vector2D operator *(Int32 b, Vector2D a)
          => new Vector2D(a.X * b, a.Y * b);

        public override bool Equals(object obj)
        {
            Vector2D b = (Vector2D)obj;
            return this.X.Equals(b.X) && this.Y.Equals(b.Y);
        }
    }
}
