using NUnit.Framework;
using FarmsGame.Models;
using System.Drawing;

namespace FarmsGame.Tests
{
    [TestFixture]
    public class GameObjectTests
    {
        [Test]
        public void GameObject_InitializesCorrectly()
        {
            var obj = new Item(new Point(200, 200), new Size(20, 20), ItemType.Useful);
            Assert.AreEqual(new Rectangle(new Point(200, 200), new Size(20, 20)), obj.Bounds);
            Assert.AreEqual(ItemType.Useful, obj.Type);
        }
    }
}
