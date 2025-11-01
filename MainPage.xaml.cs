using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace MoodJournalApp;

public partial class MainPage : ContentPage
{
    private string selectedMood = string.Empty;
    private ObservableCollection<MoodEntry> moodHistory = new();

    public ICommand ClearAllCommand { get; }

    public MainPage()
    {
        InitializeComponent();
        MoodList.ItemsSource = moodHistory;
        ClearAllCommand = new Command(OnClearAll);
        BindingContext = this;
    }

    private void OnMoodSelected(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string mood)
        {
            selectedMood = mood;
            MoodLabel.Text = $"You are feeling {mood} today.";
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(selectedMood))
        {
            await DisplayAlert("Missing Mood", "Please select your mood first!", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(NoteEditor.Text))
        {
            await DisplayAlert("Empty Note", "Please write something in your journal.", "OK");
            return;
        }

        var entry = new MoodEntry
        {
            Mood = selectedMood,
            Note = NoteEditor.Text,
            Date = DateTime.Now
        };

        moodHistory.Insert(0, entry);

        // Only last 7 moods visible
        if (moodHistory.Count > 7)
            moodHistory.RemoveAt(moodHistory.Count - 1);

        SavedNoteLabel.Text = $"Saved: {selectedMood}";
        NoteEditor.Text = string.Empty;
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is MoodEntry entry)
            moodHistory.Remove(entry);
    }

    private void OnClearAll()
    {
        moodHistory.Clear();
        SavedNoteLabel.Text = "All entries cleared!";
    }
}
