namespace MoodJournalApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new MainPage(); // Shell yok, direkt sayfa
    }
}
