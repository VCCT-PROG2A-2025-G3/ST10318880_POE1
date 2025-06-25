namespace ST10318880_POE1.Chatbot
{
    public class Chatbot
    {
        private string? userName;
        private string? favouriteTopic;
        private readonly Random rng = new();
        private string? lastTopic = null;

        public void SetUserName(string name)
        {
            userName = name;
        }

        public void SetFavouriteTopic(string topicInput)
        {
            topicInput = topicInput.ToLower().Trim();

            if (topicInput.Contains("password"))
                favouriteTopic = "password";
            else if (topicInput.Contains("phishing"))
                favouriteTopic = "phishing";
            else if (topicInput.Contains("browsing") || topicInput.Contains("safe"))
                favouriteTopic = "browsing";
            else
                favouriteTopic = "unknown";
        }

        public string GetResponse(string input, string userName)
        {
            return GenerateResponse(input, userName);
        }

        private string GenerateResponse(string input, string userName)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "I didn't catch that. Could you repeat it?";

            string lower = input.ToLower();

            // Match topics
            if (lower.Contains("how are you"))
            {
                return GetRandomResponse(
                    new[]
                    {
                        "I'm functioning securely and ready to assist!",
                        "All systems go! I'm here to help.",
                        "I'm feeling quite binary today — 1s and 0s!",
                    },
                    userName
                );
            }
            else if (lower.Contains("purpose"))
            {
                return GetRandomResponse(
                    new[]
                    {
                        "I'm here to teach you about cybersecurity.",
                        "My mission is to make you cybersafe!",
                        "Educating users about digital safety is my core function.",
                    },
                    userName
                );
            }
            else if (lower.Contains("what can i ask"))
            {
                return "You can ask me about password safety, phishing, or safe browsing.";
            }
            else if (lower.Contains("password"))
            {
                return GetRandomResponse(
                    new[]
                    {
                        "Use long, unique passwords with letters, numbers, and symbols.",
                        "Never reuse passwords and consider using a password manager.",
                        "Strong passwords are your first defense — make them complex and private.",
                    },
                    userName
                );
            }
            else if (lower.Contains("phishing"))
            {
                return GetRandomResponse(
                    new[]
                    {
                        "Phishing tricks you into giving info. Never click suspicious links!",
                        "Beware of fake emails or texts that ask for personal info.",
                        "Always double-check sender details before clicking links or downloading files.",
                    },
                    userName
                );
            }
            else if (lower.Contains("browsing"))
            {
                return GetRandomResponse(
                    new[]
                    {
                        "Use HTTPS and avoid entering personal info on shady websites.",
                        "Stick to reputable websites and always look for the lock icon.",
                        "Secure browsing means using private networks and encrypted sites.",
                    },
                    userName
                );
            }

            // Fallback if no known topic
            return "I'm not sure how to answer that yet. Try asking about cybersecurity topics!";
        }

        public string GenerateResponse(string input)
        {
            string sentiment = DetectSentiment(input);

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
                return GetFollowUpResponse();
            }

            string lower = input.ToLower();
            lastTopic = null;

            if (lower.Contains("how are you"))
            {
                lastTopic = "how";
                return GetRandomResponse(
                    new[]
                    {
                        "I'm functioning securely and ready to assist!",
                        "All systems go! I'm here to help.",
                        "I'm feeling quite binary today — 1s and 0s!",
                    },
                    userName
                );
            }
            else if (lower.Contains("purpose"))
            {
                lastTopic = "purpose";
                return GetRandomResponse(
                    new[]
                    {
                        "I'm here to teach you about cybersecurity.",
                        "My mission is to make you cybersafe!",
                        "Educating users about digital safety is my core function.",
                    },
                    userName
                );
            }
            else if (lower.Contains("what can i ask"))
            {
                lastTopic = "topics";
                return "You can ask me about password safety, phishing, or safe browsing.";
            }
            else if (lower.Contains("password"))
            {
                lastTopic = "password";
                return GetRandomResponse(
                    new[]
                    {
                        "Use long, unique passwords with letters, numbers, and symbols.",
                        "Never reuse passwords and consider using a password manager.",
                        "Strong passwords are your first defense — make them complex and private.",
                    },
                    userName
                );
            }
            else if (lower.Contains("phishing"))
            {
                lastTopic = "phishing";
                return GetRandomResponse(
                    new[]
                    {
                        "Phishing tricks you into giving info. Never click suspicious links!",
                        "Beware of fake emails or texts that ask for personal info.",
                        "Always double-check sender details before clicking links or downloading files.",
                    },
                    userName
                );
            }
            else if (lower.Contains("browsing"))
            {
                lastTopic = "browsing";
                return GetRandomResponse(
                    new[]
                    {
                        "Use HTTPS and avoid entering personal info on shady websites.",
                        "Stick to reputable websites and always look for the lock icon.",
                        "Secure browsing means using private networks and encrypted sites.",
                    },
                    userName
                );
            }

            string[] selected = sentiment switch
            {
                "frustrated" => new[]
                {
                    "I'm really trying my best to help, I promise!",
                    "Let’s take it one step at a time — can you rephrase that?",
                    "I get that this might be frustrating. Let's try another angle.",
                },
                "worried" => new[]
                {
                    "Don’t worry, you're not alone. I can guide you.",
                    "Security can be stressful, but you're in the right place to learn.",
                    "Take a breath — I'm here to help you stay safe.",
                },
                "curious" => new[]
                {
                    "Great question! I love that you're curious.",
                    "That’s an awesome thing to explore!",
                    "Curiosity is the first step to becoming cyber smart!",
                },
                _ => new[]
                {
                    "I'm not sure how to answer that yet. Try asking about cybersecurity topics!",
                    "Hmm, that's outside my current knowledge. Try a different question.",
                    "Good question! But I’m best with cybersecurity topics right now.",
                },
            };

            return GetRandomResponse(selected, userName);
        }

        private string GetFollowUpResponse()
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

            return GetRandomResponse(details, userName);
        }

        private string DetectSentiment(string input)
        {
            input = input.ToLower();

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

        private string GetRandomResponse(string[] options, string? name = null)
        {
            string response = options[rng.Next(options.Length)];
            return name != null ? $"Sure, {name}! {response}" : response;
        }
    }
}
