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

namespace ToDoApp
{
    /// <summary>
    /// Logika interakcji dla klasy AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        private ObservableCollection<TaskToDo> taskList = new ObservableCollection<TaskToDo>();
        private bool isEdit;
        private TaskToDo taskToEdit;

        public AddEditWindow()
        {
            InitializeComponent();
        }

        // Passing ObservableCollection - got some problems with accessibility, source: https://stackoverflow.com/questions/75820604/how-can-i-pass-my-observablecollection-of-any-type-to-a-method-c-sharp
        public AddEditWindow(bool edit, TaskToDo editTask, params object[] pArgs)
        {
            InitializeComponent();
            if (pArgs[0] != null)
            {
                dynamic tasks = pArgs[0];
                taskList = tasks;
                taskName.Text = editTask.Name;
                taskDescription.Text = editTask.Description;
                taskDeadline.DisplayDate = editTask.Deadline;
                taskPriority.Text = editTask.Priority.ToString();
                taskToEdit = editTask;
                taskStatus.SelectedIndex = (int)editTask.Status;
                isEdit = edit; 
            }
        }

        public AddEditWindow(bool edit, params object[] pArgs)
        {
            InitializeComponent();
            if (pArgs[0] != null)
            {
                dynamic tasks = pArgs[0];
                taskList = tasks;
                isEdit = edit;
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            TaskToDo taskToDo = new TaskToDo();
            taskToDo.Name = taskName.Text;
            taskToDo.Description = taskDescription.Text;
            taskToDo.Updated = DateTime.Now;
            taskToDo.Deadline = taskDeadline.DisplayDate;
            taskToDo.Priority = int.Parse(taskPriority.Text);
            taskToDo.Status = (Model.TaskStatus)taskStatus.SelectedIndex;

            // If adding
            if (!isEdit)
            {
                taskToDo.Created = DateTime.Now;
                taskList.Add(taskToDo);
            }
            // If editing
            else
            {
                int editIndex = taskList.IndexOf(taskToEdit);
                if (editIndex != -1)
                {
                    taskList[editIndex] = taskToDo;
                }

            }
            Close();

        }

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
