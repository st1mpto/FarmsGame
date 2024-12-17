using System;
using System.Drawing;
using System.Windows.Forms;

public class MainMenuView : Form
{
    public event Action StartGameClicked;
    public event Action ViewRecordsClicked;
    public event Action ExitClicked;

    public MainMenuView()
    {
        this.Text = "Farm Game - Главное меню";
        this.Size = new Size(800, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;

        // Заголовок
        var titleLabel = new Label
        {
            Text = "Farm Game",
            Font = new Font("Arial", 32, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(300, 100)
        };
        this.Controls.Add(titleLabel);

        // Кнопка "Начать игру"
        var startButton = new Button
        {
            Text = "Начать игру",
            Font = new Font("Arial", 16),
            Size = new Size(200, 50),
            Location = new Point(300, 200)
        };
        startButton.Click += (s, e) => StartGameClicked?.Invoke();
        this.Controls.Add(startButton);

        // Кнопка "Рекорды"
        var recordsButton = new Button
        {
            Text = "Рекорды",
            Font = new Font("Arial", 16),
            Size = new Size(200, 50),
            Location = new Point(300, 270)
        };
        recordsButton.Click += (s, e) => ViewRecordsClicked?.Invoke();
        this.Controls.Add(recordsButton);

        // Кнопка "Выйти"
        var exitButton = new Button
        {
            Text = "Выйти",
            Font = new Font("Arial", 16),
            Size = new Size(200, 50),
            Location = new Point(300, 340)
        };
        exitButton.Click += (s, e) => ExitClicked?.Invoke();
        this.Controls.Add(exitButton);
    }
}
