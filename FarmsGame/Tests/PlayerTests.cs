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
            var player = new Player(new Point(100, 100));
            var initialPosition = player.Bounds.Location;

            player.Move(1, 0); // Move right
            Assert.AreEqual(new Point(initialPosition.X + 10, initialPosition.Y), player.Bounds.Location);
        }

        [Test]
        public void PickUpItem_AssignsHeldItem()
        {
            var player = new Player(new Point(100, 100));
            var item = new Item(new Point(100, 100), new Size(20, 20), ItemType.Useful);

            player.PickUpItem(item);
            Assert.AreEqual(item, player.HeldItem);
        }

        [Test]
        public static void DropItem_ResetsHeldItem()
        {
            var player = new Player(new Point(100, 100));
            var item = new Item(new Point(100, 100), new Size(20, 20), ItemType.Useful);

            player.PickUpItem(item);
            player.DropItem();
            CustomAssert.IsNull(player.HeldItem, "HeldItem should be null after dropping.");
        }

        [Test]
        public void CannotPickUpMoreThanOneItem()
        {
            var player = new Player(new Point(100, 100));
            var item1 = new Item(new Point(100, 100), new Size(20, 20), ItemType.Useful);
            var item2 = new Item(new Point(120, 100), new Size(20, 20), ItemType.Trash);

            player.PickUpItem(item1);
            player.PickUpItem(item2);
            Assert.AreEqual(item1, player.HeldItem);
        }
    }
}
