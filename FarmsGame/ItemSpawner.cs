using System;
using System.Drawing;
using System.Windows.Forms;

public class ItemSpawner
{
    private Timer itemSpawnTimer;
    private Control.ControlCollection controls;

    public ItemSpawner(Control.ControlCollection controls)
    {
        this.controls = controls;

        itemSpawnTimer = new Timer { Interval = 3000 }; // Спавн каждые 3 секунды
        itemSpawnTimer.Tick += (sender, e) => SpawnItem();
        itemSpawnTimer.Start();
    }

    private void SpawnItem()
    {
        // Создаем новый PictureBox для предмета
        var item = new PictureBox
        {
            BackColor = new Random().Next(2) == 0 ? Color.Red : Color.Gray, // Полезный предмет или мусор
            Size = new Size(20, 20),
            Location = new Point(new Random().Next(0, 800 - 20), new Random().Next(0, 600 - 20))
        };

        // Присваиваем Tag после создания
        item.Tag = item.BackColor == Color.Red ? "Useful" : "Trash"; // Устанавливаем правильный тег
        controls.Add(item);
    }
}
