using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class Habit
{
    public string Name { get; set; }
    public bool Completed { get; set; }

    #region JSON Helpers
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

    static void ResetTracker()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Console.WriteLine("\n🌸 Tracker reset successfully! \n");
        }
        else
        {
            Console.WriteLine("\nNo tracker file found to reset. \n");
        }
    }

    #endregion

    static void Main()
    {
        List<Habit> habits = LoadHabits();

        while (true)
        {
            Console.WriteLine("BLOOM TRACKER 🌱");
            Console.WriteLine("1) View habits");
            Console.WriteLine("2) Add a habit");
            Console.WriteLine("3) Mark habit as completed");
            Console.WriteLine("4) Reset tracker");
            Console.WriteLine("0) Exit");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            if (choice == null) Console.WriteLine("Please enter a number.");

            switch (choice)
            {
                case "1":
                    ViewHabits(habits);
                    break;

                case "2":
                    AddHabit(habits);
                    break;

                case "3":
                    MarkHabitCompleted(habits);
                    break;

                case "4":
                    ResetTracker();
                    habits = new List<Habit>();
                    break;

                case "0":
                    SaveHabits(habits);
                    Console.WriteLine("Bye for now 🌼");
                    return;

                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }

            Console.WriteLine();
        }
        #region Methods

        static void ViewHabits(List<Habit> habits)
        {
            if (habits.Count == 0)
            {
                Console.WriteLine("No habits yet. Add one to get started 🌱");
                return;
            }

            Console.WriteLine("\nYour habits:");
            for (int i = 0; i < habits.Count; i++)
            {
                string status = habits[i].Completed ? "✅ Done" : "❌ Not done";
                Console.WriteLine($"{i + 1}) {habits[i].Name} - {status}");
            }
        }

        static void AddHabit(List<Habit> habits)
        {
            //continue step 6
        }

        #endregion
    }
}