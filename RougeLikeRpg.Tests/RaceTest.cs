using System;
using NUnit.Framework;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Actors.Races;

namespace RougeLikeRpg.Tests
{
    [TestFixture]
    public class RaceTests
    {
        HumanRace race;
        Player actor;
        [SetUp]
        public void Setup()
        {
            race = new HumanRace();
            actor = new Player();
        }

        [Test]
        public void AddRaceModificatorsTest()
        {
            race.AddRaceModificator(actor);
            Assert.AreEqual(3, actor.ChariMod);
        }
    }
}
