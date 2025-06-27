using ST10318880_POE1.GUI.Quiz;

namespace ST10318880_POE1.Services.ChatQuiz
{
    public class ChatQuizService
    {
        private List<QuizQuestion> questions = new();
        private int currentIndex;
        private int score;
        private readonly LogService _logService;

        public ChatQuizService(LogService logService)
        {
            _logService = logService;
            LoadQuestions();
            currentIndex = 0;
            score = 0;
        }

        // Initialize a static list of quiz questions
        private void LoadQuestions()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    QuestionText = "Using '123456' as a password is safe.",
                    Answer = false,
                    Explanation = "It's one of the most common (and weakest) passwords.",
                },
                new QuizQuestion
                {
                    QuestionText = "VPNs help protect your online privacy.",
                    Answer = true,
                    Explanation = "VPNs encrypt your traffic and hide your IP address.",
                },
                new QuizQuestion
                {
                    QuestionText = "Clicking unknown links in emails is risky.",
                    Answer = true,
                    Explanation = "Links can lead to phishing or malware.",
                },
            };
        }

        // Returns the current question's text
        public string? GetCurrentQuestion()
        {
            if (currentIndex < questions.Count)
                return questions[currentIndex].QuestionText;
            return null;
        }

        // Indicates if there are more questions left
        public bool HasMoreQuestions => currentIndex < questions.Count;

        // Handles submitted answer and returns feedback
        public string SubmitAnswer(string userInput)
        {
            string cleaned = userInput.Trim().ToLower();
            bool userAnswer;

            if (!HasMoreQuestions)
                return "Quiz is already complete.";

            // Accepts both full and shorthand answers
            if (cleaned == "true" || cleaned == "t")
                userAnswer = true;
            else if (cleaned == "false" || cleaned == "f")
                userAnswer = false;
            else
                return "❓ Please answer with 'True' or 'False'.";

            var q = questions[currentIndex];
            bool isCorrect = userAnswer == q.Answer;

            if (isCorrect)
                score++;

            // Create feedback with icon and explanation
            string feedback =
                (isCorrect ? "✅ Correct!" : "❌ Incorrect.") + $"\nℹ️ Explanation: {q.Explanation}";

            currentIndex++;

            if (HasMoreQuestions)
            {
                feedback += $"\n\n❓ {GetCurrentQuestion()} (True/False)";
            }
            else
            {
                feedback += $"\n\n🎉 Quiz complete! You scored {score}/{questions.Count}.";
            }

            return feedback;
        }

        // Returns true when the quiz is over
        public bool IsComplete => !HasMoreQuestions;
    }
}
