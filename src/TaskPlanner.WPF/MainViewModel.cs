using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

    [ObservableProperty]
    private string? _newTaskName;

    [ObservableProperty]
    private DateTime _selectedDate= DateTime.Now;

    [ObservableProperty]
    private bool _showTaskNameRequiredErrorMessage;

    public bool ShowRelaxImage => Tasks.Count == 0;
    
    public MainViewModel(TaskJsonManager jsonManager)
    {
        _jsonManager = jsonManager;

        InitializeViewModel();
        Tasks.CollectionChanged += OnTasksCollectionChanged;
    }

    [RelayCommand]
    public void CreateTask()
    {
        if (string.IsNullOrEmpty(NewTaskName))
        {
            ShowTaskNameRequiredErrorMessage = true;

            return;
        }

        var newTask = new TaskItemViewModel(NewTaskName, SelectedDate);

        newTask.PropertyChanged += OnTaskCheckedChanged;

        Tasks.Add(newTask);

        NewTaskName = string.Empty;
        ShowTaskNameRequiredErrorMessage = false;
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
        OnPropertyChanged(nameof(ShowRelaxImage));
    }

    private void OnTaskCheckedChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is TaskItemViewModel task &&
            e.PropertyName == nameof(task.IsChecked))
        {
            task.PropertyChanged -= OnTaskCheckedChanged;

            Tasks.Remove(task);
        }
        
    }

}