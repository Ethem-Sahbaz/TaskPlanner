using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskPlanner.WPF.Tasks;

namespace TaskPlanner.WPF.Services;
public sealed class TaskJsonManager
{
    private const string filepath = "Data/Tasks.json";
    public List<TaskItemViewModel> ReadTasksFromJson()
    {
        var json = File.ReadAllText(filepath);
        var tasks = new List<TaskItemViewModel>();

        try
        {
            tasks = JsonSerializer.Deserialize<List<TaskItemViewModel>>(json);
        }
        catch (JsonException)
        {

            return tasks!;
        }

        return tasks!;
    }

    public void WriteTasksToJson(List<TaskItemViewModel> tasks)
    {
        var json = JsonSerializer.Serialize(tasks);

        File.WriteAllText(filepath, json);
    }
}
