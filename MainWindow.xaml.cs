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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> tasks = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            taskListBox.ItemsSource = tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string task = taskTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(task))
            {
                tasks.Add(task);
                taskTextBox.Clear();
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
            if (taskListBox.SelectedIndex != -1)
            {
                int selectedIndex = taskListBox.SelectedIndex;
                tasks[selectedIndex] = "[Completed] " + tasks[selectedIndex];
            }
        }

        
    }
}
