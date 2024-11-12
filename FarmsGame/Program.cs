using System;
using System.Drawing;
using System.Windows.Forms;

public class FarmGameForm : Form
{
    private Player player;
    private ItemSpawner itemSpawner;
    private StorageZone storageZone;
    private TrashZone trashZone; // Добавляем урну для мусора
    private ScoreManager scoreManager;
    private Timer gameTimer;
    private int timeRemaining;

    private Label dropMessageLabel; // Метка для отображения сообщения о выпадении предмета

    public FarmGameForm()
    {
        this.Text = "Farm Game";
        this.Size = new Size(800, 600);

        player = new Player();
        this.Controls.Add(player.Farmer);

        storageZone = new StorageZone();
        this.Controls.Add(storageZone.Zone);

        trashZone = new TrashZone(); // Инициализируем урну для мусора
        this.Controls.Add(trashZone.Zone);

        scoreManager = new ScoreManager();
        itemSpawner = new ItemSpawner(this.Controls);

        gameTimer = new Timer { Interval = 1000 };
        gameTimer.Tick += OnGameTick;
        timeRemaining = 120;
        gameTimer.Start();

        var movementTimer = new Timer { Interval = 20 };
        movementTimer.Tick += (sender, e) => player.Move(dropMessageLabel);
        movementTimer.Start();

        dropMessageLabel = new Label
        {
            Text = "",
            AutoSize = true,
            ForeColor = Color.Red,
            Location = new Point(10, 10),
            Visible = false
        };
        this.Controls.Add(dropMessageLabel);

        this.KeyDown += (sender, e) =>
        {
            player.HandleKeyDown(e.KeyCode);
            if (e.KeyCode == Keys.Space) HandleItemInteraction();
        };

        this.KeyUp += (sender, e) => player.HandleKeyUp(e.KeyCode);
    }

    private void OnGameTick(object sender, EventArgs e)
    {
        timeRemaining--;
        if (timeRemaining <= 0)
        {
            gameTimer.Stop();
            MessageBox.Show("Время вышло! Ваш счет: " + scoreManager.Score);
        }
        this.Text = $"Farm Game - Очки: {scoreManager.Score} | Время: {timeRemaining} сек";
    }

    private void HandleItemInteraction()
    {
        if (player.HeldItem == null)
        {
            player.PickUpItem(this.Controls);
        }
        else
        {
            string itemType = player.HeldItem.Tag.ToString();

            // Проверяем, находится ли игрок в зоне хранения или зоне для мусора
            if (storageZone.IsInStorageZone(player.Farmer.Bounds, itemType))
            {
                scoreManager.AddScore(itemType); // Начисляем очки в зависимости от типа предмета
            }
            else if (trashZone.IsInTrashZone(player.Farmer.Bounds, itemType))
            {
                scoreManager.AddScore("TrashZone"); // Начисляем 2 очка за выбрасывание мусора
            }

            // Удаляем предмет после доставки или выброса
            this.Controls.Remove(player.HeldItem);
            player.HeldItem.Dispose();
            player.DropItem();
        }
    }

    [STAThread]
    public static void Main()
    {
        Application.Run(new FarmGameForm());
    }
}
