using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class RecordsManager
{
    private static readonly string RecordsFile = "records.txt";

    // Метод для сохранения нового рекорда
    public static void SaveRecord(string playerName, int score)
    {
        var records = LoadRecords();
        records.Add(new Record(playerName, score));
        records = records.OrderByDescending(r => r.Score).ToList();

        // Сохраняем рекорды в файл
        using (var writer = new StreamWriter(RecordsFile))
        {
            foreach (var record in records)
            {
                writer.WriteLine($"{record.PlayerName}:{record.Score}");
            }
        }
    }

    // Метод для загрузки рекордов
    public static List<Record> LoadRecords()
    {
        var records = new List<Record>();

        if (File.Exists(RecordsFile))
        {
            var lines = File.ReadAllLines(RecordsFile);
            foreach (var line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1], out var score))
                {
                    records.Add(new Record(parts[0], score));
                }
            }
        }

        return records;
    }
}

public class Record
{
    public string PlayerName { get; }
    public int Score { get; }

    public Record(string playerName, int score)
    {
        PlayerName = playerName;
        Score = score;
    }
}
