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

        public AddEditWindow()
        {
            InitializeComponent();
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
            if (!isEdit)
            {
                taskToDo.Created = DateTime.Now;
            }
            taskToDo.Name = taskName.Text;
            taskToDo.Description = taskDescription.Text;
            taskToDo.Updated = DateTime.Now;
            taskToDo.Deadline = taskDeadline.DisplayDate;
            taskToDo.Priority = int.Parse(taskPriority.Text);
            taskList.Add(taskToDo);
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
            double result;
            return double.TryParse(text, out result);
        }
    }
}
