using CommunityToolkit.Mvvm.ComponentModel;

namespace TaskPlanner.WPF.Tasks;

public partial class TaskItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private bool _isChecked;
}