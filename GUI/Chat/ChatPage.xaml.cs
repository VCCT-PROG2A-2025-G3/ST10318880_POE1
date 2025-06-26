using System.Windows;
using System.Windows.Controls;
using ST10318880_POE1.Chatbot;
using ST10318880_POE1.Services;

namespace ST10318880_POE1.GUI.Chat
{
    public partial class ChatPage : Page
    {
        private Chatbot.Chatbot _chatbot;
        private string _userName;
        private readonly LogService _logService;

        public ChatPage(Chatbot.Chatbot chatbot, string userName, LogService logService)
        {
            InitializeComponent();
            _chatbot = chatbot;
            _userName = userName;
            _logService = logService;

            AppendMessage("🤖", $"Hello {_userName}! What's on your mind today?");
            // Log greeting
            _logService.AddChatMessage($"Bot: Hello {_userName}! What's on your mind today?");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = UserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(userMessage))
                return;

            AppendMessage("🙋", userMessage);
            _logService.AddChatMessage($"User: {userMessage}");

            string botReply = _chatbot.GetResponse(userMessage, _userName);
            AppendMessage("🤖", botReply);
            _logService.AddChatMessage($"Bot: {botReply}");

            UserInput.Text = "";
        }

        private void AppendMessage(string senderTag, string message)
        {
            ChatOutput.Text += $"{senderTag}: {message}\n\n";
            ChatScrollViewer.ScrollToEnd();
        }

        private void UserInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SendButton_Click(this, new RoutedEventArgs());
                e.Handled = true;
            }
        }
    }
}
