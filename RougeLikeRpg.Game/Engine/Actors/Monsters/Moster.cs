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
        
        public bool IsActorInUpFov(Actor actor)
        {
            Vector2D pos = this.Position + Move(Direction.Up);
            for (int i = 0; i < FovX; i++)
            {
                if (pos.X == actor.Position.X && pos.Y == actor.Position.Y)
                    return true;
                pos.X++;
            }
            return false;
        }

        public bool IsActorInDownFov(Actor actor)
        {
            Vector2D pos = this.Position + Move(Direction.Down);
            for (int i = 0; i < FovX; i++)
            {
                pos.X--;
                if (pos.X == actor.Position.X && pos.Y == actor.Position.Y)
                    return true;
            }
            return false;
        }

        public bool IsInAttackFov(Actor actor)
        {
            Vector2D[] fov = new Vector2D[4];
            for (int i = 0; i < fov.Length; i++)
                fov[i] = Move((Direction)i);
            
            for (int i = 0; i < fov.Length; i++)
            {
                Vector2D pos = fov[i];
                if (pos.X.Equals(actor.Position.X)
                    && pos.Y.Equals(actor.Position.Y))
                    return true;
            }
            return false;
        }
        public bool IsActorInLeftFov(Actor actor)
        {
            Vector2D pos = this.Position + Move(Direction.Left);
            for (int i = 0; i < FovY; i++)
            {
                pos.Y--;
                if (pos.X == actor.Position.X && pos.Y == actor.Position.Y)
                    return true;
            }
            return false;
        }
        
        public bool IsActorInRigthFov(Actor actor)
        {
            Vector2D pos = this.Position + Move(Direction.Right);
            for (int i = 0; i < FovY; i++)
            {
                pos.Y++;
                if (pos.X == actor.Position.X && pos.Y == actor.Position.Y)
                    return true;
            }
            return false;
        }
        
        public bool IsActorInFov(Actor actor)
        {
            return IsActorInUpFov(actor) || IsActorInDownFov(actor) 
                || IsActorInLeftFov(actor) || IsActorInRigthFov(actor);
        }
       


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
