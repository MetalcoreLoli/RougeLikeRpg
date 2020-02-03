using System;
using NUnit.Framework;
using RougeLikeRPG.Engine.Actors.Builders;
using RougeLikeRPG.Engine.Actors.Monsters;
using RougeLikeRPG.Engine.Actors.Monsters.Builders;

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
        public void MonsterBuilderSetColorTest()
        {
            builder.SetColor(ConsoleColor.Green);
            Assert.AreEqual(ConsoleColor.Green, builder.Get().Color);
        }

        [Test]
        public void MonsterBuilderRollStats()
        {
            builder.RollStats();
            Assert.Less(0, builder.Get().Dex);
        }
    }
}
