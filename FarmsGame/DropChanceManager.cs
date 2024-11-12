using System;

public class DropChanceManager
{
    private readonly int dropChance; // Шанс выпадения в процентах
    private readonly Random random;

    public DropChanceManager(int dropChance)
    {
        this.dropChance = dropChance;
        random = new Random();
    }

    // Метод для проверки, должен ли предмет выпасть
    public bool ShouldDrop()
    {
        return random.Next(100) < dropChance;
    }
}
