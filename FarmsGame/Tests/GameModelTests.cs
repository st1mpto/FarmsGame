using NUnit.Framework;
using FarmsGame.Models;

namespace FarmsGame.Tests
{
    [TestFixture]
    public class GameModelTests
    {
        [Test]
        public void GameModel_SpawnItem_IncreasesObjectCount()
        {
            var model = new GameModel(60);
            var initialCount = model.Objects.Count;

            model.SpawnItem();

            Assert.AreEqual(initialCount + 1, model.Objects.Count);
        }

        [Test]
        public void GameModel_UpdateScore_ChangesScoreCorrectly()
        {
            var model = new GameModel(60);

            model.UpdateScore(10);
            model.UpdateScore(-5);

            Assert.AreEqual(5, model.Score);
        }
    }
}
