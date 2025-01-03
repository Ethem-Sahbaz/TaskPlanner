using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskPlanner.WPF.Tasks;

namespace TaskPlanner.WPF;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<TaskListItemViewModel> _taskLists = new();
    
    [ObservableProperty]
    private TaskListItemViewModel? _selectedTask;
    
    public MainViewModel()
    {
        InitializeViewModel();
    }

    private void InitializeViewModel()
    {

        // Dummy data
        for (int j = 0; j < 4; j++)
        {
            var taskList = new TaskListItemViewModel(){ Title = $"Task List {j}" };

            for (int i = 0; i < 6; i++)
            {
                var task = new TaskItemViewModel(){ Title = "Task " + i };
                taskList.Tasks.Add(task);
            }
            
            TaskLists.Add(taskList);
        }

    }

}