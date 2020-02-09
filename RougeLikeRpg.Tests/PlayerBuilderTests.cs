using NUnit.Framework;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Actors.Builders;
using RougeLikeRPG.Engine.Actors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRpg.Tests
{
    public class PlayerBuilderTests
    {
        Player player;
        ActorBuilder<Player> builder;

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
        public void BuilderSetRaceTest()
        {
            builder.SetRace(Race.Human);
            Assert.AreEqual(Race.Human, builder.Get().Race);
        }

        [Test]
        public void BuilderSetSymbol()
        {
            char symbol = '@';
            builder.SetSymbol(symbol);
            Assert.AreEqual('@', builder.Get().Symbol);
        }

        [Test]
        public void BuilderRollStatsTest()
        {
            builder.RollStats();
            Assert.Less(0, builder.Get().Dex);
            Assert.Less(0, builder.Get().Str);
            Assert.Less(0, builder.Get().Lucky);
            Assert.Less(0, builder.Get().Intell);
            Assert.Less(0, builder.Get().Chari);
        }
    
        [Test]
        public void BuilderSetFovXY()
        {
            builder.SetFovX(15);
            builder.SetFovY(4);

            Assert.AreEqual(builder.Get().FovX, 15);
            Assert.AreEqual(builder.Get().FovY, 4);
        }

        [Test]
        public void BuildPlayer()
        {
            player = builder.Get();
            Assert.AreEqual(player.IsDead, false);
        }
    }
}
