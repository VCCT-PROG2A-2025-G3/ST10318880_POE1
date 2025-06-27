using ST10318880_POE1.GUI.Task;

namespace ST10318880_POE1.Services
{
    // Singleton service responsible for logging chat messages, activities, and tasks
    public class LogService
    {
        private static LogService? _instance;
        public static LogService Instance => _instance ??= new LogService();
        private readonly List<string> _chatMessages = new();
        private readonly List<string> _activities = new();
        private readonly List<CyberTask> _tasks = new();
        private string? _lastChatbotTopic;

        // Private constructor to enforce singleton pattern
        private LogService() { }

        // Adds a new activity entry with a timestamp
        public void AddActivity(string activityDescription)
        {
            var entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {activityDescription}";
            _activities.Add(entry);

            // Limit the number of stored activities to 100
            if (_activities.Count > 100)
                _activities.RemoveAt(0);
        }

        // Adds a chat message with a timestamp
        public void AddChatMessage(string message)
        {
            _chatMessages.Add($"{DateTime.Now:HH:mm:ss} - {message}");

            // Keep only the last 10 messages
            if (_chatMessages.Count > 10)
                _chatMessages.RemoveAt(0);
        }

        // Returns the most recent activities, up to a specified maximum count
        public IReadOnlyList<string> GetRecentActivities(int maxCount = 20)
        {
            return _activities.AsEnumerable().Reverse().Take(maxCount).ToList();
        }

        // Returns the last 10 chat messages (read-only)
        public IReadOnlyList<string> GetLastChatMessages()
        {
            return _chatMessages.AsReadOnly();
        }

        // Stores or retrieves the last topic mentioned by the chatbot
        public string? LastChatbotTopic
        {
            get => _lastChatbotTopic;
            set => _lastChatbotTopic = value;
        }

        // Adds a task and logs its creation as an activity
        public void AddTask(CyberTask task)
        {
            _tasks.Add(task);
            AddActivity($"Task added: {task.Title} - {task.Description}");
        }

        // Returns all stored tasks (read-only)
        public IReadOnlyList<CyberTask> GetTasks()
        {
            return _tasks.AsReadOnly();
        }
    }
}
