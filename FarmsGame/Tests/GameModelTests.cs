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

        [Test]
        public void GameModel_TimerDecrements_EndsGameAtZero()
        {
            var model = new GameModel(3); // 3 seconds
            bool gameOverCalled = false;
            model.GameOver += () => gameOverCalled = true;

            System.Threading.Thread.Sleep(4000); // Wait 4 seconds

            Assert.IsTrue(gameOverCalled);
            Assert.AreEqual(0, model.RemainingTime);
        }
    }
}
