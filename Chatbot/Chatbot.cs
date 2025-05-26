namespace ST10318880_POE1
{
    public class Chatbot
    {
        // Bot visuals
        private readonly string botTag = "🤖: "; // Bot tag prefixed to bot responses
        private readonly string userTag = "🙋: "; // User tag prefixed to user input
        private readonly string divider = new('═', 80); // Line divider

        // Entry point of the chatbot logic
        public void Start(string userName)
        {
            DisplayWelcome(userName); // Show welcome box

            // Conversation loop
            while (true) // Infinite loop until 'exit' or 'bye' is typed
            {
                TypeDelay("\n" + botTag + "Ask me something (type 'exit' to quit): ");
                Console.Write(userTag);

                // User input
                Console.ForegroundColor = ConsoleColor.Yellow;
                string input = (Console.ReadLine() ?? "").ToLower().Trim();
                Console.ResetColor();

                if (input.Contains("exit") || input.Contains("bye"))
                {
                    TypeDelay(botTag + "Stay safe online. Goodbye!");
                    Console.WriteLine(divider);
                    break;
                }
                else if (string.IsNullOrWhiteSpace(input)) // Empty input
                {
                    TypeDelay("\n" + botTag + "I didn't quite catch that. Could you try again?");
                    Console.WriteLine(divider);
                    continue;
                }

                Respond(input); // Respond to user input
            }
        }

        // Welcome Message Box
        private void DisplayWelcome(string name)
        {
            string message = $"Welcome, {name}";
            int totalWidth = 90; // Width of ASCII art box
            string paddedMessage =
                $"║{message.PadLeft((totalWidth - 2 + message.Length) / 2).PadRight(totalWidth - 2)}║";
            string border = new string('═', totalWidth - 2);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"╔{border}╗");
            Console.WriteLine(paddedMessage);
            Console.WriteLine($"╚{border}╝");
            Console.ResetColor();
        }

        // Respond based on keyword detection
        private void Respond(string input)
        {
            if (input.Contains("how are you"))
                TypeDelay(botTag + "I'm functioning securely and ready to answer your questions!");
            else if (input.Contains("purpose"))
                TypeDelay(botTag + "I'm here to teach you about cybersecurity.");
            else if (input.Contains("what can i ask"))
                TypeDelay(
                    botTag + "You can ask me about password safety, phishing, or safe browsing."
                );
            else if (input.Contains("password"))
                TypeDelay(
                    botTag + "Use long, unique passwords with letters, numbers, and symbols."
                );
            else if (input.Contains("phishing"))
                TypeDelay(
                    botTag + "Phishing tricks you into giving info. Never click suspicious links!"
                );
            else if (input.Contains("browsing"))
                TypeDelay(botTag + "Use HTTPS and avoid entering personal info on shady websites.");
            else
                TypeDelay(
                    botTag
                        + "I'm not sure how to answer that yet. Ask me something cybersecurity related."
                );

            Console.WriteLine(divider);
        }

        // Typing effect for chatbot output
        private static void TypeDelay(
            string message,
            int delay = 20,
            ConsoleColor color = ConsoleColor.Blue
        )
        {
            Console.ForegroundColor = color;
            foreach (char c in message)
            {
                Console.Write(c); // Print character
                Thread.Sleep(delay); // Wait for delay to simulate typing
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
