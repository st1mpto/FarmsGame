using System;
using System.Windows.Forms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        bool exitGame = false; // Флаг для выхода из игры

        while (!exitGame)
        {
            // Создаём новый экземпляр главного меню
            using (var mainMenu = new MainMenuView())
            {
                var result = mainMenu.ShowDialog();

                if (result == DialogResult.OK) // Начать игру
                {
                    var model = new GameModel(10); // Длительность игры 120 секунд
                    var view = new GameView(model);
                    var controller = new GameController(model, view);

                    view.ShowDialog(); // Запускаем игру
                }
                else if (result == DialogResult.Abort) // Выйти из игры
                {
                    exitGame = true; // Завершаем цикл
                }
            }
        }
    }
}
