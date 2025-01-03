using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TaskPlanner.WPF.Tasks;

public partial class TaskListItemViewModel : ObservableObject
{
    [ObservableProperty]
    private Guid _id;
    
    [ObservableProperty]
    private string _title;
    
    [ObservableProperty]
    private ObservableCollection<TaskItemViewModel> _tasks = new();
    
}