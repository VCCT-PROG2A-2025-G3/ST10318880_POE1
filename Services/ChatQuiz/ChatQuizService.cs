using System.Collections.Generic;
using ST10318880_POE1.GUI.Quiz;

namespace ST10318880_POE1.Services.ChatQuiz
{
    public class ChatQuizService
    {
        private List<QuizQuestion> questions;
        private int currentIndex;
        private int score;
        private readonly LogService _logService;

        public ChatQuizService(LogService logService)
        {
            LoadQuestions();
            currentIndex = 0;
            score = 0;
        }

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

        public string GetCurrentQuestion()
        {
            if (currentIndex < questions.Count)
                return questions[currentIndex].QuestionText;
            return null;
        }

        public bool HasMoreQuestions => currentIndex < questions.Count;

        public string SubmitAnswer(string userInput)
        {
            string cleaned = userInput.Trim().ToLower();
            bool userAnswer;

            if (!HasMoreQuestions)
                return "Quiz is already complete.";

            // if (!bool.TryParse(userInput.ToLower(), out bool userAnswer))
            //     return " Please answer with '(T)rue' or '(F)alse'.";

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

        public bool IsComplete => !HasMoreQuestions;
    }
}
