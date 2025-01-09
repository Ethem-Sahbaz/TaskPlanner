using CommunityToolkit.Mvvm.ComponentModel;

namespace TaskPlanner.WPF.Tasks;

/// <summary>
/// ViewModel for a task item.
/// </summary>
public partial class TaskItemViewModel : ObservableObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TaskItemViewModel"/> class.
    /// </summary>
    /// <param name="title">The title of the task.</param>
    /// <param name="dueDate">The due date of the task.</param>
    public TaskItemViewModel(string title, DateTime dueDate)
    {
        _title = title;
        _dueDate = dueDate;
    }

    /// <summary>
    /// Gets or sets the title of the task.
    /// </summary>
    [ObservableProperty]
    private string _title;

    /// <summary>
    /// Gets or sets the due date of the task.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDue))]
    private DateTime _dueDate;

    /// <summary>
    /// Gets or sets a value indicating whether the task is checked.
    /// </summary>
    [ObservableProperty]
    private bool _isChecked;

    /// <summary>
    /// Gets the formatted due date of the task.
    /// </summary>
    public string FormattedDate => DueDate.ToString("dd.MM.yyyy");

    /// <summary>
    /// Gets a value indicating whether the task is due.
    /// </summary>
    public bool IsDue => DueDate.Date < DateTime.Now.Date;
}
