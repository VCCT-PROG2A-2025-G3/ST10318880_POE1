using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ST10318880_POE1.Services;

namespace ST10318880_POE1.GUI.Task
{
    // Represents the task management page, allowing users to create and manage tasks
    public partial class TaskPage : Page
    {
        private ObservableCollection<CyberTask> tasks; // Collection bound to the ListBox
        private readonly LogService _logService;

        // Constructor initializes UI and loads tasks from the log service
        public TaskPage(LogService logService)
        {
            InitializeComponent();

            _logService = logService;
            tasks = new ObservableCollection<CyberTask>(_logService.GetTasks());
            TaskListBox.ItemsSource = tasks; // Bind task list to UI
        }

        // Show additional reminder input options when checkbox is checked
        private void EnableReminderCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            ReminderOptionsPanel.Visibility = Visibility.Visible;
        }

        // Hide reminder options and clear their input when checkbox is unchecked
        private void EnableReminderCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            ReminderOptionsPanel.Visibility = Visibility.Collapsed;
            ReminderDatePicker.SelectedDate = null;
            TimeFrameInput.Text = string.Empty;
        }

        // Handles the "Add Task" button click to create a new task
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleInput.Text.Trim();
            string description = DescriptionInput.Text.Trim();
            DateTime? reminderDate = ReminderDatePicker.SelectedDate;
            string timeFrame = TimeFrameInput.Text.Trim();

            // Validate required fields
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show(
                    "Please enter both a title and description.",
                    "Missing Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            // Create and populate a new task object
            var newTask = new CyberTask
            {
                Title = title,
                Description = description,
                ReminderDate = reminderDate,
                TimeFrame = string.IsNullOrEmpty(timeFrame) ? null : timeFrame,
            };

            // Save task to log service and update UI
            _logService.AddTask(newTask);
            tasks.Add(newTask);

            _logService.AddActivity(
                $"Task created - Title: '{title}' Description: '{description}'"
            );

            MessageBox.Show(
                "Task added successfully!",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        // Handles task selection and opens the task dialog for actions
        private void TaskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TaskListBox.SelectedItem is CyberTask selectedTask)
            {
                var dialog = new TaskDialog(selectedTask);
                dialog.ShowDialog(); // Show modal dialog

                // Handle result from dialog
                if (dialog.Result == TaskDialog.TaskAction.Completed)
                {
                    selectedTask.IsCompleted = true;
                    TaskListBox.Items.Refresh(); // Refresh UI to show updated status
                }
                else if (dialog.Result == TaskDialog.TaskAction.Delete)
                {
                    tasks.Remove(selectedTask); // Remove from collection
                }

                TaskListBox.SelectedItem = null; // Reset selection
            }
        }
    }

    // CyberTask Class
    public class CyberTask
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public string? TimeFrame { get; set; }
        public bool IsCompleted { get; set; } = false;

        // Provides a formatted reminder string for display
        public string ReminderDisplay
        {
            get
            {
                if (ReminderDate.HasValue)
                    return ReminderDate.Value.ToString("yyyy-MM-dd");
                else if (!string.IsNullOrWhiteSpace(TimeFrame))
                    return TimeFrame;
                else
                    return "None";
            }
        }
    }
}
