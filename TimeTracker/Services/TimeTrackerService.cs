using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Services
{
    public class TimeTrackerService
    {
        private readonly ApplicationDbContext _context;

        public TimeTrackerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddTimeEntry(TimeEntry entry)
        {
            _context.TimeEntries.Add(entry);
            _context.SaveChanges();
        }

        public List<TimeEntry> GetAllTimeEntries()
        {
            return _context.TimeEntries.ToList();
        }

        public Dictionary<string, object> StatisticsChanged()
        {
            var entries = _context.TimeEntries.ToList();

            var totalCount = entries.Count;
            var totalMinutes = entries.Sum(e => e.DurationInMinutes);
            var avgMinutes = totalCount > 0 ? entries.Average(e => e.DurationInMinutes) : 0;

            var byCategory = entries
                .GroupBy(e => e.Category)
                .ToDictionary(g => g.Key.ToString(), g => g.Count());

            return new Dictionary<string, object>
            {
                { "TotalEntries", totalCount },
                { "TotalMinutes", totalMinutes },
                { "AverageMinutes", avgMinutes },
                { "EntriesByCategory", byCategory }
            };
        }

        // Статистика за сьогодні
        public Dictionary<string, object> StatisticsToday()
        {
            var today = DateTime.Today;
            var entries = _context.TimeEntries
                .Where(e => e.StartTime.Date == today)
                .ToList();

            return BuildStats(entries);
        }

        // Статистика за тиждень
        public Dictionary<string, object> StatisticsThisWeek()
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1); // понеділок
            var entries = _context.TimeEntries
                .Where(e => e.StartTime.Date >= startOfWeek)
                .ToList();

            return BuildStats(entries);
        }

        // Фільтр по категорії та даті
        public List<TimeEntry> FilterEntries(CategoryEnum? category, string period)
        {
            var query = _context.TimeEntries.AsQueryable();

            if (category.HasValue)
                query = query.Where(e => e.Category == category.Value);

            if (period == "today")
                query = query.Where(e => e.StartTime.Date == DateTime.Today);
            else if (period == "week")
            {
                var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
                query = query.Where(e => e.StartTime.Date >= startOfWeek);
            }

            return query.ToList();
        }

        private Dictionary<string, object> BuildStats(List<TimeEntry> entries)
        {
            var totalCount = entries.Count;
            var totalMinutes = entries.Sum(e => e.DurationInMinutes);
            var avgMinutes = totalCount > 0 ? entries.Average(e => e.DurationInMinutes) : 0;

            var byCategory = entries
                .GroupBy(e => e.Category)
                .ToDictionary(g => g.Key.ToString(), g => g.Sum(e => e.DurationInMinutes));

            return new Dictionary<string, object>
            {
                { "TotalEntries", totalCount },
                { "TotalMinutes", totalMinutes },
                { "AverageMinutes", avgMinutes },
                { "MinutesByCategory", byCategory }
            };
        }
    }
}
