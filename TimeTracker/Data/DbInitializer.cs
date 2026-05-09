using TimeTracker.Models;

namespace TimeTracker.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();
            // Check if there are any time entries already in the database
            if (context.TimeEntries.Any())
            {
                return; // Database has been seeded
            }
            // Seed the database with some initial time entries
            var timeEntries = new TimeEntry[]
            {
                // Learning
                new TimeEntry { TaskName = "Online Course", Category = CategoryEnum.Learning, StartTime = DateTime.Now.AddHours(-10), EndTime = DateTime.Now.AddHours(-9), Notes = "Studying C# basics" },
                new TimeEntry { TaskName = "Language Practice", Category = CategoryEnum.Learning, StartTime = DateTime.Now.AddHours(-8), EndTime = DateTime.Now.AddHours(-7), Notes = "Practicing English vocabulary" },
                new TimeEntry { TaskName = "Research Paper", Category = CategoryEnum.Learning, StartTime = DateTime.Now.AddHours(-6), EndTime = DateTime.Now.AddHours(-5), Notes = "Reading academic article" },

                // Work
                new TimeEntry { TaskName = "Team Meeting", Category = CategoryEnum.Work, StartTime = DateTime.Now.AddHours(-5), EndTime = DateTime.Now.AddHours(-4), Notes = "Weekly sync with team" },
                new TimeEntry { TaskName = "Coding Feature", Category = CategoryEnum.Work, StartTime = DateTime.Now.AddHours(-3), EndTime = DateTime.Now.AddHours(-2), Notes = "Implementing new module" },
                new TimeEntry { TaskName = "Client Call", Category = CategoryEnum.Work, StartTime = DateTime.Now.AddHours(-1), EndTime = DateTime.Now, Notes = "Discussing requirements" },

                // Rest
                new TimeEntry { TaskName = "Nap", Category = CategoryEnum.Rest, StartTime = DateTime.Now.AddHours(-7), EndTime = DateTime.Now.AddHours(-6), Notes = "Short rest" },
                new TimeEntry { TaskName = "Watching Movie", Category = CategoryEnum.Rest, StartTime = DateTime.Now.AddHours(-2), EndTime = DateTime.Now.AddHours(-1), Notes = "Relaxing with a film" },
                new TimeEntry { TaskName = "Meditation", Category = CategoryEnum.Rest, StartTime = DateTime.Now.AddHours(-9), EndTime = DateTime.Now.AddHours(-8), Notes = "Mindfulness practice" },

                // Sport
                new TimeEntry { TaskName = "Morning Run", Category = CategoryEnum.Sport, StartTime = DateTime.Now.AddHours(-12), EndTime = DateTime.Now.AddHours(-11), Notes = "5 km jog" },
                new TimeEntry { TaskName = "Gym Workout", Category = CategoryEnum.Sport, StartTime = DateTime.Now.AddHours(-4), EndTime = DateTime.Now.AddHours(-3), Notes = "Strength training" },
                new TimeEntry { TaskName = "Yoga Session", Category = CategoryEnum.Sport, StartTime = DateTime.Now.AddHours(-2), EndTime = DateTime.Now.AddHours(-1), Notes = "Stretching and balance" },

                // Other
                new TimeEntry { TaskName = "Grocery Shopping", Category = CategoryEnum.Other, StartTime = DateTime.Now.AddHours(-6), EndTime = DateTime.Now.AddHours(-5), Notes = "Buying food" },
                new TimeEntry { TaskName = "House Cleaning", Category = CategoryEnum.Other, StartTime = DateTime.Now.AddHours(-3), EndTime = DateTime.Now.AddHours(-2), Notes = "Tidying up rooms" },
                new TimeEntry { TaskName = "Family Time", Category = CategoryEnum.Other, StartTime = DateTime.Now.AddHours(-1), EndTime = DateTime.Now, Notes = "Dinner with family" }
            };

            foreach (var entry in timeEntries)
            {
                context.TimeEntries.Add(entry);
            }
            context.SaveChanges();
        }
    }
}
