using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using ST10318880_POE1.Chatbot;
using ST10318880_POE1.GUI.Chat;
using ST10318880_POE1.GUI.Log;
using ST10318880_POE1.GUI.Quiz;
using ST10318880_POE1.GUI.Task;

namespace ST10318880_POE1
{
    public partial class MainWindow : Window
    {
        private Chatbot.Chatbot _chatbot;
        private string _userName = "User";

        public MainWindow()
        {
            InitializeComponent();

            _chatbot = new Chatbot.Chatbot();

            // Ask for user name
            string? name = Interaction.InputBox("What is your name?", "User Name", "User");
            if (!string.IsNullOrWhiteSpace(name))
            {
                _userName = name;
                _chatbot.SetUserName(name);
            }

            // Ask for favourite topic
            string? topic = Interaction.InputBox(
                "What's your favourite cybersecurity topic? (e.g. phishing, passwords, browsing)",
                "Favourite Topic",
                ""
            );
            if (!string.IsNullOrWhiteSpace(topic))
            {
                _chatbot.SetFavouriteTopic(topic);
            }

            // Load default page
            MainFrame.Content = new ChatPage(_chatbot, _userName);
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ChatPage(_chatbot, _userName);
        }

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TaskPage();
        }

        private void QuizButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new QuizPage();
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new LogPage();
        }
    }
}
