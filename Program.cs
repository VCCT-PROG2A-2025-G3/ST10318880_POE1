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
            Console.Write("Enter your name: ");
            string name = Console.ReadLine() ?? "User";

            string asciiArt =
                @"
                                 @@@@@@@@@                                                          
                             @@@@@@@@@@@@@@@@@                                                      
                          @@@@@@@@@@@@@@@@@@@@@@@                                                   
                        @@@@@@@             @@@@@@@                         @@@@@                   
                       @@@@@@                 @@@@@@                      @@@@@@@@@                 
                      @@@@@      @@@@@@@@@      @@@@@             @@@@@@@@@@@@ @@@@@                
                     @@@@@     @@@@@@@@@@@@@@     @@@@           @@@@@@@@@@@@  @@@@@                
                     @@@@    @@@@@@     @@@@@@    @@@@@        @@@@@@     @@@@@@@@@                 
                    @@@@@    @@@@         @@@@@    @@@@       @@@@@        @@@@@@                   
                    @@@@    @@@@           @@@@    @@@@     @@@@@                                   
                    @@@@    @@@@           @@@@    @@@@@@@@@@@@@                                    
                    @@@@    @@@@           @@@@    @@@@@@@@@@@                 @@@@@@@@             
                    @@@@    @@@@           @@@@    @@@@            @@@@@@@@@@@@@@@@@@@@@            
                    @@@@    @@@@           @@@@    @@@@          @@@@@@@@@@@@@@@@@  @@@@            
                  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@      @@@@@@@@@@@@@@@@@@@@@@@@            
               @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@           @@@@@@@@             
              @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                                  
              @@@@                                       @@@@           @@@@@@                      
              @@@@                @@@@@@@                 @@@          @@@@@@@@@                    
              @@@@              @@@@@@@@@@@@              @@@@@@@@@@@@@@@@  @@@@@                   
              @@@@            @@@@@@@ @@@@@@@             @@@@@@@@@@@@@@@@  @@@@                    
              @@@@            @@@@       @@@@@            @@@          @@@@@@@@@                    
              @@@@           @@@@@       @@@@@            @@@           @@@@@@                      
              @@@@            @@@@@      @@@@@            @@@@@@@@@@            @@@@@@@             
              @@@@             @@@@@   @@@@@@             @@@@@@@@@@@@         @@@@@@@@@@           
              @@@@              @@@@@ @@@@@               @@@     @@@@@@@@@@@@@@@@   @@@@           
              @@@@               @@@@ @@@@                @@@       @@@@@@@@@@@@@@@ @@@@@           
              @@@@               @@@@ @@@@                @@@                  @@@@@@@@@            
              @@@@               @@@@ @@@@                @@@@@@@@@              @@@@@              
              @@@@               @@@@@@@@@                @@@@@@@@@@@                               
              @@@@               @@@@@@@@@                @@@    @@@@@                              
              @@@@                                       @@@@     @@@@@@     @@@@@@@@               
              @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@       @@@@@@@@@@@@@@@@@@              
               @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@         @@@@@@@@@@@   @@@@             
                                                                        @@@@@@@@@@@@@@              
                                                                             @@@@@@@@               
";

            // Ascii styling
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(asciiArt);
            Console.ResetColor();

            // Welcome Message Box
            string message = $"Welcome, {name}";
            int totalWidth = 90; // Width of the ascii art
            string paddedMessage =
                $"║{message.PadLeft((totalWidth - 2 + message.Length) / 2).PadRight(totalWidth - 2)}║";
            string border = new string('═', totalWidth - 2);

            // Welcome styling
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"╔{border}╗");
            Console.WriteLine(paddedMessage);
            Console.WriteLine($"╚{border}╝");
            Console.ResetColor();

            // ---------------------------------------------------------------------------------------------------------------

            // Bot typing function
            static void TypeDelay(
                string message, // Bot response
                int delay = 20, // Millisecond delay between characters
                ConsoleColor color = ConsoleColor.Blue
            )
            {
                Console.ForegroundColor = color;
                foreach (char c in message) // Loop through each character in message
                {
                    Console.Write(c); // Print character
                    Thread.Sleep(delay); // Wait for the delay, simulates typing effect
                }
                Console.WriteLine();
                Console.ResetColor();
            }

            // ---------------------------------------------------------------------------------------------------------------

            // Loop variables
            string botTag = "🤖: "; // Bot tag prefixed to bot responses
            string userTag = "🙋: "; // User tag prefixed to user input
            string divider = new string('═', 80);

            // Conversation loop
            while (true) // Infinite loop until break is hit (exit / bye)
            {
                TypeDelay("\n" + botTag + "Ask me something (type 'exit' to quit): ");

                Console.Write(userTag);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                string input = (Console.ReadLine() ?? "").ToLower().Trim();
                Console.ResetColor();

                if (input.Contains("exit") || input.Contains("bye"))
                {
                    TypeDelay(botTag + "Stay safe online. Goodbye!");
                    Console.WriteLine(divider);
                    break;
                }
                else if (string.IsNullOrWhiteSpace(input)) // If user accidentally presses enter
                {
                    TypeDelay("\n" + botTag + "I didn't quite catch that. Could you try again?");

                    Console.WriteLine(divider);
                    continue;
                }
                else if (input.Contains("how are you"))
                {
                    TypeDelay(
                        "\n"
                            + botTag
                            + "I'm functioning securely and ready to answer whatever questions I can!"
                    );

                    Console.WriteLine(divider);
                }
                else if (input.Contains("purpose"))
                {
                    TypeDelay("\n" + botTag + "I'm here to teach you about cybersecurity.");

                    Console.WriteLine(divider);
                }
                else if (input.Contains("what can i ask"))
                {
                    TypeDelay(
                        "\n"
                            + botTag
                            + "You can ask me about password safety, phishing, or safe browsing."
                    );
                    Console.WriteLine(divider);
                }
                else if (input.Contains("password"))
                {
                    TypeDelay(
                        "\n"
                            + botTag
                            + "Use long, unique passwords with letters, numbers, and symbols."
                    );
                    Console.WriteLine(divider);
                }
                else if (input.Contains("phishing"))
                {
                    TypeDelay(
                        "\n"
                            + botTag
                            + "Phishing is when someone tries to trick you into giving info. Never click suspicious links!"
                    );
                    Console.WriteLine(divider);
                }
                else if (input.Contains("browsing"))
                {
                    TypeDelay(
                        "\n"
                            + botTag
                            + "Make sure websites use HTTPS and avoid entering personal info on unknown sites."
                    );
                    Console.WriteLine(divider);
                }
                else
                {
                    TypeDelay(
                        "\n"
                            + botTag
                            + "I'm not sure how to answer that yet. Try asking about cybersecurity topics!"
                    );
                    Console.WriteLine(divider);
                }
            }
        }
    }
}
