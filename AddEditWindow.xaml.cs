using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using ToDoApp.Model;
using ToDoApp.ViewModel;

namespace ToDoApp
{
    /// <summary>
    /// Logika interakcji dla klasy AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        private TaskListViewModel taskListViewModel;
        private bool isEdit;
        private TaskToDo taskToEdit;

        public AddEditWindow()
        {
            InitializeComponent();
        }

        // Passing ObservableCollection - got some problems with accessibility, source: https://stackoverflow.com/questions/75820604/how-can-i-pass-my-observablecollection-of-any-type-to-a-method-c-sharp
        // Constructor for editing existing task
        public AddEditWindow(bool edit, TaskToDo editTask, params object[] pArgs)
        {
            InitializeComponent();

            if (pArgs[0] != null)
            {
                dynamic tasks = pArgs[0];
                taskListViewModel = tasks;
                taskName.Text = editTask.Name;
                taskDescription.Text = editTask.Description;
                taskDeadline.DisplayDate = editTask.Deadline;
                taskPriority.Text = editTask.Priority.ToString();
                taskToEdit = editTask;
                taskStatus.SelectedIndex = (int)editTask.Status;
                isEdit = edit; 
            }
        }

        // Constructor for adding new task (without filling fields)
        public AddEditWindow(bool edit, params object[] pArgs)
        {
            InitializeComponent();

            if (pArgs[0] != null)
            {
                dynamic tasks = pArgs[0];
                taskListViewModel = tasks;
                isEdit = edit;
            }
        }

        // Close the window without saving changes
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Save the task
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            TaskToDo taskToDo = new TaskToDo();
            taskToDo.Name = taskName.Text;
            taskToDo.Description = taskDescription.Text;
            taskToDo.Updated = DateTime.Now;
            taskToDo.Deadline = taskDeadline.DisplayDate;
            taskToDo.Priority = int.Parse(taskPriority.Text);
            taskToDo.Status = (Model.TaskStatus)taskStatus.SelectedIndex;

            // If adding a new task
            if (!isEdit)
            {
                taskToDo.Created = DateTime.Now;
                taskListViewModel.Tasks.Add(taskToDo); // Add the task to the ViewModel's Tasks collection
            }
            // If editing an existing task
            else
            {
                int editIndex = taskListViewModel.Tasks.IndexOf(taskToEdit);
                if (editIndex != -1)
                {
                    taskListViewModel.Tasks[editIndex] = taskToDo; // Update the task
                }

            }
            Close();
        }

        // Allow only numeric input for priority field
        private void taskPriority_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Check if entered text is numeric
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // If it's not a number, block the entered text
            }
        }

        // Helper function to check if a text is numeric
        private bool IsNumeric(string text)
        {
            int result;
            return int.TryParse(text, out result);
        }
    }
}
