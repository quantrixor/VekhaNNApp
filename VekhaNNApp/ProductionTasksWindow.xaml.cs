using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VekhaNNApp.Model;

namespace VekhaNNApp
{
    /// <summary>
    /// Логика взаимодействия для ProductionTasksWindow.xaml
    /// </summary>
    public partial class ProductionTasksWindow : Window
    {
        private VekhaNNEntities _context;

        public ProductionTasksWindow()
        {
            InitializeComponent();
            _context = new VekhaNNEntities();
            LoadTasks();
        }

        private void LoadTasks()
        {
            TasksDataGrid.ItemsSource = _context.ProductionTasks.ToList();
            ClearInputData();
        }
        private void ClearInputData()
        {
            TaskNameTextBox.Text = string.Empty;
            DescriptionTextBox.Text = string.Empty;
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
        }
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var task = new ProductionTasks
            {
                TaskName = TaskNameTextBox.Text,
                Description = DescriptionTextBox.Text,
                StartDate = StartDatePicker.SelectedDate.Value,
                EndDate = EndDatePicker.SelectedDate.Value
            };
            _context.ProductionTasks.Add(task);
            _context.SaveChanges();
            LoadTasks();
        }

        private void UpdateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem is ProductionTasks selectedTask)
            {
                selectedTask.TaskName = TaskNameTextBox.Text;
                selectedTask.Description = DescriptionTextBox.Text;
                selectedTask.StartDate = StartDatePicker.SelectedDate.Value;
                selectedTask.EndDate = EndDatePicker.SelectedDate.Value;
                _context.SaveChanges();
                LoadTasks();
            }
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem is ProductionTasks selectedTask)
            {
                _context.ProductionTasks.Remove(selectedTask);
                _context.SaveChanges();
                LoadTasks();
            }
        }

        private void TasksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem is ProductionTasks selectedTask)
            {
                TaskNameTextBox.Text = selectedTask.TaskName;
                DescriptionTextBox.Text = selectedTask.Description;
                StartDatePicker.SelectedDate = selectedTask.StartDate;
                EndDatePicker.SelectedDate = selectedTask.EndDate;
            }
        }
    }
}
