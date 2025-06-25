using System;
using System.Diagnostics;
using System.Media;

namespace ST10318880_POE1
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "";

            // #pragma warning disable CA1416
            //             SoundPlayer player = new SoundPlayer("greeting.wav");
            //             player.PlaySync();
            // #pragma warning restore CA1416

            while (true)
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(name) && name.All(char.IsLetter) && name.Length >= 3)
                    break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "⚠️  Please enter a valid name (letters only, at least 3 characters)."
                );
                Console.ResetColor();
            }

            Console.Write("Would you like to launch the GUI? (yes/no): ");
            string? guiResponse = Console.ReadLine()?.Trim().ToLower();

            if (guiResponse == "yes" || guiResponse == "y")
            {
                try
                {
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName =
                                @"..\\ChatbotGUI\\bin\\Debug\\net8.0-windows\\ChatbotGUI.exe",
                            Arguments = name,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                        },
                    };

                    process.Start();
                    process.WaitForExit(); // Waits for GUI to close
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"❌ Failed to launch GUI: {ex.Message}");
                    Console.ResetColor();
                }
            }

            AsciiArt.Show();

            Chatbot bot = new Chatbot();
            bot.Start(name);
        }
    }
}
