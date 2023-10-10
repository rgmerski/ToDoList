using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using ToDoApp.Model;
using ToDoApp.ViewModel;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaskListViewModel taskListViewModel = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = taskListViewModel; // Set the DataContext to the TaskListViewModel
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            // Open the Add/Edit window for adding a new task
            AddEditWindow addEditWindow = new(false, taskListViewModel);
            addEditWindow.ShowDialog();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (taskListBox.SelectedItem != null)
            {
                TaskToDo editTask = taskListBox.SelectedItem as TaskToDo;

                // Open the Add/Edit window for editing an existing task
                AddEditWindow editWindow = new(true, editTask, taskListViewModel);
                editWindow.ShowDialog();
            }
        }

        private void RemoveTask_Click(object sender, RoutedEventArgs e)
        {
            if (taskListBox.SelectedItem != null)
            {
                TaskToDo selectedTask = taskListBox.SelectedItem as TaskToDo;
                taskListViewModel.Tasks.Remove(selectedTask); // Remove the selected task
            }
        }


        private void taskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If any element is selected, update buttons
            removeButton.IsEnabled = (taskListBox.SelectedIndex != -1);
            editButton.IsEnabled = (taskListBox.SelectedIndex != -1);

            // and set text box to a current task name
            if (taskListBox.SelectedIndex != -1)
            {
                TaskToDo selected = taskListBox.SelectedItem as TaskToDo;
                taskTextBox.Text = selected.Name;
            }
            else
            {
                taskTextBox.Text = "";
            }
        }        
    }
 }
