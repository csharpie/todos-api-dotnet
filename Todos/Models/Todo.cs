using Newtonsoft.Json;

namespace Todo.Models;

public class Todo
{
    [JsonProperty("sys_id")]
    public string SysId { get; set; }
    
    public string Description { get; set; }
    
    public bool Completed { get; set; }

    public Todo(string sysId, string description, bool completed)
    {
        SysId = sysId;
        Description = description;
        Completed = completed;
    }
}