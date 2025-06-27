using System.Collections.ObjectModel;
using System.Windows.Controls;
using ST10318880_POE1.Services;

namespace ST10318880_POE1.GUI.Log
{
    public partial class LogPage : Page
    {
        // Collections bound to the UI to display recent activities and chat messages
        public ObservableCollection<string> RecentActivities { get; set; } =
            new ObservableCollection<string>();

        public ObservableCollection<string> RecentChatMessages { get; set; } =
            new ObservableCollection<string>();

        private readonly LogService _logService;

        // Constructor injects the shared log service and initializes the page
        public LogPage(LogService logService)
        {
            InitializeComponent();
            _logService = logService;

            // Populate the collections with the latest log data
            RefreshLogs();

            // Set this page as the binding source for XAML UI
            DataContext = this;
        }

        // Loads the most recent logs into the observable collections for UI binding
        private void RefreshLogs()
        {
            RecentActivities.Clear();
            foreach (var activity in _logService.GetRecentActivities(20))
            {
                RecentActivities.Add(activity); // Add recent user activities
            }

            RecentChatMessages.Clear();
            foreach (var msg in _logService.GetLastChatMessages())
            {
                RecentChatMessages.Add(msg); // Add recent chat interactions
            }
        }
    }
}
