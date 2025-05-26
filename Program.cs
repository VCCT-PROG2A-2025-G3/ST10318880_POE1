using System.Media;

namespace ST10318880_POE1
{
    class Program
    {
        static void Main(string[] args)
        {
#pragma warning disable CA1416
            SoundPlayer player = new SoundPlayer("greeting.wav");
            player.PlaySync();
#pragma warning restore CA1416

            string name = "";

            while (true)
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine()?.Trim();

                // Validate name: non-empty, alphabetical, at least 3 characters
                if (!string.IsNullOrWhiteSpace(name) && name.All(char.IsLetter) && name.Length >=3)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "⚠️  Please enter a valid name (letters only, at least 2 characters)."
                );
                Console.ResetColor();
            }

            // Display ASCII splash art
            AsciiArt.Show();

            // Start chatbot
            Chatbot bot = new Chatbot();
            bot.Start(name);
        }
    }
}
