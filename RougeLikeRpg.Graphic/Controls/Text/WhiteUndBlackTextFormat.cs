using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Controls.Text
{
    public class WhiteUndBlackTextFormat : ITextFormat
    {
        public void Apply(Word word)
        {
            word.Color = ColorManager.White;
            word.BackColor = ColorManager.Black;
        }
    }
}