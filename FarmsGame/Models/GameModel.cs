using FarmsGame.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class GameModel
{
    public Player Player { get; private set; }
    public List<GameObject> Objects { get; private set; }
    public int Score { get; private set; }
    public int RemainingTime { get; private set; }

    // Текстуры
    public Image PlayerTexture { get; private set; }
    public Image UsefulItemTexture { get; private set; }
    public Image TrashItemTexture { get; private set; }
    public Image StorageZoneTexture { get; private set; }
    public Image TrashZoneTexture { get; private set; }
    public Image SpeedBoostTexture { get; private set; }

    public event Action<int> TimeUpdated;
    public event Action<int> ScoreUpdated;
    public event Action GameOver;
    public event Action<string> ItemDropped;

    private Random random;
    private Timer gameTimer;

    // Переменная для проверки интервала между шансами выпадения
    private DateTime lastDropCheckTime = DateTime.MinValue;

    public GameModel(int gameDurationInSeconds)
    {
        Player = new Player(new Point(110, 110), new Size(110, 110))
        {
            TextureUp = Image.FromFile("Resources/player_up.png"),
            TextureDown = Image.FromFile("Resources/player_down.png"),
            TextureLeft = Image.FromFile("Resources/player_left.png"),
            TextureRight = Image.FromFile("Resources/player_right.png"),
        };

        Objects = new List<GameObject>
    {
        new StorageZone(new Point(700, 500)),
        new TrashZone(new Point(100, 500))
    };

        UsefulItemTexture = Image.FromFile("Resources/useful_item.png");
        TrashItemTexture = Image.FromFile("Resources/trash_item.png");
        StorageZoneTexture = Image.FromFile("Resources/storage_zone.png");
        TrashZoneTexture = Image.FromFile("Resources/trash_zone.png");
        SpeedBoostTexture = Image.FromFile("Resources/speed_boost.png");

        random = new Random();
        RemainingTime = gameDurationInSeconds;

        StartGameTimer();
    }

    private void StartGameTimer()
    {
        gameTimer = new Timer { Interval = 1000 }; // 1 секунда
        gameTimer.Tick += (sender, e) =>
        {
            RemainingTime--;
            TimeUpdated?.Invoke(RemainingTime);

            if (RemainingTime <= 0)
            {
                gameTimer.Stop();
                GameOver?.Invoke();
            }
        };
        gameTimer.Start();
    }

    public void MovePlayer(int dx, int dy)
    {
        Player.Move(dx, dy);

        foreach (var obj in Objects)
        {
            if (obj is Zone zone && zone.Bounds.IntersectsWith(Player.Bounds))
            {
                if (zone is StorageZone && Player.HeldItem != null)
                {
                    if (Player.HeldItem.Type == ItemType.Useful)
                    {
                        UpdateScore(10);
                    }
                    else
                    {
                        UpdateScore(-5);
                    }
                    Player.DropItem();
                }
                else if (zone is TrashZone && Player.HeldItem != null)
                {
                    if (Player.HeldItem.Type == ItemType.Trash)
                    {
                        UpdateScore(2);
                    }
                    else
                    {
                        UpdateScore(-5);
                    }
                    Player.DropItem();
                }
            }
        }
    }

    private bool ShouldDropItem()
    {
        // Проверяем вероятность выпадения раз в секунду
        if ((DateTime.Now - lastDropCheckTime).TotalSeconds >= 1)
        {
            lastDropCheckTime = DateTime.Now;
            const int dropChance = 10; // 10% вероятность
            return random.Next(100) < dropChance;
        }

        return false;
    }

    public void PickUpItem()
    {
        foreach (var obj in Objects)
        {
            if (obj is Item item && item.Bounds.IntersectsWith(Player.Bounds))
            {
                if (item.Type == ItemType.SpeedBoost)
                {
                    ActivateSpeedBoost();
                }
                else
                {
                    Player.PickUpItem(item);
                }

                Objects.Remove(item);
                break;
            }
        }
    }
    private void ActivateSpeedBoost()
    {
        Player.SetSpeedBoost(15); // Устанавливаем ускорение
        Timer speedBoostTimer = new Timer { Interval = 5000 }; // Буст длится 5 секунд
        speedBoostTimer.Tick += (sender, e) =>
        {
            Player.ResetSpeed();
            speedBoostTimer.Stop();
            speedBoostTimer.Dispose();
        };
        speedBoostTimer.Start();
    }

    public void UpdateScore(int value)
    {
        Score += value;
        ScoreUpdated?.Invoke(Score);
    }

    private void DropHeldItem()
    {
        if (Player.HeldItem != null)
        {
            var droppedItem = new Item(Player.Bounds.Location, new Size(35, 35), Player.HeldItem.Type);
            Objects.Add(droppedItem);

            Player.DropItem();
            ItemDropped?.Invoke("Предмет выпал!");
        }
    }

    public void SpawnItem()
    {
        // Добавляем шанс на спавн бустера скорости (например, 10%)
        var randomType = random.Next(100) < 5 ? ItemType.SpeedBoost :
                         (random.Next(2) == 0 ? ItemType.Useful : ItemType.Trash);

        var position = new Point(random.Next(50, 750), random.Next(50, 550));

        var newItem = new Item(position, new Size(35, 35), randomType);
        Objects.Add(newItem);

        Timer itemRemovalTimer = new Timer { Interval = 10000 }; // Удаление через 10 секунд
        itemRemovalTimer.Tick += (sender, e) =>
        {
            Objects.Remove(newItem);
            itemRemovalTimer.Stop();
            itemRemovalTimer.Dispose();
        };
        itemRemovalTimer.Start();
    }
}
