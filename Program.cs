using System.Media;

namespace ST10318880_POE1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Play greeting audio
#pragma warning disable CA1416
            SoundPlayer player = new SoundPlayer("greeting.wav");
            player.PlaySync();
#pragma warning restore CA1416

            // Ask user for name
            Console.Write("Enter your name: ");
            string name = Console.ReadLine() ?? "User";

            // Display ASCII splash art
            AsciiArt.Show();

            // Start chatbot session
            Chatbot bot = new Chatbot();
            bot.Start(name);
        }
    }
}