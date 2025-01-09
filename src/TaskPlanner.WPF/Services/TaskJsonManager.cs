using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskPlanner.WPF.Tasks;

namespace TaskPlanner.WPF.Services;
/// <summary>
/// Manages reading and writing tasks to and from a JSON file.
/// </summary>
public sealed class TaskJsonManager
{
    /// <summary>
    /// The file path where tasks are stored.
    /// </summary>
    private const string filepath = "Data/Tasks.json";

    /// <summary>
    /// Reads tasks from the JSON file.
    /// </summary>
    /// <returns>A list of TaskItemViewModel objects.</returns>
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

    /// <summary>
    /// Writes tasks to the JSON file.
    /// </summary>
    /// <param name="tasks">The list of TaskItemViewModel objects to write.</param>
    public void WriteTasksToJson(List<TaskItemViewModel> tasks)
    {
        var json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(filepath, json);
    }
}
