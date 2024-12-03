using FarmsGame.Models;
using System.Drawing;

public class StorageZone : Zone
{
    public StorageZone(Point position) : base(position, new Size(80, 80), ItemType.Zone) { }
}
