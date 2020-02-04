using RougeLikeRPG.Core;
using RougeLikeRPG.Engine.Actors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.Actors.Monsters
{
    internal abstract class Monster : Actor
    {

        public Int32 FovX { get; set; }

        public Int32 FovY { get; set; }

        public bool IsActorInFov(Actor actor, Int32 fovX, Int32 fovY, out Direction direction)
        {
            var mon_position = this.Position;
            direction = Direction.None;
            if (IsInFov(actor, mon_position, fovX, Direction.Up))
            {
                direction = Direction.Up;
                return true;
            }
            if (IsInFov(actor, mon_position, fovX, Direction.Down))
            {
                direction = Direction.Down;
                return true;
            }
            if (IsInFov(actor, mon_position, fovY, Direction.Left))
            {
                direction = Direction.Left;
                return true;
            }
            if (IsInFov(actor, mon_position, fovY, Direction.Right))
            {
                direction = Direction.Right;
                return true;
            }
            return false;
        }

        public bool IsInFov(Actor actor, Vector2D start, Int32 fovRange, Direction direction)
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
            if ((this.Position + this.MoveDirectionVector(Direction.Up)).Equals(actor.Position))
                return true;

            else if ((this.Position + this.MoveDirectionVector(Direction.Down)).Equals(actor.Position))
                return true;

            else if ((this.Position + this.MoveDirectionVector(Direction.Right)).Equals(actor.Position))
                return true;

            else if ((this.Position + this.MoveDirectionVector(Direction.Left)).Equals(actor.Position))
                return true;

            else return false;
        }

        public Vector2D MoveRandomDirectionVector()
        {
            Direction dir = (Direction)new Random().Next(1, 5);
            return MoveDirectionVector(dir);
        }

        public Vector2D MoveDirectionVector(Direction dir)
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
