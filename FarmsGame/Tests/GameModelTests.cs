using NUnit.Framework;
using FarmsGame.Models;
using System.Drawing;

namespace FarmsGame.Tests
{
    [TestFixture]
    public class GameModelTests
    {
        [Test]
        public void SpawnItem_AddsNewObject()
        {
            var model = new GameModel(60);
            var initialCount = model.Objects.Count;

            model.SpawnItem();
            Assert.AreEqual(initialCount + 1, model.Objects.Count);
        }

        [Test]
        public void UpdateScore_IncreasesScore()
        {
            var model = new GameModel(60);

            model.UpdateScore(10);
            Assert.AreEqual(10, model.Score);

            model.UpdateScore(-5);
            Assert.AreEqual(5, model.Score);
        }

        [Test]
        public void PickUpItem_RemovesItemFromObjects()
        {
            var model = new GameModel(60);
            var item = new Item(new Point(100, 100), new Size(20, 20), ItemType.Useful);
            model.Objects.Add(item);

            model.PickUpItem();
            Assert.IsFalse(model.Objects.Contains(item));
        }

        [Test]
        public static void MovePlayer_UpdatesPlayerPosition()
        {
            var model = new GameModel(60);
            var initialPosition = model.Player.Bounds.Location;

            model.MovePlayer(1, 0); // Move right
            CustomAssert.AreNotEqual(initialPosition, model.Player.Bounds.Location, "Player position should be updated after moving.");
        }
    }
}
