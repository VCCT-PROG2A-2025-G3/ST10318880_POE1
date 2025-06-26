using System.Windows;

namespace ST10318880_POE1.GUI.Task
{
    public partial class TaskDialog : Window
    {
        public enum TaskAction
        {
            None,
            Completed,
            Delete,
        }

        public TaskAction Result { get; private set; } = TaskAction.None;

        public TaskDialog(CyberTask task)
        {
            InitializeComponent();
            TaskTitleText.Text = $"Title: {task.Title}";
            TaskDescriptionText.Text = $"Description: {task.Description}";
            ReminderTextBlock.Text = $"Reminder: {task.ReminderDisplay}";
        }

        private void Completed_Click(object sender, RoutedEventArgs e)
        {
            Result = TaskAction.Completed;
            Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Result = TaskAction.Delete;
            Close();
        }
    }
}
