using NUnit.Framework;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Actors.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRpg.Tests
{
    public class PlayerBuilderTests
    {
        Player player;
        PlayerBuilder builder;

        [SetUp]
        public void Setup()
        {
            builder = new PlayerBuilder();
        }

        [Test]
        public void BuilderSetNameTest()
        {
            string expectedName = "Trap-chan";
            builder.SetName(expectedName);
            Assert.AreEqual(expectedName, builder.Get().Name);
        }

        [Test]
        public void BuilderSetColor()
        {
            ConsoleColor color = ConsoleColor.White;
            builder.SetColor(color);
            Assert.AreEqual(color, builder.Get().Color);
        }

        [Test]
        public void BuilderSetSymbol()
        {
            char symbol = '@';
            builder.SetSymbol(symbol);
            Assert.AreEqual(symbol, builder.Get().Symbol);
        }

        [Test]
        public void BuilderRollStatsTest()
        {
            builder.RollStats();
            Assert.Less(0, builder.Get().Dex);
        }

        [Test]
        public void BuildPlayer()
        {
            player = builder.Get();
            Assert.AreEqual(player.IsDead, false);
        }
    }
}
