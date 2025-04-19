using System;
using System.Media;

namespace ST10318880_POE1
{
    class Program
    {
        static void Main(string[] args)
        {
            SoundPlayer player = new SoundPlayer("greeting.wav");
            player.PlaySync();
        }
    }
}
