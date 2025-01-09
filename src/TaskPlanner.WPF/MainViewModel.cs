using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskPlanner.WPF.Services;
using TaskPlanner.WPF.Tasks;

namespace TaskPlanner.WPF;

/// <summary>
/// Represents the main view model for the Task Planner application.
/// </summary>
public partial class MainViewModel : ObservableObject
{
    /// <summary>
    /// Manages the JSON operations for tasks.
    /// </summary>
    private readonly TaskJsonManager _jsonManager;

    /// <summary>
    /// Collection of task items.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<TaskItemViewModel> _tasks = new();

    /// <summary>
    /// Name of the new task to be created.
    /// </summary>
    [ObservableProperty]
    private string? _newTaskName;

    /// <summary>
    /// Selected date for the new task.
    /// </summary>
    [ObservableProperty]
    private DateTime _selectedDate = DateTime.Now;

    /// <summary>
    /// Indicates whether the error message for required task name should be shown.
    /// </summary>
    [ObservableProperty]
    private bool _showTaskNameRequiredErrorMessage;

    /// <summary>
    /// Indicates whether to show the relax image when there are no tasks.
    /// </summary>
    public bool ShowRelaxImage => Tasks.Count == 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="jsonManager">The JSON manager for tasks.</param>
    public MainViewModel(TaskJsonManager jsonManager)
    {
        _jsonManager = jsonManager;

        InitializeViewModel();
        Tasks.CollectionChanged += OnTasksCollectionChanged;
    }

    /// <summary>
    /// Creates a new task and adds it to the task collection.
    /// </summary>
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

    /// <summary>
    /// Initializes the view model by reading tasks from JSON.
    /// </summary>
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
    /// Handles the collection changed event to save tasks to JSON and update properties.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void OnTasksCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        _jsonManager.WriteTasksToJson(Tasks.ToList());
        OnPropertyChanged(nameof(ShowRelaxImage));
    }

    /// <summary>
    /// Handles the property changed event for task items to remove completed tasks.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void OnTaskCheckedChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is TaskItemViewModel task && e.PropertyName == nameof(task.IsChecked))
        {
            task.PropertyChanged -= OnTaskCheckedChanged;
            Tasks.Remove(task);
        }
    }
}
