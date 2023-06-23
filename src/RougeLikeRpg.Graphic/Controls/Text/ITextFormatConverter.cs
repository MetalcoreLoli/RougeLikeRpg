using System.Collections.Generic;

namespace RougeLikeRpg.Graphic.Controls.Text
{
    public interface ITextFormatConverter
    {
        IEnumerable<Word> Convert();
    }
}