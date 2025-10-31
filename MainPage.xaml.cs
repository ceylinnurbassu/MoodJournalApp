using System.Collections.ObjectModel;

namespace MoodJournalApp;

public partial class MainPage : ContentPage
{
    private string selectedMood = string.Empty;
    private ObservableCollection<string> moodHistory = new();

    public MainPage()
    {
        InitializeComponent();
        MoodList.ItemsSource = moodHistory;
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

        string note = NoteEditor.Text ?? string.Empty;
        string entry = $"{DateTime.Now:HH:mm} - {selectedMood}: {note}";

        // Listeyi otomatik yenileyen koleksiyon
        moodHistory.Insert(0, entry);

        // Sadece son 7 kaydı tut
        if (moodHistory.Count > 7)
            moodHistory.RemoveAt(moodHistory.Count - 1);

        SavedNoteLabel.Text = $"Saved: {selectedMood}";
        NoteEditor.Text = string.Empty;
    }
}
