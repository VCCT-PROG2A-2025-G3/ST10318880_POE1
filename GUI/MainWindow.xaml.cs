using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using ST10318880_POE1.Chatbot;
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

            _chatbot = new Chatbot.Chatbot();

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

            // Pass LogService instance here as well
            MainFrame.Content = new ChatPage(_chatbot, _userName, _logService);
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ChatPage(_chatbot, _userName, _logService);
        }

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TaskPage(_logService);
        }

        private void QuizButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new QuizPage(_logService);
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new LogPage(_logService);
        }
    }
}
