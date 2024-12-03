using FarmsGame.Models;
using System.Drawing;

public class Item : GameObject
{
    public Item(Point position, Size size, ItemType type) : base(position, size, type) { }
}
