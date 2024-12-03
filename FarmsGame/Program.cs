using System;
using System.Windows.Forms;
using FarmsGame.Tests;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var model = new GameModel(120); // Длительность игры 120 секунд
        var view = new GameView(model);
        var controller = new GameController(model, view);

        Application.Run(view);
    }
}
