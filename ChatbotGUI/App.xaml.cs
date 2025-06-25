using System.Windows;

namespace ChatbotGUI
{
    public partial class App : Application
    {
        public static string? UserName { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (e.Args.Length > 0)
            {
                UserName = e.Args[0];
            }
        }
    }
}
