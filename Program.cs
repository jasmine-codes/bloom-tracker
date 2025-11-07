using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class Habit
{
    public string Name { get; set; }
    public bool Completed { get; set; }

    public int StreakCount { get; set; }

    public DateTime? LastCompletedDate { get; set; }

    static readonly string[] motivationalQuotes =
    {
        "✨ Keep going - small steps lead to big change!",
        "💪 You're showing up for yourself - that's what matters.",
        "🌿 Progress, not perfection.",
        "🔥 Consistency beats motivation every time.",
        "🌞 You did something amazing today!",
        "🧘🏻‍♀️ Take a deep breath - you're doing great."
    };

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

        Console.ReadKey();
    }

    #endregion

    static void Main()
    {
        List<Habit> habits = LoadHabits();
        RefreshHabits(habits);

        while (true)
        {
            Console.Clear();
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

    }

     #region Methods

        static void ViewHabits(List<Habit> habits)
        {
            if (habits.Count == 0)
            {
                Console.WriteLine("No habits yet. Add one to get started 🌱");
            }
            else
            {
                Console.WriteLine("\nYour habits:");
                for (int i = 0; i < habits.Count; i++)
                {
                    string status = habits[i].Completed ? "✅ Done" : "❌ Not done";
                    string streak = habits[i].StreakCount > 0 ? $"{habits[i].StreakCount}-day streak" : "-";
                    Console.WriteLine($"{i + 1}) {habits[i].Name} - {status} ({streak})");
                }
            }

            Console.ReadLine();
        }

        static void AddHabit(List<Habit> habits)
        {
            Console.Write("Enter a new habit name: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Habit can't be empty.");
                return;
            }

            habits.Add(new Habit { Name = name, Completed = false });
            SaveHabits(habits);
            Console.WriteLine($"Added habit: {name} 🌸");

            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
        }

        static void MarkHabitCompleted(List<Habit> habits)
        {
            if (habits.Count == 0)
            {
                Console.WriteLine("No habits to mark. Add some first 🌿");
                return;
            }

            ViewHabits(habits);
            Console.Write("Enter the number of the habit to mark completed: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int index) && index > 0 && index <= habits.Count)
            {
                Habit habit = habits[index - 1];
                DateTime today = DateTime.Today;

                if (habit.LastCompletedDate == today)
                {
                    Console.WriteLine($"{habit.Name} already marked as completed today! 🌼");
                }
                else
                {
                    if (habit.LastCompletedDate == today.AddDays(-1))
                    {
                        habit.StreakCount++;

                    }
                    else
                    {
                        habit.StreakCount = 1;
                    }

                    habit.Completed = true;
                    habit.LastCompletedDate = today;

                    SaveHabits(habits);
                    Console.WriteLine($"marked '{habit.Name}' as completed! ✅");

                }

                Console.WriteLine($"🔥 Current streak: {habit.StreakCount} day(s)!");

            }
            else
            {
                Console.WriteLine("Invalid habit number.");
            }

            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
        }

        static void RefreshHabits(List<Habit> habits)
        {
            DateTime today = DateTime.Today;

            foreach (var habit in habits)
            {
                if (habit.LastCompletedDate != today) habit.Completed = false;
            }

            SaveHabits(habits);
        }

        #endregion
}