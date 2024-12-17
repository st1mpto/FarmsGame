using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class MainMenuView : Form
{
    public MainMenuView()
    {
        // Настройки формы
        this.Text = "Farm Game - Главное меню";
        this.Size = new Size(800, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

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
        startButton.Click += (s, e) =>
        {
            this.DialogResult = DialogResult.OK; // Запуск игры
            this.Close();
        };
        this.Controls.Add(startButton);

        // Кнопка "Рекорды"
        var recordsButton = new Button
        {
            Text = "Рекорды",
            Font = new Font("Arial", 16),
            Size = new Size(200, 50),
            Location = new Point(300, 270)
        };
        recordsButton.Click += (s, e) =>
        {
            var records = RecordsManager.LoadRecords();
            var recordsText = string.Join(Environment.NewLine, records.Select(r => $"{r.PlayerName}: {r.Score}"));
            MessageBox.Show(recordsText == "" ? "Рекордов пока нет." : recordsText,
                            "Рекорды", MessageBoxButtons.OK, MessageBoxIcon.Information);
        };
        this.Controls.Add(recordsButton);

        // Кнопка "Выйти"
        var exitButton = new Button
        {
            Text = "Выйти",
            Font = new Font("Arial", 16),
            Size = new Size(200, 50),
            Location = new Point(300, 340)
        };
        exitButton.Click += (s, e) =>
        {
            this.DialogResult = DialogResult.Abort; // Завершение программы
            this.Close();
        };
        this.Controls.Add(exitButton);
    }
}
