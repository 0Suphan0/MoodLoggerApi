namespace MoodLoggerApi.Models
{
    public class MoodLog
    {
        public int Id { get; set; }
        public string Mood { get; set; } = null!; 
        public string? Note { get; set; }
        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
