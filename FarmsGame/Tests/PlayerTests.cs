using NUnit.Framework;
using FarmsGame.Models;
using System.Drawing;

namespace FarmsGame.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void Move_ChangesPlayerPosition()
        {
            // Arrange
            var player = new Player(new Point(100, 100), new Size(110, 110)); // Указан размер
            var initialPosition = player.Bounds.Location;

            // Act
            player.Move(1, 0); // Move right

            // Assert
            Assert.AreEqual(new Point(initialPosition.X + 10, initialPosition.Y), player.Bounds.Location);
        }

        [Test]
        public void PickUpItem_AssignsHeldItem()
        {
            // Arrange
            var player = new Player(new Point(100, 100), new Size(110, 110)); // Указан размер
            var item = new Item(new Point(100, 100), new Size(20, 20), ItemType.Useful);

            // Act
            player.PickUpItem(item);

            // Assert
            Assert.AreEqual(item, player.HeldItem);
        }

        [Test]
        public void DropItem_ResetsHeldItem()
        {
            // Arrange
            var player = new Player(new Point(100, 100), new Size(110, 110)); // Указан размер
            var item = new Item(new Point(100, 100), new Size(20, 20), ItemType.Useful);

            // Act
            player.PickUpItem(item);
            player.DropItem();

            // Assert
            CustomAssert.IsNull(player.HeldItem);
        }

        [Test]
        public void CannotPickUpMoreThanOneItem()
        {
            // Arrange
            var player = new Player(new Point(100, 100), new Size(110, 110)); // Указан размер
            var item1 = new Item(new Point(100, 100), new Size(20, 20), ItemType.Useful);
            var item2 = new Item(new Point(120, 100), new Size(20, 20), ItemType.Trash);

            // Act
            player.PickUpItem(item1);
            player.PickUpItem(item2);

            // Assert
            Assert.AreEqual(item1, player.HeldItem);
        }
    }
}
