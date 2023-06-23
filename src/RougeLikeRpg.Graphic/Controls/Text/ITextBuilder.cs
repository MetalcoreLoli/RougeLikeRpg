using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Controls.Text
{
    public interface ITextBuilder
    {
        TextBuilder Append(string testText);
        TextBuilder Append(string testText, ITextFormat format);
        TextBuilder AppendWords(string test);
        Cell[] Construct();
    }
}