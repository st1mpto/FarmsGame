using FarmsGame.Models;
using System.Drawing;

public abstract class Zone : GameObject
{
    protected Zone(Point position, Size size, ItemType type) : base(position, size, type) { }
}
