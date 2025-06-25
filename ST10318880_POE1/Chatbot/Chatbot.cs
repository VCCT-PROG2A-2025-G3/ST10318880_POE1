namespace ST10318880_POE1
{
    public class Chatbot
    {
        private string? userName;
        private string? favouriteTopic;

        // Bot interface elements
        private readonly string botTag = "🤖: "; // Bot prefix for responses
        private readonly string userTag = "🙋: "; // User prefix for input
        private readonly string divider = new('═', 80); // Visual divider line for separation
        private readonly Random rng = new(); // Random generator for varied responses
        private string? lastTopic = null; // Track last topic for follow-ups

        // Start the chatbot session with the provided user name
        public void Start(string nameInput)
        {
            userName = nameInput;
            DisplayWelcome(userName);

            // Ask user to choose their favorite topic
            Console.WriteLine();
            TypeDelay(botTag + $"Welcome, {userName}! Here are some topics I can talk about:");
            TypeDelay(botTag + "• Password Safety");
            TypeDelay(botTag + "• Phishing");
            TypeDelay(botTag + "• Safe Browsing");
            TypeDelay(botTag + "What’s your favourite topic?");

            // Read user's topic choice
            Console.Write(userTag);
            Console.ForegroundColor = ConsoleColor.Yellow;
            string topicInput = (Console.ReadLine() ?? "").ToLower().Trim();
            Console.ResetColor();

            // Store a simplified version of the user's favorite topic
            if (topicInput.Contains("password"))
                favouriteTopic = "password";
            else if (topicInput.Contains("phishing"))
                favouriteTopic = "phishing";
            else if (topicInput.Contains("browsing") || topicInput.Contains("safe"))
                favouriteTopic = "browsing";
            else
                favouriteTopic = "unknown";

            Console.WriteLine();
            if (favouriteTopic == "unknown")
            {
                TypeDelay(botTag + "Got it! I'll try to help however I can.");
            }
            else
            {
                TypeDelay(
                    botTag
                        + $"Awesome! I'll try to share some tips about {favouriteTopic} during our chat."
                );
            }

            Console.WriteLine(divider);

            // Main chatbot loop
            while (true)
            {
                TypeDelay(botTag + "Ask me something (type 'exit' to quit): ");
                Console.WriteLine();
                Console.Write(userTag);

                Console.ForegroundColor = ConsoleColor.Yellow;
                string input = (Console.ReadLine() ?? "").ToLower().Trim();
                Console.ResetColor();

                // Exit condition
                if (input.Contains("exit") || input.Contains("bye"))
                {
                    TypeDelay(botTag + $"Goodbye {userName}, and stay safe online!");
                    Console.WriteLine(divider);
                    break;
                }
                else if (string.IsNullOrWhiteSpace(input))
                {
                    // Handle empty input
                    TypeDelay(botTag + "I didn't quite catch that. Could you try again?");
                    Console.WriteLine(divider);
                    continue;
                }

                Respond(input);
            }
        }

        // Display a stylized welcome box with the user's name
        private void DisplayWelcome(string name)
        {
            string message = $"Welcome, {name}";
            int totalWidth = 90;
            string paddedMessage =
                $"║{message.PadLeft((totalWidth - 2 + message.Length) / 2).PadRight(totalWidth - 2)}║";
            string border = new string('═', totalWidth - 2);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"╔{border}╗");
            Console.WriteLine(paddedMessage);
            Console.WriteLine($"╚{border}╝");
            Console.ResetColor();
        }

        // Respond to user input by detecting keywords and sentiment
        private void Respond(string input)
        {
            string sentiment = DetectSentiment(input);

            // Detect follow-up intent
            bool isFollowUp =
                input.Contains("more")
                || input.Contains("explain")
                || input.Contains("confused")
                || input.Contains("don’t understand")
                || input.Contains("dont understand")
                || input.Contains("i don't get")
                || input.Contains("what do you mean");

            if (isFollowUp && lastTopic != null)
            {
                HandleFollowUp();
                return;
            }

            // Direct response based on topic keywords
            if (input.Contains("how are you"))
            {
                lastTopic = "how";
                TypeDelay(
                    botTag
                        + GetRandomResponse(
                            new[]
                            {
                                "I'm functioning securely and ready to assist!",
                                "All systems go! I'm here to help.",
                                "I'm feeling quite binary today — 1s and 0s!",
                            }
                        )
                );
            }
            else if (input.Contains("purpose"))
            {
                lastTopic = "purpose";
                TypeDelay(
                    botTag
                        + GetRandomResponse(
                            new[]
                            {
                                "I'm here to teach you about cybersecurity.",
                                "My mission is to make you cybersafe!",
                                "Educating users about digital safety is my core function.",
                            }
                        )
                );
            }
            else if (input.Contains("what can i ask"))
            {
                lastTopic = "topics";
                TypeDelay(
                    botTag
                        + GetRandomResponse(
                            new[]
                            {
                                "You can ask me about password safety, phishing, or safe browsing.",
                            }
                        )
                );
            }
            else if (input.Contains("password"))
            {
                lastTopic = "password";
                TypeDelay(
                    botTag
                        + GetRandomResponse(
                            new[]
                            {
                                "Use long, unique passwords with letters, numbers, and symbols.",
                                "Never reuse passwords and consider using a password manager.",
                                "Strong passwords are your first defense — make them complex and private.",
                            }
                        )
                );
            }
            else if (input.Contains("phishing"))
            {
                lastTopic = "phishing";
                TypeDelay(
                    botTag
                        + GetRandomResponse(
                            new[]
                            {
                                "Phishing tricks you into giving info. Never click suspicious links!",
                                "Beware of fake emails or texts that ask for personal info.",
                                "Always double-check sender details before clicking links or downloading files.",
                            }
                        )
                );
            }
            else if (input.Contains("browsing"))
            {
                lastTopic = "browsing";
                TypeDelay(
                    botTag
                        + GetRandomResponse(
                            new[]
                            {
                                "Use HTTPS and avoid entering personal info on shady websites.",
                                "Stick to reputable websites and always look for the lock icon.",
                                "Secure browsing means using private networks and encrypted sites.",
                            }
                        )
                );
            }
            else
            {
                lastTopic = null;

                // Responses based on sentiment if topic is unknown
                string[] neutralResponses = new[]
                {
                    "I'm not sure how to answer that yet. Try asking about cybersecurity topics!",
                    "Hmm, that's outside my current knowledge. Try a different question.",
                    "Good question! But I’m best with cybersecurity topics right now.",
                };

                string[] frustratedResponses = new[]
                {
                    "I'm really trying my best to help, I promise!",
                    "Let’s take it one step at a time — can you rephrase that?",
                    "I get that this might be frustrating. Let's try another angle.",
                };

                string[] worriedResponses = new[]
                {
                    "Don’t worry, you're not alone. I can guide you.",
                    "Security can be stressful, but you're in the right place to learn.",
                    "Take a breath — I'm here to help you stay safe.",
                };

                string[] curiousResponses = new[]
                {
                    "Great question! I love that you're curious.",
                    "That’s an awesome thing to explore!",
                    "Curiosity is the first step to becoming cyber smart!",
                };

                string[] selected = sentiment switch
                {
                    "frustrated" => frustratedResponses,
                    "worried" => worriedResponses,
                    "curious" => curiousResponses,
                    _ => neutralResponses,
                };

                TypeDelay(botTag + GetRandomResponse(selected, userName));
            }

            Console.WriteLine(divider);
        }

        // Select a random response and personalize it with the user's name if available
        private string GetRandomResponse(string[] options, string? name = null)
        {
            string response = options[rng.Next(options.Length)];
            return name != null ? $"Sure, {name}! {response}" : response;
        }

        // Simulate typing animation effect when displaying bot messages
        private static void TypeDelay(
            string message,
            int delay = 20,
            ConsoleColor color = ConsoleColor.Blue
        )
        {
            Console.ForegroundColor = color;
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        // Handle follow-up questions by referencing the last known topic
        private void HandleFollowUp()
        {
            string[] details = lastTopic switch
            {
                "password" => new[]
                {
                    "You should also change passwords regularly and avoid saving them in your browser.",
                    "Use a password manager to generate and store strong, unique passwords.",
                    "Two-factor authentication (2FA) adds another layer of password security.",
                },
                "phishing" => new[]
                {
                    "Phishing can happen through email, social media, or fake websites.",
                    "Legitimate companies never ask for sensitive info via email.",
                    "Look out for generic greetings like 'Dear user' — it’s a red flag.",
                },
                "browsing" => new[]
                {
                    "Always use antivirus software and keep your browser up to date.",
                    "Avoid clicking on pop-ups or downloading unknown browser extensions.",
                    "Using a VPN helps keep your internet activity private.",
                },
                "purpose" => new[]
                {
                    "I'm designed to simulate a helpful cybersecurity assistant for learning.",
                    "My goal is to raise awareness about staying safe online.",
                    "Think of me as a guide through basic digital security concepts.",
                },
                _ => new[]
                {
                    "Could you clarify what you'd like to know more about?",
                    "I'm not sure how to expand on that yet.",
                    "Please ask another cybersecurity-related question!",
                },
            };

            TypeDelay(botTag + GetRandomResponse(details, userName));
            Console.WriteLine(divider);
        }

        // Basic sentiment analysis based on presence of keywords
        private string DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("nervous"))
                return "worried";
            if (
                input.Contains("confused")
                || input.Contains("not helping")
                || input.Contains("frustrated")
            )
                return "frustrated";
            if (
                input.Contains("how")
                || input.Contains("why")
                || input.Contains("can you explain")
                || input.Contains("?")
            )
                return "curious";

            return "neutral";
        }
    }
}
