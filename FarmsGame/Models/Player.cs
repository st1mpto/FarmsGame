using System.Drawing;

public class Player
{
    public Rectangle Bounds { get; private set; }
    public GameObject HeldItem { get; private set; }
    private int speed = 10;

    // Размер игрока
    private Size playerSize;

    // Текущая текстура
    public Image CurrentTexture { get; private set; }

    // Текстуры для каждого направления
    public Image TextureUp { get; set; }
    public Image TextureDown { get; set; }
    public Image TextureLeft { get; set; }
    public Image TextureRight { get; set; }

    public Player(Point initialPosition, Size size)
    {
        playerSize = size;
        Bounds = new Rectangle(initialPosition, playerSize);
        CurrentTexture = TextureDown; // Начальная текстура по умолчанию
    }

    public void Move(int dx, int dy)
    {
        Bounds = new Rectangle(Bounds.X + dx * speed, Bounds.Y + dy * speed, Bounds.Width, Bounds.Height);

        if (dx > 0) CurrentTexture = TextureRight;
        else if (dx < 0) CurrentTexture = TextureLeft;
        else if (dy > 0) CurrentTexture = TextureDown;
        else if (dy < 0) CurrentTexture = TextureUp;
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
    private int defaultSpeed = 10;

    public void SetSpeedBoost(int boostSpeed)
    {
        speed = boostSpeed;
    }

    public void ResetSpeed()
    {
        speed = defaultSpeed;
    }
}
