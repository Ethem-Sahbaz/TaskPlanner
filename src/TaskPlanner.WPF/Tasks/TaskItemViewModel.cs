using CommunityToolkit.Mvvm.ComponentModel;

namespace TaskPlanner.WPF.Tasks;

public partial class TaskItemViewModel : ObservableObject
{
    public TaskItemViewModel(string title, DateTime dueDate)
    {
        _title = title;
        _dueDate = dueDate;
    }

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDue))]
    private DateTime _dueDate;

    [ObservableProperty]
    private bool _isChecked;

    public string FormattedDate => DueDate.ToString("dd.MM.yyyy");
    public bool IsDue => DueDate.Date < DateTime.Now.Date;
    

}