using FarmsGame.Models;
using System.Drawing;

public class TrashZone : Zone
{
    public TrashZone(Point position) : base(position, new Size(60, 60), ItemType.Zone) { }
}
