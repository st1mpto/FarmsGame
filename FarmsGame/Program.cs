using System;
using System.Windows.Forms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var mainMenu = new MainMenuView();

        // Обработка событий главного меню
        mainMenu.StartGameClicked += () =>
        {
            mainMenu.Hide();
            var model = new GameModel(120); // Длительность игры 120 секунд
            var view = new GameView(model);
            var controller = new GameController(model, view);

            // Показываем игровую форму
            view.ShowDialog();
            mainMenu.Show(); // После завершения игры возвращаем главное меню
        };

        mainMenu.ViewRecordsClicked += () =>
        {
            MessageBox.Show("Рекорды пока не реализованы!", "Рекорды", MessageBoxButtons.OK, MessageBoxIcon.Information);
        };

        mainMenu.ExitClicked += () =>
        {
            Application.Exit();
        };

        // Запуск главного меню
        Application.Run(mainMenu);
    }
}
