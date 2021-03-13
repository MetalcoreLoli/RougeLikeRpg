using NUnit.Framework;
using RougeLikeRpg.Graphic.Controls.Text;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Test
{
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



    }
}