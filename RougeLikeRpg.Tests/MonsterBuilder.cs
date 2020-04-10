using System;
using NUnit.Framework;
using RougeLikeRPG.Engine.Actors.Builders;
using RougeLikeRPG.Engine.Actors.Enums;
using RougeLikeRPG.Engine.Actors.Monsters;
using RougeLikeRPG.Engine.Actors.Monsters.Builders;
using RougeLikeRPG.Graphic.Core;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Tests
{
    [TestFixture]
    public class MonsterBuilderTests
    {
        ActorBuilder<Goblin> builder;

        [SetUp]
        public void Setup()
        {
            builder = new MonsterBuilder<Goblin>(); 
        }

        [Test]
        public void MonsterBuilderGetTest()
        {
            var goblin = builder.Get();
            Assert.AreNotEqual(null, goblin);
        }
        
        [Test]
        public void MonsterBuilderSetNameTest()
        {
            string expandName = "Goblin";
            builder.SetName(expandName);
            Assert.AreEqual(expandName, builder.Get().Name);
        }

        [Test]
        public void MonsterBuilderSetSymbol()
        {
            char symbol = 'G';
            builder.SetSymbol(symbol);
            Assert.AreEqual(symbol, builder.Get().Symbol);
        }

        [Test]
        public void BuilderSetRaceTest()
        {
            builder.SetRace(Race.Human);
            Assert.AreEqual(Race.Human, builder.Get().Race);
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
        public void MonsterBuilderSetColorTest()
        {
            builder.SetColor(ColorManager.Green);
            Assert.AreEqual(ColorManager.Green, builder.Get().Color);
        }

        [Test]
        public void MonsterBuilderRollStats()
        {
            builder.RollStats();
            Assert.Less(0, builder.Get().Dex);
        }
    }
}
