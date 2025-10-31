namespace MoodJournalApp
{
    public class MoodEntry
    {
        public string Mood { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public string DisplayText => $"{Date:dd MMM HH:mm} | {Mood}\n{Note}";
    }
}
