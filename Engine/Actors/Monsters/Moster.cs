using RougeLikeRPG.Core;
using RougeLikeRPG.Engine.Actors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.Actors.Monsters
{
    internal abstract class Monster : Actor
    {

        public Vector2D Move()
        {
            Direction dir = (Direction)new Random().Next(1, 5);
            return Move(dir);
        }

        public Vector2D Move(Direction dir)
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
    }
}
