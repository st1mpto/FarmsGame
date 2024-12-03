using NUnit.Framework;
using FarmsGame.Models;
using System.Drawing;

namespace FarmsGame.Tests
{
    [TestFixture]
    public class ZoneTests
    {
        [Test]
        public void StorageZone_CorrectlyInitialized()
        {
            var zone = new StorageZone(new Point(700, 500));
            Assert.AreEqual(ItemType.Zone, zone.Type);
            Assert.AreEqual(new Rectangle(new Point(700, 500), new Size(80, 80)), zone.Bounds);
        }

        [Test]
        public void TrashZone_CorrectlyInitialized()
        {
            var zone = new TrashZone(new Point(100, 500));
            Assert.AreEqual(ItemType.Zone, zone.Type);
            Assert.AreEqual(new Rectangle(new Point(100, 500), new Size(60, 60)), zone.Bounds);
        }
    }
}
