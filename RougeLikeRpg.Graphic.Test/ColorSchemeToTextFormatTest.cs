using NUnit.Framework;
using RougeLikeRpg.Graphic.Controls.Text;

namespace RougeLikeRpg.Graphic.Test
{
    public class ColorSchemeToTextFormatTest
    {
        private ITextColorScheme _scheme;

        [SetUp]
        public void SetUp()
        {
            _scheme = new TemplateTextColorScheme();
        }

        [Test]
        public void Adapt()
        {
            Assert.Pass();
        }
    }
}