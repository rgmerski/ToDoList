using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoApp.Model;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<TaskToDo> tasks = new ObservableCollection<TaskToDo>();

        public MainWindow()
        {
            InitializeComponent();
            taskListBox.ItemsSource = tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddEditWindow addEditWindow = new(false, tasks);
            addEditWindow.ShowDialog();
           
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (taskListBox.SelectedItem != null)
            {
                TaskToDo editTask = taskListBox.SelectedItem as TaskToDo;
                AddEditWindow editWindow = new(true, editTask, tasks);
                editWindow.ShowDialog();
            }
        }

        private void RemoveTask_Click(object sender, RoutedEventArgs e)
        {
            if (taskListBox.SelectedIndex != -1)
            {
                int selectedIndex = taskListBox.SelectedIndex;
                tasks.RemoveAt(selectedIndex);
            }
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (taskListBox.SelectedIndex != -1)
            {
                int selectedIndex = taskListBox.SelectedIndex;
                tasks[selectedIndex] = "[Completed] " + tasks[selectedIndex];
            }
            */
        }

        private void taskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If any element is selected, update buttons
            removeButton.IsEnabled = (taskListBox.SelectedIndex != -1);
            completeButton.IsEnabled = (taskListBox.SelectedIndex != -1);
            editButton.IsEnabled = (taskListBox.SelectedIndex != -1);

            // and set text box to a current task name
            if (taskListBox.SelectedIndex != -1)
            {
                taskTextBox.Text = tasks.ElementAt(taskListBox.SelectedIndex).Name;
            }
            else
            {
                taskTextBox.Text = "";
            }
        }        
    }
}
