using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class Habit
{
    public string Name { get; set;}
    public bool Completed { get; set; }

static string filePath = "habits.json";

static void SaveHabits(List<Habit> habits)
{
    string json = JsonSerializer.Serialize(habits, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(filePath, json);
}

static List<Habit> LoadHabits()
{
    if (!File.Exists(filePath)) return new List<Habit>();

    string json = File.ReadAllText(filePath);
    return JsonSerializer.Deserialize<List<Habit>>(json);
}
    static void Main()
    {

    }
}