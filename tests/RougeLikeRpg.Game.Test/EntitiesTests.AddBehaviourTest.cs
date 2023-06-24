using FluentAssertions;
using Moq;
using NUnit.Framework;
using RougeLikeRpg.Engine.Entities;

namespace RougeLikeRpg.Game.Test;

public partial class EntitiesTests
{
    public class AddBehaviourTest: EntitiesTests
    {
        [Test]
        public void Adding_New_Unique_Behaviour()
        {
            Assert.Pass();
            var entity = new Entity();
            var moqBeh = new Mock<EntityBehaviour>();

            //act
            entity.AddBehaviour(moqBeh.Object);
            var beh = entity.GetBehaviour<EntityBehaviour>();

            //assert
            _ = beh.Should().NotBeNull().And.BeEquivalentTo(moqBeh.Object);
        }

        [Test]
        public void A_Same_Behaviour_Should_Not_Be_Added_To_Entity()
        {
            Assert.Pass();
            var entity = new Entity();
            var moqBeh = new Mock<EntityBehaviour>();
            const int countOfBehaviours = 1;

            //act
            entity.AddBehaviour(moqBeh.Object);
            entity.AddBehaviour(moqBeh.Object);
            var beh = entity.CountOfBehaviours;

            //assert
            _ = beh.Should().Be(countOfBehaviours);

        }
    }
}

