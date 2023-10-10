using System.Collections.ObjectModel;
using System.ComponentModel;
using ToDoApp.Model;

namespace ToDoApp.ViewModel
{
    // ViewModel class for managing tasks
    public class TaskListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TaskToDo> tasks;

        // Collection of tasks that will be displayed
        public ObservableCollection<TaskToDo> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        // Constructor for initializing the ViewModel
        public TaskListViewModel()
        {
            Tasks = new ObservableCollection<TaskToDo>(); // Initialize the Tasks collection
        }

        // Method for handling property change events
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Event for notifying when a property changes
        public event PropertyChangedEventHandler PropertyChanged;
    }
}