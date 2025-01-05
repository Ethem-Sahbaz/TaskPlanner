using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskPlanner.WPF.Services;
using TaskPlanner.WPF.Tasks;

namespace TaskPlanner.WPF;

public partial class MainViewModel : ObservableObject
{
    private readonly TaskJsonManager _jsonManager;
    
    [ObservableProperty]
    private ObservableCollection<TaskItemViewModel> _tasks = new();
    
    public MainViewModel(TaskJsonManager jsonManager)
    {
        _jsonManager = jsonManager;

        InitializeViewModel();
        Tasks.CollectionChanged += OnTasksCollectionChanged;
    }

    private void InitializeViewModel()
    {
       var tasks = _jsonManager.ReadTasksFromJson();

        foreach (var task in tasks) 
        {
            task.PropertyChanged += OnTaskCheckedChanged;
            Tasks.Add(task);
        }

    }

    /// <summary>
    /// Saves the lists everytime the collection changes.
    /// </summary>
    private void OnTasksCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        _jsonManager.WriteTasksToJson(Tasks.ToList());
    }

    private void OnTaskCheckedChanged(object? sender, PropertyChangedEventArgs e)
    {
        
        if (sender is TaskItemViewModel task &&
            e.PropertyName == nameof(task.IsChecked))
        {
            
        }
        var test = nameof(task.IsChecked);
        
    }

}