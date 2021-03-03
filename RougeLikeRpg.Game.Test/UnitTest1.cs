using System;
using NUnit.Framework;
using RougeLikeRpg.Engine.Core;

namespace RougeLikeRpg.Game.Test
{
    public class EntityWorldSingletonTests
    {
        [SetUp]
        public void Setup()
        {
            var entityWorldSingleton = EntityWorldSingleton.Instance;
        }

        [Test]
        public void InstanceTest()
        {
            var cache = EntityWorldSingleton.Instance;
            var cacheOne = EntityWorldSingleton.Instance;
            Assert.That(ReferenceEquals(cache, cacheOne), Is.True);
        }

        [Test]
        public void AddToWorldUndFromWorldTest()
        {
            string name = "Kate";
            var world = EntityWorldSingleton.Instance.Registry<string>().AddToWorld(name);
            var actual = world.FromWorldBy<string>(s => s.Equals("name"));

            Assert.That(actual.Equals(name), Is.True);
        }
    }
}