using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace MoodJournalApp;

public partial class MainPage : ContentPage
{
    private string selectedMood = string.Empty;

    private ObservableCollection<MoodEntry> moodHistory = new();  // ObservableCollection:  dynamic list display -Auto updates UI when data changes


    public ICommand ClearAllCommand { get; } // ICommand Binding Example: used to bind Clear All button action 


    public MainPage()
    {
        //Data Binding setup
        InitializeComponent();
        MoodList.ItemsSource = moodHistory;
        ClearAllCommand = new Command(OnClearAll);
        BindingContext = this; // enables XAML Command binding

        //Jakob’s Law = familiar design using common UI layout (Label + Button + List)
    }
    // UX Law: Fitts’s Law : buttons are large, emoji-based, and easy to tap (target size reduces user effort)
    private void OnMoodSelected(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string mood)
        {
            selectedMood = mood;
            MoodLabel.Text = $"You are {mood} today";
        }
    }
     //UX Law: Hick’s Law: limited set of 5 clear choices (emoji) reduces decision complexity
    private async void OnSaveClicked(object sender, EventArgs e)
    {
         //  Postel’s Law = be liberal in what you accept allow flexible note input but validate mood selection
        if (string.IsNullOrEmpty(selectedMood))
        {
            await DisplayAlert("Missing Mood", "Please select your mood first", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(NoteEditor.Text))
        {
            await DisplayAlert("Empty Note", "Please write something in your journal", "OK");

            return;
        }
        // Auto-timestamp when entry is created
        var entry = new MoodEntry
        {
            Mood = selectedMood,
            Note = NoteEditor.Text,
            Date = DateTime.Now
        };
          // Data Manipulation add new entry to top (newest first)
        moodHistory.Insert(0, entry);

        // Data Persistence Simulation: Only last 7 moods visible
        if (moodHistory.Count > 5)
            moodHistory.RemoveAt(moodHistory.Count - 1);

        SavedNoteLabel.Text = $"Saved: {selectedMood}";
        NoteEditor.Text = string.Empty;
    }

            
    // Tesler’s Law: irreducible complexity handled in code (data validation, timestamping) UI remains simple
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is MoodEntry entry)
            moodHistory.Remove(entry);
    }
    // ICommand Binding Example "Clear All" button in XAML binds here through {Binding ClearAllCommand}
    // Demonstrates command invocation and list reset functionality
    private void OnClearAll()
    {
        moodHistory.Clear();
        SavedNoteLabel.Text = "All entries cleared";
    }
}
