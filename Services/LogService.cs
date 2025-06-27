using ST10318880_POE1.GUI.Task;

namespace ST10318880_POE1.Services
{
    public class LogService
    {
        private static LogService? _instance;
        public static LogService Instance => _instance ??= new LogService();

        private readonly List<string> _chatMessages = new();
        private readonly List<string> _activities = new();

        // Add this missing field:
        private readonly List<CyberTask> _tasks = new();

        private string? _lastChatbotTopic;

        private LogService() { }

        public void AddActivity(string activityDescription)
        {
            var entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {activityDescription}";
            _activities.Add(entry);
            if (_activities.Count > 100)
                _activities.RemoveAt(0);
        }

        public void AddChatMessage(string message)
        {
            _chatMessages.Add($"{DateTime.Now:HH:mm:ss} - {message}");
            if (_chatMessages.Count > 10)
                _chatMessages.RemoveAt(0);
        }

        public IReadOnlyList<string> GetRecentActivities(int maxCount = 20)
        {
            return _activities.AsEnumerable().Reverse().Take(maxCount).ToList();
        }

        public IReadOnlyList<string> GetLastChatMessages()
        {
            return _chatMessages.AsReadOnly();
        }

        public string? LastChatbotTopic
        {
            get => _lastChatbotTopic;
            set => _lastChatbotTopic = value;
        }

        public void AddTask(CyberTask task)
        {
            _tasks.Add(task);
            AddActivity($"Task added: {task.Title} - {task.Description}");
        }

        public IReadOnlyList<CyberTask> GetTasks()
        {
            return _tasks.AsReadOnly();
        }
    }
}
