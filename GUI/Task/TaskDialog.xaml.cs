using System.Windows;

namespace ST10318880_POE1.GUI.Task
{
    // A modal dialog window for displaying task details and allowing user actions
    public partial class TaskDialog : Window
    {
        // Enum representing the user's chosen action on the task
        public enum TaskAction
        {
            None, // No action taken
            Completed, // Task marked as completed
            Delete, // Task marked for deletion
        }

        // Property to store the result of the user's action
        public TaskAction Result { get; private set; } = TaskAction.None;

        // Constructor initializes the dialog with task details
        public TaskDialog(CyberTask task)
        {
            InitializeComponent();

            // Populate the UI elements with task data
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
