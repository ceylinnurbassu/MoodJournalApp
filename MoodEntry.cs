using System.Globalization; 

namespace MoodJournalApp
{
    public class MoodEntry
    {
        public string Mood { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        // Auto formatting automatically formats date and mood for display
        public string DisplayText =>  $"{Date.ToString("dd MMM HH:mm", new CultureInfo("en-US"))} | {Mood}\n{Note}";


        // Aesthetic–Usability effect visually pleasing pastel colors improve perceived usability
        public Color MoodColor => Mood switch
        {
            "Happy" => Color.FromArgb("#FFF4B1"),
            "Sad" => Color.FromArgb("#D6E4F0"),
            "Angry" => Color.FromArgb("#F7C8C8"),
            "Neutral" => Color.FromArgb("#EAEAEA"),
            "Tired" => Color.FromArgb("#EADCF8"),

            _ => Color.FromArgb("#FFFFFF")
        };
    }
}
