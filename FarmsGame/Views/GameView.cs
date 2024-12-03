using FarmsGame.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

public class GameView : Form
{
    private readonly GameModel model;
    private Label scoreLabel;
    private Label timerLabel;
    private Label notificationLabel;

    public GameView(GameModel model)
    {
        this.model = model;

        this.Text = "Farm Game MVC";
        this.Size = new Size(800, 600);
        this.DoubleBuffered = true; // Включение двойной буферизации

        // Установка фонового изображения
        this.BackgroundImage = Image.FromFile("Resources/background.png");
        this.BackgroundImageLayout = ImageLayout.Stretch; // Растягиваем фон на весь экран

        scoreLabel = new Label
        {
            Text = "Очки: 0",
            AutoSize = true,
            ForeColor = Color.Black,
            Font = new Font("Arial", 14),
            Location = new Point(10, 10)
        };
        this.Controls.Add(scoreLabel);

        timerLabel = new Label
        {
            Text = "Оставшееся время: 60s",
            AutoSize = true,
            ForeColor = Color.Black,
            Font = new Font("Arial", 14),
            Location = new Point(10, 40)
        };
        this.Controls.Add(timerLabel);

        notificationLabel = new Label
        {
            Text = "",
            AutoSize = true,
            ForeColor = Color.White,
            BackColor = Color.Red,
            Font = new Font("Arial", 14, FontStyle.Bold),
            Location = new Point(10, 70),
            Visible = false
        };
        this.Controls.Add(notificationLabel);

        model.ScoreUpdated += UpdateScoreLabel;
        model.TimeUpdated += UpdateTimerLabel;
        model.GameOver += ShowGameOver;
        model.ItemDropped += ShowNotification;

        var updateTimer = new Timer { Interval = 10 }; // Снижена частота обновления до 50 мс
        updateTimer.Tick += (sender, e) => Refresh();
        updateTimer.Start();
    }

    private void UpdateScoreLabel(int score)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => UpdateScoreLabel(score)));
            return;
        }

        scoreLabel.Text = $"Очки: {score}";
    }

    private void UpdateTimerLabel(int remainingTime)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => UpdateTimerLabel(remainingTime)));
            return;
        }

        timerLabel.Text = $"Оставшееся время: {remainingTime}s";
    }

    private void ShowGameOver()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(ShowGameOver));
            return;
        }

        MessageBox.Show($"Игра окончена! Ваши очки: {model.Score}", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
        Application.Exit();
    }

    private void ShowNotification(string message)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => ShowNotification(message)));
            return;
        }

        notificationLabel.Text = message;
        notificationLabel.Visible = true;

        Timer hideNotificationTimer = new Timer { Interval = 2000 };
        hideNotificationTimer.Tick += (s, e) =>
        {
            notificationLabel.Visible = false;
            hideNotificationTimer.Stop();
            hideNotificationTimer.Dispose();
        };
        hideNotificationTimer.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;

        // Отрисовка игрока
        g.DrawImage(model.PlayerTexture, model.Player.Bounds);

        // Отрисовка объектов
        foreach (var obj in model.Objects)
        {
            Image texture = null;

            if (obj is StorageZone)
            {
                texture = model.StorageZoneTexture;
            }
            else if (obj is TrashZone)
            {
                texture = model.TrashZoneTexture;
            }
            else if (obj is Item item)
            {
                if (item.Type == ItemType.Useful)
                {
                    texture = model.UsefulItemTexture;
                }
                else if (item.Type == ItemType.Trash)
                {
                    texture = model.TrashItemTexture;
                }
            }

            if (texture != null)
            {
                g.DrawImage(texture, obj.Bounds);
            }
        }
    }
}
