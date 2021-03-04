using System;
using System.Security.Cryptography;
using NUnit.Framework;
using RougeLikeRpg.Engine.Core;

namespace RougeLikeRpg.Game.Test
{
    public class EntityWorldSingletonTests
    {
        private EntityWorldSingleton _world;
        [SetUp]
        public void Setup()
        {
            _world = EntityWorldSingleton.Instance;
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
            _world.Registry<string>().AddToWorld(name);
            var actual = _world.FromWorldBy<string>(s => s.Equals("Kate"));

            Assert.That(actual.Equals(name), Is.True);
            Assert.Throws<ArgumentException>(() => _world.AddToWorld(123));
            Assert.Throws<ArgumentException>(() => _world.FromWorldBy<int>(c => c == 123));
        }

        [Test]
        public void FirstFromWorldTest()
        {
            var actual = _world.FirstFromWorldOf<string>();
            var excepted = "Kate";
            Assert.AreEqual(excepted, actual);    
        }


        [Test]
        public void DropFromWorldUndDropFromWorldOfTest()
        {
            var val = _world.Registry<string>().AddToWorld("Kate").FirstFromWorldOf<string>();
            var actual =  _world.DropFromWorld(val).Registry<char>().AddToWorld('9').DropFromAllOf<char>().FromWorldBy<string>(s => s.Equals("Kate"));
            Assert.That(actual == null, Is.True);
            Assert.Throws<ArgumentException>(() => _world.FirstFromWorldOf<char>());
            Assert.Throws<ArgumentException>(() => _world.DropFromAllOf<int>());
        }


        [Test]
        public void ContainsTest()
        {
            var actualValue = _world.Registry<char>().AddToWorld('1').Contains('1');
            var actualType= _world.Registry<char>().AddToWorld('2').Contains<char>();
            var actualFalse = _world.Contains('3');
            
           Assert.That(actualType, Is.True); 
           Assert.That(actualValue, Is.True); 
           Assert.That(actualFalse, Is.False); 
        }
    }
}