using System;
using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Graphic.Core;


namespace RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonFactory
{
    internal class DefaultDungeonFactory : AbstractFactory 
    {
        public override Room MakeRoom(int Width, int Height, Vector2D location) 
        {
            var room = new Room(Width, Height, location);
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    int idx = x + Width * y;
                    room.Body[idx].Color = ColorManager.White;
                    if (x == 0)
                        room.Body[idx].Symbol = '║';

                    if (y == 0)
                        room.Body[idx].Symbol = '═';

                    if (y == Height - 1)
                        room.Body[idx].Symbol = '═';

                    if (x == Width - 1)
                        room.Body[idx].Symbol = '║';

                    if (x == 0 && y == 0)
                        room.Body[idx].Symbol = '╔';

                    if (x == 0 && y == Height - 1)
                        room.Body[idx].Symbol = '╚';

                    if (x == Width - 1 && y == Height - 1)
                        room.Body[idx].Symbol = '╝';

                    if (x == Width - 1 && y == 0)
                        room.Body[idx].Symbol = '╗';
                }
            return room;    
        }
    }
}
