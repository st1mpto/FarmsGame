using FarmsGame.Models;
using System;
using System.Drawing;
using System.Media; // Подключаем для работы с музыкой
using System.Windows.Forms;

public class GameView : Form
{
    private readonly GameModel model;
    private Label scoreLabel;
    private Label timerLabel;
    private Label notificationLabel;

    // Поле для проигрывания музыки
    private SoundPlayer backgroundMusic;

    public GameView(GameModel model)
    {
        this.model = model;

        this.Text = "Farm Game MVC";
        this.Size = new Size(800, 600);
        this.DoubleBuffered = true; // Включение двойной буферизации

        // Установка фонового изображения
        this.BackgroundImage = Image.FromFile("Resources/background.png");
        this.BackgroundImageLayout = ImageLayout.Stretch; // Растягиваем фон на весь экран

        InitializeLabels();
        InitializeMusic(); // Добавление музыки

        model.ScoreUpdated += UpdateScoreLabel;
        model.TimeUpdated += UpdateTimerLabel;
        model.GameOver += ShowGameOver;
        model.ItemDropped += ShowNotification;

        var updateTimer = new Timer { Interval = 10 }; // Частота обновления экрана
        updateTimer.Tick += (sender, e) => Refresh();
        updateTimer.Start();
    }

    private void InitializeMusic()
    {
        try
        {
            // Указываем путь к музыкальному файлу
            string musicPath = "Resources/background_music.wav";
            backgroundMusic = new SoundPlayer(musicPath);
            backgroundMusic.PlayLooping(); // Запуск музыки в цикле
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка воспроизведения музыки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void InitializeLabels()
    {
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
        if (model.Player.CurrentTexture != null)
        {
            g.DrawImage(model.Player.CurrentTexture, model.Player.Bounds);
        }

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
                switch (item.Type)
                {
                    case ItemType.Useful:
                        texture = model.UsefulItemTexture;
                        break;
                    case ItemType.Trash:
                        texture = model.TrashItemTexture;
                        break;
                    case ItemType.SpeedBoost:
                        texture = model.SpeedBoostTexture;
                        break;
                }
            }

            if (texture != null)
            {
                g.DrawImage(texture, obj.Bounds);
            }
        }
    }
}
