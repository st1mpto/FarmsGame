using System.Drawing;

public class Player
{
    public Rectangle Bounds { get; private set; }
    public GameObject HeldItem { get; private set; }
    private int baseSpeed = 10; // Изначальная скорость
    private float speedModifier = 0.75f; // Модификатор скорости при переноске предмета

    public Player(Point initialPosition)
    {
        Bounds = new Rectangle(initialPosition, new Size(110, 110));
    }

    public void Move(int dx, int dy)
    {
        // Рассчитываем текущую скорость в зависимости от состояния игрока
        int currentSpeed = HeldItem == null ? baseSpeed : (int)(baseSpeed * speedModifier);

        // Обновляем положение игрока
        Bounds = new Rectangle(
            Bounds.X + dx * currentSpeed,
            Bounds.Y + dy * currentSpeed,
            Bounds.Width,
            Bounds.Height
        );
    }

    public void PickUpItem(GameObject item)
    {
        if (HeldItem == null)
        {
            HeldItem = item;
        }
    }

    public void DropItem()
    {
        HeldItem = null;
    }
}
