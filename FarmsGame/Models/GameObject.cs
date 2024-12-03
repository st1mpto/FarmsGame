using FarmsGame.Models;
using System.Drawing;

public class GameObject
{
    public Rectangle Bounds { get; private set; }
    public ItemType Type { get; private set; }

    public GameObject(Point position, Size size, ItemType type)
    {
        Bounds = new Rectangle(position, size);
        Type = type;
    }
}
