using NUnit.Framework;
using RougeLikeRpg.Graphic.Controls.Text;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Test
{
    internal class TestGreenWordFormat : ITextFormat
    {
        public void Apply(Word word)
        {
            word.Color = ColorManager.Green;
        }
    }

    internal class TestWhiteWordFormat : ITextFormat
    {
        public void Apply(Word word)
        {
            word.Color = ColorManager.White;
        }
    }
    public class TextBuilderTest
    {
        private TextBuilder _builder;

        [SetUp]
        public void Setup()
        {
            _builder = new TextBuilder();
        }

        [Test]
        public  void AppendTest()
        {
            string testText = "test";
            Cell[] text = _builder.Append(testText).Construct ();

            Assert.That(text.Length == testText.Length, Is.True);
            
            for (int i = 0; i < testText.Length; i++)
            {
                Assert.That(text[i].Symbol == testText[i], Is.True);
            }
        }

        [Test]
        public void AppendRangeTest()
        {
            string test = "hello world";
            var text = _builder.AppendWords(test).Construct();
            
            Assert.AreEqual(test.Length,  text.Length);
            
            for (int i = 0; i < test.Length; i++)
            {
                Assert.AreEqual( test[i], text[i].Symbol);
            }
        }

        [Test]
        public void FormattedTextAppendTest()
        {
            const string greenText = "Hello";
            const string whiteText = "World";
            var greenUndWhiteText = 
                _builder.Append(greenText, new TestGreenWordFormat())
                        .Append(whiteText, new TestWhiteWordFormat())
                        .Construct();

            for (int i = 0; i < greenText.Length; i++)
                Assert.AreEqual(ColorManager.Green, greenUndWhiteText[i].Color);

            for (int i = whiteText.Length + 1; i < whiteText.Length + greenText.Length - 1; i++)
                Assert.AreEqual(ColorManager.White, greenUndWhiteText[i].Color); 
        }
    }
}