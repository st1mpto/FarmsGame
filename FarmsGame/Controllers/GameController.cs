using System.Windows.Forms;
using System.Collections.Generic;


public class GameController
{
    private readonly GameModel model;
    private readonly HashSet<Keys> pressedKeys;

    public GameController(GameModel model, GameView view)
    {
        this.model = model;
        pressedKeys = new HashSet<Keys>();

        view.KeyDown += (sender, e) =>
        {
            pressedKeys.Add(e.KeyCode);

            if (e.KeyCode == Keys.Space)
            {
                model.PickUpItem();
            }
        };

        view.KeyUp += (sender, e) =>
        {
            pressedKeys.Remove(e.KeyCode);
        };

        var movementTimer = new Timer { Interval = 20 };
        movementTimer.Tick += (sender, e) => HandleMovement();
        movementTimer.Start();

        var spawnTimer = new Timer { Interval = 2500 }; // Спавн каждые 2.5 секунды
        spawnTimer.Tick += (sender, e) => model.SpawnItem();
        spawnTimer.Start();
    }

    private void HandleMovement()
    {
        int dx = 0, dy = 0;

        if (pressedKeys.Contains(Keys.W) || pressedKeys.Contains(Keys.Up)) dy = -1;
        if (pressedKeys.Contains(Keys.S) || pressedKeys.Contains(Keys.Down)) dy = 1;
        if (pressedKeys.Contains(Keys.A) || pressedKeys.Contains(Keys.Left)) dx = -1;
        if (pressedKeys.Contains(Keys.D) || pressedKeys.Contains(Keys.Right)) dx = 1;

        if (dx != 0 || dy != 0)
        {
            model.MovePlayer(dx, dy);
        }
    }
}
