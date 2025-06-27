using System.Text.RegularExpressions;
using ST10318880_POE1.GUI.Task;
using ST10318880_POE1.Services;
using ST10318880_POE1.Services.ChatQuiz;

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

        public bool IsInTaskConversation()
        {
            return currentState != ConversationState.None;
        }

        // Common phrases for adding tasks/reminders
        private readonly List<string> addTaskKeywords = new List<string>
        {
            "add task",
            "create task",
            "new task",
            "remind me",
            "reminder",
            "set reminder",
            "schedule task",
            "task reminder",
            "add a new task",
            "set a task",
            "i want to add a task",
            "can you add a task",
            "make a task",
            "remember this",
            "remember to",
            "log a task",
            "save a task",
            "write this down",
            "note this down",
            "help me remember",
            "i need to do something",
            "don’t forget to",
            "can you note",
            "todo",
            "add to my list",
            "task list",
            "remind",
            "add something",
            "track this",
        };

        // Phrases to initiate the quiz
        private readonly List<string> startQuizKeywords = new List<string>
        {
            "start quiz",
            "take quiz",
            "quiz time",
            "begin quiz",
            "i want to do a quiz",
            "can i take a quiz",
            "run the quiz",
            "give me a quiz",
            "test me",
            "start the test",
            "begin test",
            "i want a test",
            "start cyber quiz",
            "cybersecurity quiz",
            "ask me questions",
            "let’s start the quiz",
            "start knowledge check",
            "launch the quiz",
            "quiz me",
            "start a quick test",
            "cyber test",
            "challenge me",
            "quiz now",
            "let’s quiz",
            "start challenge",
            "knowledge check",
            "ask a question",
            "ask quiz question",
        };

        // Regex patterns to match flexible user inputs
        private readonly Regex addTaskRegex = new Regex(
            @"\b(add|create|make|note|remember|remind|log|save|write|track|schedule|set)\b.*\b(task|reminder|something|this|todo|list)?\b",
            RegexOptions.IgnoreCase
        );

        private readonly Regex startQuizRegex = new Regex(
            @"\b(start|take|run|launch|begin|give|do|want|quiz|test|challenge|ask)\b.*\b(quiz|test|questions?)\b|\bquiz\b",
            RegexOptions.IgnoreCase
        );

        // Enum for chatbot intent detection
        public enum Intent
        {
            AddTask,
            StartQuiz,
            Other,
            Unknown,
        }

        // Detect user's intent based on regex or keyword lists
        public Intent DetectIntent(string input)
        {
            string cleanedInput = new string(
                input.ToLower().Where(c => !char.IsPunctuation(c)).ToArray()
            );

            Console.WriteLine($"[DEBUG] DetectIntent cleaned input: {cleanedInput}");

            // Regex-based matching first
            if (addTaskRegex.IsMatch(cleanedInput))
            {
                Console.WriteLine("[DEBUG] Detected intent (regex): AddTask");
                return Intent.AddTask;
            }

            if (startQuizRegex.IsMatch(cleanedInput))
            {
                Console.WriteLine("[DEBUG] Detected intent (regex): StartQuiz");
                return Intent.StartQuiz;
            }

            // Fallback to keyword list matching
            if (addTaskKeywords.Any(keyword => cleanedInput.Contains(keyword)))
            {
                Console.WriteLine("[DEBUG] Detected intent (keywords): AddTask");
                return Intent.AddTask;
            }

            if (startQuizKeywords.Any(keyword => cleanedInput.Contains(keyword)))
            {
                Console.WriteLine("[DEBUG] Detected intent (keywords): StartQuiz");
                return Intent.StartQuiz;
            }

            Console.WriteLine("[DEBUG] Detected intent: Other");
            return Intent.Other;
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
                return "Here’s what you can do:\n"
                    + "• Ask about cybersecurity topics like passwords, phishing, or safe browsing.\n"
                    + "• Add a task by saying things like 'remind me to...' or 'add a task'.\n"
                    + "• Take a quiz by saying 'start quiz' or 'test me'.";
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
            else if (
                lower.Contains("favourite topic")
                || lower.Contains("favorite topic")
                || lower.Contains("my topic")
            )
            {
                if (string.IsNullOrEmpty(favouriteTopic) || favouriteTopic == "unknown")
                    return "Hmm, I'm not sure yet — I haven't picked up on your favorite topic.";

                return $"I'd say your favorite topic so far is **{favouriteTopic}**!";
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

        private enum ConversationState
        {
            None,
            WaitingForTitle,
            WaitingForDescription,
            WaitingForReminderConfirmation,
            WaitingForReminderDate,
            TakingQuiz,
        }

        // Secondary helper method to log actyivities held in chatbot as opposed to dedicated UI
        public Chatbot(LogService logService)
        {
            _logService = logService;
        }

        private readonly LogService _logService;
        private ConversationState currentState = ConversationState.None;
        private ChatQuizService? chatQuizService;
        private CyberTask pendingTask = new CyberTask();

        public string HandleInput(string input)
        {
            string lower = input.ToLower();

            // Handle quiz answer input if we're in quiz mode
            if (currentState == ConversationState.TakingQuiz)
            {
                string response = chatQuizService!.SubmitAnswer(input);

                if (chatQuizService.IsComplete)
                {
                    currentState = ConversationState.None;
                }

                return response;
            }

            // Starting point for new conversation or intent
            if (currentState == ConversationState.None)
            {
                var intent = DetectIntent(input);

                if (intent == Intent.AddTask)
                {
                    currentState = ConversationState.WaitingForTitle;
                    return "Sure! What would you like the task title to be?";
                }

                if (intent == Intent.StartQuiz)
                {
                    chatQuizService = new ChatQuizService(_logService);
                    currentState = ConversationState.TakingQuiz;
                    return $"🧠 Let's start the quiz!\n\n❓ {chatQuizService.GetCurrentQuestion()} (True/False)";
                }

                // Default to general response
                return GenerateResponse(input, userName ?? "");
            }
            // Handle task conversation flow
            else if (currentState == ConversationState.WaitingForTitle)
            {
                pendingTask.Title = input;
                currentState = ConversationState.WaitingForDescription;
                return "Got it! Add a description.";
            }
            else if (currentState == ConversationState.WaitingForDescription)
            {
                if (string.IsNullOrWhiteSpace(input) || input.ToLower().Trim() == "no")
                {
                    return "A description is required. Please provide one.";
                }

                pendingTask.Description = input;
                currentState = ConversationState.WaitingForReminderConfirmation;
                return "Thanks! Should I set a reminder for this task? (yes/no)";
            }
            else if (currentState == ConversationState.WaitingForReminderConfirmation)
            {
                if (lower.Contains("yes"))
                {
                    currentState = ConversationState.WaitingForReminderDate;
                    return "Okay, please tell me the date and time for the reminder.";
                }
                else
                {
                    currentState = ConversationState.None;
                    SaveTask(pendingTask);
                    pendingTask = new CyberTask();
                    return "Task added without a reminder!";
                }
            }
            else if (currentState == ConversationState.WaitingForReminderDate)
            {
                if (DateTime.TryParse(input, out DateTime reminder))
                {
                    pendingTask.ReminderDate = reminder;
                    currentState = ConversationState.None;
                    SaveTask(pendingTask);
                    pendingTask = new CyberTask();
                    return $"Task added with reminder set for {reminder}.";
                }
                else
                {
                    return "Sorry, I couldn't understand that date/time. Please try again.";
                }
            }

            return "I'm not sure how to help with that right now.";
        }

        private void SaveTask(CyberTask task)
        {
            _logService.AddTask(task);
            _logService.AddActivity($"Task added via chatbot: {task.Title}");
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
