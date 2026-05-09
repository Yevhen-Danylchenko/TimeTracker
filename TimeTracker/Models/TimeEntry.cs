namespace TimeTracker.Models
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public CategoryEnum Category { get; set; } = CategoryEnum.Other;
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public DateTime EndTime { get; set; } = DateTime.MinValue;
        public int DurationInMinutes => (int)(EndTime - StartTime).TotalMinutes;
        public string Notes { get; set; } = string.Empty;
    }
}
