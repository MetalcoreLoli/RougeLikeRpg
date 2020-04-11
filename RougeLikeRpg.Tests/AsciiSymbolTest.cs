using NUnit.Framework;
using RougeLikeRpg.Graphic.Core;
using System;

namespace RougeLikeRpg.Tests
{
    [TestFixture]
    public class AsciiSymbolTest
    {
        [Test]
        public void AsciiSymbolFromCodeTest()
        {
            AsciiSymbol symbol = 64;
            char character = '@';
            Assert.AreEqual(character, symbol.Symbol);
        }


        [Test]
        public void AsciiSymbolFromCharTest()
        {
            AsciiSymbol symbol = '!';
            char character = '!';

            Assert.AreEqual((UInt16)character, symbol.Code);
        }
    }
}
