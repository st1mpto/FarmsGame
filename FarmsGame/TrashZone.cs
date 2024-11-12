using System.Drawing;
using System.Windows.Forms;

public class TrashZone
{
    public PictureBox Zone { get; private set; }

    public TrashZone()
    {
        Zone = new PictureBox
        {
            BackColor = Color.Brown, // Выберите цвет для урны
            Size = new Size(60, 60),
            Location = new Point(100, 500), // Задайте положение урны на форме
            Tag = "TrashZone" // Устанавливаем уникальный тег для урны
        };
    }

    public bool IsInTrashZone(Rectangle farmerBounds, string itemType)
    {
        return farmerBounds.IntersectsWith(Zone.Bounds) && itemType == "Trash";
    }
}
