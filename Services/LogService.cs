using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10318880_POE1.Services
{
    public class LogService
    {
        private static LogService? _instance;
        public static LogService Instance => _instance ??= new LogService();

        private readonly List<string> _chatMessages = new();
        private readonly List<string> _activities = new();

        private string? _lastChatbotTopic;

        private LogService() { }

        // Add a user activity description (e.g., task created, reminder set, quiz attempted)
        public void AddActivity(string activityDescription)
        {
            var entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {activityDescription}";
            _activities.Add(entry);
            // Optional: keep activities list trimmed (e.g., last 100 entries)
            if (_activities.Count > 100)
                _activities.RemoveAt(0);
        }

        // Add a chat message (user or bot)
        public void AddChatMessage(string message)
        {
            _chatMessages.Add($"{DateTime.Now:HH:mm:ss} - {message}");
            // Keep only last 10 messages
            if (_chatMessages.Count > 10)
                _chatMessages.RemoveAt(0);
        }

        // Get recent activities (newest first)
        public IReadOnlyList<string> GetRecentActivities(int maxCount = 20)
        {
            return _activities.AsEnumerable().Reverse().Take(maxCount).ToList();
        }

        // Get last chat messages (in chronological order)
        public IReadOnlyList<string> GetLastChatMessages()
        {
            return _chatMessages.AsReadOnly();
        }

        // Get or set the last chatbot topic or keyword identified
        public string? LastChatbotTopic
        {
            get => _lastChatbotTopic;
            set => _lastChatbotTopic = value;
        }
    }
}
