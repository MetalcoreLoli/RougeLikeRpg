using NUnit.Framework;
using RougeLikeRPG.Core;
using System;

namespace RougeLikeRpg.Tests
{
    public class Tests
    {
        Vector2D vector;
        [SetUp]
        public void Setup()
        {
            vector = new Vector2D(2, 2);
        }

        [Test]
        public void Vector2DScalarMulty()
        {
            Vector2D b = new Vector2D(3, 3);
            Assert.AreEqual(vector * b, 12);
        }

        [Test]
        public void Vector2DMultyOnScalar()
        {
            Assert.AreEqual(vector * 3, new Vector2D(6, 6));
        }
    }
}