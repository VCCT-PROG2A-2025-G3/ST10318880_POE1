using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ST10318880_POE1.GUI.Quiz
{
    public partial class QuizPage : Page
    {
        private List<QuizQuestion> questions;
        private int currentIndex = 0;
        private int score = 0;

        public QuizPage()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayCurrentQuestion();
        }

        private void LoadQuestions()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    QuestionText = "Phishing is a type of cyberattack.",
                    Answer = true,
                    Explanation = "Phishing tricks users into revealing sensitive info.",
                },
                new QuizQuestion
                {
                    QuestionText = "HTTPS is less secure than HTTP.",
                    Answer = false,
                    Explanation = "HTTPS encrypts traffic, HTTP does not.",
                },
                // Add 8 more...
                new QuizQuestion
                {
                    QuestionText = "Two-factor authentication increases security.",
                    Answer = true,
                    Explanation = "It adds a second layer of authentication.",
                },
                new QuizQuestion
                {
                    QuestionText = "Antivirus software guarantees 100% protection.",
                    Answer = false,
                    Explanation = "It helps, but cannot detect all threats.",
                },
                new QuizQuestion
                {
                    QuestionText = "Firewalls block unauthorized access.",
                    Answer = true,
                    Explanation = "That’s their main purpose.",
                },
                new QuizQuestion
                {
                    QuestionText = "Public Wi-Fi is always safe to use for banking.",
                    Answer = false,
                    Explanation = "Public Wi-Fi is often insecure.",
                },
                new QuizQuestion
                {
                    QuestionText = "Malware is only spread through email.",
                    Answer = false,
                    Explanation = "It can spread via websites, USBs, etc.",
                },
                new QuizQuestion
                {
                    QuestionText = "Using the same password everywhere is safe.",
                    Answer = false,
                    Explanation = "Reusing passwords increases risk.",
                },
                new QuizQuestion
                {
                    QuestionText = "Software updates help fix vulnerabilities.",
                    Answer = true,
                    Explanation = "Updates patch security holes.",
                },
                new QuizQuestion
                {
                    QuestionText =
                        "Shoulder surfing means looking over someone’s shoulder to steal data.",
                    Answer = true,
                    Explanation = "It’s a real threat in public places.",
                },
            };
        }

        private void DisplayCurrentQuestion()
        {
            FeedbackTextBlock.Text = "";
            NextButton.Visibility = Visibility.Collapsed;

            if (currentIndex < questions.Count)
            {
                QuestionTextBlock.Text = questions[currentIndex].QuestionText;
            }
            else
            {
                QuestionTextBlock.Text = $"Quiz complete! Your score: {score}/{questions.Count}";
                FeedbackTextBlock.Text = score switch
                {
                    >= 9 => "Excellent! You're very cyber-aware.",
                    >= 6 => "Good job! A few areas to review.",
                    _ => "Consider reviewing cybersecurity basics.",
                };
                QuestionTextBlock.FontWeight = FontWeights.Normal;
            }
        }

        private void HandleAnswer(bool userAnswer)
        {
            if (currentIndex >= questions.Count)
                return;

            var question = questions[currentIndex];
            bool isCorrect = userAnswer == question.Answer;

            if (isCorrect)
            {
                score++;
                FeedbackTextBlock.Text = "✅ Correct! " + question.Explanation;
                FeedbackTextBlock.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                FeedbackTextBlock.Text = "❌ Incorrect. " + question.Explanation;
                FeedbackTextBlock.Foreground = System.Windows.Media.Brushes.Red;
            }

            NextButton.Visibility = Visibility.Visible;
        }

        private void TrueButton_Click(object sender, RoutedEventArgs e) => HandleAnswer(true);

        private void FalseButton_Click(object sender, RoutedEventArgs e) => HandleAnswer(false);

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            currentIndex++;
            DisplayCurrentQuestion();
        }
    }
}
