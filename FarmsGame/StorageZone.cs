using System.Drawing;
using System.Windows.Forms;

public class StorageZone
{
    public PictureBox Zone { get; private set; }

    public StorageZone()
    {
        Zone = new PictureBox
        {
            BackColor = Color.Blue,
            Size = new Size(80, 80),
            Location = new Point(700, 500),
            Tag = "StorageZone" // Устанавливаем уникальный тег для зоны хранения
        };
    }

    public bool IsInStorageZone(Rectangle farmerBounds, string itemType)
    {
        return farmerBounds.IntersectsWith(Zone.Bounds);
    }
}
