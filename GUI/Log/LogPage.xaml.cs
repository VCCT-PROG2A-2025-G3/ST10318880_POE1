using System.Collections.ObjectModel;
using System.Windows.Controls;
using ST10318880_POE1.Services;

namespace ST10318880_POE1.GUI.Log
{
    public partial class LogPage : Page
    {
        // ObservableCollections for binding to UI
        public ObservableCollection<string> RecentActivities { get; set; } =
            new ObservableCollection<string>();
        public ObservableCollection<string> RecentChatMessages { get; set; } =
            new ObservableCollection<string>();

        private readonly LogService _logService;

        public LogPage(LogService logService)
        {
            InitializeComponent();
            _logService = logService;

            // Load initial data into collections
            RefreshLogs();

            // Set DataContext for binding
            DataContext = this;
        }

        private void RefreshLogs()
        {
            RecentActivities.Clear();
            foreach (var activity in _logService.GetRecentActivities(20))
            {
                RecentActivities.Add(activity);
            }

            RecentChatMessages.Clear();
            foreach (var msg in _logService.GetLastChatMessages())
            {
                RecentChatMessages.Add(msg);
            }
        }
    }
}
