using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ST10318880_POE1.Services;

namespace ST10318880_POE1.GUI.Task
{
    public partial class TaskPage : Page
    {
        private ObservableCollection<CyberTask> tasks = new ObservableCollection<CyberTask>();
        private readonly LogService _logService;

        public TaskPage(LogService logService)
        {
            InitializeComponent();
            _logService = logService;
            TaskListBox.ItemsSource = tasks;
        }

        private void EnableReminderCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            ReminderOptionsPanel.Visibility = Visibility.Visible;
        }

        private void EnableReminderCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            ReminderOptionsPanel.Visibility = Visibility.Collapsed;
            ReminderDatePicker.SelectedDate = null;
            TimeFrameInput.Text = string.Empty;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleInput.Text.Trim();
            string description = DescriptionInput.Text.Trim();
            DateTime? reminderDate = ReminderDatePicker.SelectedDate;
            string timeFrame = TimeFrameInput.Text.Trim();

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

            var newTask = new CyberTask
            {
                Title = title,
                Description = description,
                ReminderDate = reminderDate,
                TimeFrame = string.IsNullOrEmpty(timeFrame) ? null : timeFrame,
            };

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

        private void TaskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TaskListBox.SelectedItem is CyberTask selectedTask)
            {
                var dialog = new TaskDialog(selectedTask);
                dialog.ShowDialog();

                if (dialog.Result == TaskDialog.TaskAction.Completed)
                {
                    selectedTask.IsCompleted = true;
                    TaskListBox.Items.Refresh();
                }
                else if (dialog.Result == TaskDialog.TaskAction.Delete)
                {
                    tasks.Remove(selectedTask);
                }

                TaskListBox.SelectedItem = null;
            }
        }
    }

    public class CyberTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public string? TimeFrame { get; set; }
        public bool IsCompleted { get; set; } = false;

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
