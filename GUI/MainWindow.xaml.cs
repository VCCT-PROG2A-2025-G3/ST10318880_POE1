using System.Windows;
using Microsoft.VisualBasic;
using ST10318880_POE1.GUI.Chat;
using ST10318880_POE1.GUI.Log;
using ST10318880_POE1.GUI.Quiz;
using ST10318880_POE1.GUI.Task;
using ST10318880_POE1.Services;

namespace ST10318880_POE1
{
    public partial class MainWindow : Window
    {
        private Chatbot.Chatbot _chatbot;
        private string _userName = "User";
        private readonly LogService _logService = LogService.Instance;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize chatbot with shared log service
            _chatbot = new Chatbot.Chatbot(_logService);

            // Prompt user for name and favorite topic
            string? name = Interaction.InputBox("What is your name?", "User Name", "User");
            if (!string.IsNullOrWhiteSpace(name))
            {
                _userName = name;
                _chatbot.SetUserName(name);
            }

            string? topic = Interaction.InputBox(
                "What's your favourite cybersecurity topic? (e.g. phishing, passwords, browsing)",
                "Favourite Topic",
                ""
            );
            if (!string.IsNullOrWhiteSpace(topic))
            {
                _chatbot.SetFavouriteTopic(topic);
            }

            // Load default view into the frame (Chat view)
            MainFrame.Content = new ChatPage(_chatbot, _userName, _logService);
        }

        // Load Chat page when Chat button is clicked
        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ChatPage(_chatbot, _userName, _logService);
        }

        // Load Task page when Task button is clicked
        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TaskPage(_logService);
        }

        // Load Quiz page when Quiz button is clicked
        private void QuizButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new QuizPage(_logService);
        }

        // Load Log page when Log button is clicked
        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new LogPage(_logService);
        }
    }
}
