using System;
using System.Runtime.CompilerServices;

namespace RougeLikeRPG.Graphic.Core
{
    [Serializable]
    public readonly struct Vector2D
    {
        public Int32 X { get; }
        public Int32 Y { get; }
        public Int32 Lenght { get => (Int32)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)); }
        public Vector2D Normalized => new Vector2D(X / Lenght, Y / Lenght);
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
            if (obj is Vector2D b)
                return this.X.Equals(b.X) && this.Y.Equals(b.Y);
            else throw new Exception("?? ?????????? ????? !!!");

        }

        public override string ToString()
            => $"[{X}; {Y}]";

        public static bool operator ==(Vector2D a, Vector2D b)
            => a.Equals(b);

        public static bool operator !=(Vector2D a, Vector2D b)
          => !a.Equals(b);

    }
}
