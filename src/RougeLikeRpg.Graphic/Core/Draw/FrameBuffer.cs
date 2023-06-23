using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougeLikeRpg.Graphic.Core.Draw
{
    public struct FrameBuffer
    {
        private readonly List<Cell> _cells;
        private FrameBuffer(IEnumerable<Cell> cells)
        {
            _cells = cells.ToList();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            //Console.Write($"{MovedCursorTo(obj.Position)}{SetBackColor(obj)}{SetColor(obj)}{obj.Symbol}\x1b[39m\x1b[39m");
            foreach (var cell in _cells)
            {
                MoveCursorTo(cell.Position, sb).SetColor(cell, sb).SetBackColor(cell, sb);
                sb.Append(cell.Symbol).Append("\x1b[39m\x1b[39m");
            }
            return sb.ToString();
        }


        public FrameBuffer Append(Cell cell) 
        {
            _cells.Add(cell);
            return this;
        }

        private FrameBuffer MoveCursorTo(Vector2D position, StringBuilder buffer)
        {
            buffer.Append($"\x1b[{(position.Y + 1)};{(position.X + 1)}H");
            return this;
        }

        private FrameBuffer SetBackColor(IRenderable obj, StringBuilder buffer) 
        {
            buffer.Append($"\x1b[48;2;{obj.BackColor.Red};{obj.BackColor.Green};{obj.BackColor.Blue}m");
            return this;
        }

        private FrameBuffer SetColor(IRenderable obj, StringBuilder buffer)  { 
            buffer.Append($"\x1b[38;2;{obj.Color.Red};{obj.Color.Green};{obj.Color.Blue}m");
            return this;
        }
        public static FrameBuffer CreateFrom(IEnumerable<Cell> cells) => new FrameBuffer(cells);
    }
}
