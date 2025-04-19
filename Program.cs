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

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(asciiArt);
            Console.ResetColor();

            string message = $"Welcome, {name}";
            int totalWidth = 90; // Width of the ascii art
            string paddedMessage =
                $"║{message.PadLeft((totalWidth - 2 + message.Length) / 2).PadRight(totalWidth - 2)}║";
            string border = new string('═', totalWidth - 2);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"╔{border}╗");
            Console.WriteLine(paddedMessage);
            Console.WriteLine($"╚{border}╝");
            Console.ResetColor();

            // ---------------------------------------------------------------------------------------------------------------

            static void TypeDelay(
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

            // ---------------------------------------------------------------------------------------------------------------

            string botTag = "🤖: ";
            string userTag = "🙋: ";
            string divider = new string('═', 80);

            while (true)
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
                else if (string.IsNullOrWhiteSpace(input))
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
