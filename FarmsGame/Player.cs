using System;
using System.Drawing;
using System.Windows.Forms;

public class Player
{
    public PictureBox Farmer { get; private set; }
    public PictureBox HeldItem { get; private set; }
    private DropChanceManager dropChanceManager; // Экземпляр DropChanceManager
    private DateTime pickupTime;       // Время последнего подбора предмета
    private DateTime lastDropCheck;    // Время последней проверки на выпадение

    private readonly int baseSpeed = 10; // Базовая скорость фермера
    private int currentSpeed;            // Текущая скорость фермера

    // Переменные для отслеживания состояния клавиш
    private bool isMovingUp;
    private bool isMovingDown;
    private bool isMovingLeft;
    private bool isMovingRight;

    public Player()
    {
        Farmer = new PictureBox
        {
            BackColor = Color.Green,
            Size = new Size(30, 30),
            Location = new Point(100, 100)
        };

        // Инициализируем DropChanceManager с шансом выпадения 7%
        dropChanceManager = new DropChanceManager(7);
        currentSpeed = baseSpeed; // Начальная скорость равна базовой
    }

    public void HandleKeyDown(Keys key)
    {
        switch (key)
        {
            case Keys.W:
            case Keys.Up:
                isMovingUp = true;
                break;
            case Keys.S:
            case Keys.Down:
                isMovingDown = true;
                break;
            case Keys.A:
            case Keys.Left:
                isMovingLeft = true;
                break;
            case Keys.D:
            case Keys.Right:
                isMovingRight = true;
                break;
        }
    }

    public void HandleKeyUp(Keys key)
    {
        switch (key)
        {
            case Keys.W:
            case Keys.Up:
                isMovingUp = false;
                break;
            case Keys.S:
            case Keys.Down:
                isMovingDown = false;
                break;
            case Keys.A:
            case Keys.Left:
                isMovingLeft = false;
                break;
            case Keys.D:
            case Keys.Right:
                isMovingRight = false;
                break;
        }
    }

    public void Move(Label dropMessageLabel)
    {
        // Уменьшаем скорость на 25%, если фермер несет предмет
        currentSpeed = HeldItem == null ? baseSpeed : (int)(baseSpeed * 0.75);

        int dx = 0;
        int dy = 0;

        if (isMovingUp) dy -= currentSpeed;
        if (isMovingDown) dy += currentSpeed;
        if (isMovingLeft) dx -= currentSpeed;
        if (isMovingRight) dx += currentSpeed;

        Farmer.Left += dx;
        Farmer.Top += dy;

        if (Farmer.Left < 0) Farmer.Left = 0;
        if (Farmer.Top < 0) Farmer.Top = 0;
        if (Farmer.Right > Farmer.Parent.ClientSize.Width) Farmer.Left = Farmer.Parent.ClientSize.Width - Farmer.Width;
        if (Farmer.Bottom > Farmer.Parent.ClientSize.Height) Farmer.Top = Farmer.Parent.ClientSize.Height - Farmer.Height;

        // Проверка выпадения только если с момента подбора прошло больше 2 секунд
        if (HeldItem != null && (DateTime.Now - pickupTime).TotalSeconds >= 2)
        {
            // Проверка выпадения предмета каждые 2 секунды
            if ((DateTime.Now - lastDropCheck).TotalSeconds >= 2)
            {
                lastDropCheck = DateTime.Now; // Обновляем время последней проверки

                // Используем DropChanceManager для проверки выпадения
                if (dropChanceManager.ShouldDrop())
                {
                    HeldItem = null;
                    ShowDropMessage(dropMessageLabel);
                }
            }
        }

        // Перемещение предмета с фермером, если он его несет
        if (HeldItem != null)
        {
            HeldItem.Left = Farmer.Left;
            HeldItem.Top = Farmer.Top - HeldItem.Height;
        }
    }

    public void PickUpItem(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            if (control is PictureBox item && item != Farmer && item.Tag?.ToString() != "StorageZone" && item.Bounds.IntersectsWith(Farmer.Bounds))
            {
                HeldItem = item;
                pickupTime = DateTime.Now; // Запоминаем время поднятия предмета
                lastDropCheck = DateTime.Now; // Обновляем последнее время проверки на выпадение
                break;
            }
        }
    }

    private void ShowDropMessage(Label dropMessageLabel)
    {
        dropMessageLabel.Text = "Предмет выпал!";
        dropMessageLabel.Visible = true;

        Timer messageTimer = new Timer { Interval = 1000 };
        messageTimer.Tick += (s, e) =>
        {
            dropMessageLabel.Visible = false;
            messageTimer.Stop();
            messageTimer.Dispose();
        };
        messageTimer.Start();
    }

    public void DropItem()
    {
        HeldItem = null;
    }
}
