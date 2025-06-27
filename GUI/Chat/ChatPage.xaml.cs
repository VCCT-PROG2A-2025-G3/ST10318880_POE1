using System.Windows;
using System.Windows.Controls;
using ST10318880_POE1.Services;

namespace ST10318880_POE1.GUI.Chat
{
    public partial class ChatPage : Page
    {
        private Chatbot.Chatbot _chatbot; // Instance of the chatbot to handle input
        private string _userName;
        private readonly LogService _logService;

        // Constructor for the ChatPage. It initializes components and greets the user.
        public ChatPage(Chatbot.Chatbot chatbot, string userName, LogService logService)
        {
            InitializeComponent();
            _chatbot = chatbot;
            _userName = userName;
            _logService = logService;

            // Display and log the bot's greeting
            AppendMessage("🤖", $"Hello {_userName}! What's on your mind today?");
            _logService.AddChatMessage($"Bot: Hello {_userName}! What's on your mind today?");
        }

        // Triggered when the Send button is clicked
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = UserInput.Text.Trim(); // Get user input
            if (string.IsNullOrWhiteSpace(userMessage))
                return; // Ignore empty input

            AppendMessage("🙋", userMessage); // Show user message in chat
            _logService.AddChatMessage($"User: {userMessage}"); // Log user message

            string botReply = _chatbot.HandleInput(userMessage); // Get bot reply
            AppendMessage("🤖", botReply); // Show bot response in chat
            _logService.AddChatMessage($"Bot: {botReply}"); // Log bot response

            UserInput.Text = ""; // Clear input box
        }

        // Appends a message to the chat output area
        private void AppendMessage(string senderTag, string message)
        {
            ChatOutput.Text += $"{senderTag}: {message}\n\n"; // Append message
            ChatScrollViewer.ScrollToEnd(); // Auto-scroll to latest message
        }

        // Allows Enter key to send message (keyboard shortcut)
        private void UserInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SendButton_Click(this, new RoutedEventArgs()); // Trigger send
                e.Handled = true; // Prevent default Enter behavior
            }
        }
    }
}
