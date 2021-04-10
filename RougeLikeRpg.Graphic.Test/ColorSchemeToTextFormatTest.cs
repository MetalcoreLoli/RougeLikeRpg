using System;
using System.Linq;
using NUnit.Framework;
using RougeLikeRpg.Graphic.Controls.Text;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Test
{
    public class ColorSchemeToTextFormatTest
    {
        private ITextColorScheme _scheme;

        [SetUp]
        public void SetUp()
        {
            _scheme = new TemplateTextColorScheme();
            _scheme.Scheme.Add("hello", ColorManager.Green);
            _scheme.Scheme.Add("world", ColorManager.Red);
        }

        [Test]
        public void Convert()
        {
            var formats = new TextFormatConverter(_scheme).Convert().ToArray();
            Assert.AreEqual(ColorManager.Green, formats[0].Color);
            Assert.AreEqual(ColorManager.Red, formats[1].Color);
        }
    }
}