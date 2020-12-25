using System;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonFactory
{
    internal class FireDungeonFactory : AbstractFactory
    {
    
        public override Room MakeRoom(int Width, int Height, Vector2D location) 
        {
            var room = new DefaultDungeonFactory().MakeRoom(Width, Height, location);
           
            for(int i =0; i < room.Body.Length;i++)
            {
                room.Body[i].Color = ColorManager.DarkRed;    
            }

            return room;
        }
    }
}
