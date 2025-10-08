using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Todos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Google.Apis.Auth.OAuth2;
using System.Text;

namespace Todos.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly IOptions<FirestoreSettings> _firestoreSettings;
    private readonly FirestoreDb _db;

    public TodoController(IOptions<FirestoreSettings> firestoreSettings)
    {
        _firestoreSettings = firestoreSettings;

        var base64Config = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS_BASE64");
        var jsonConfig = Encoding.UTF8.GetString(Convert.FromBase64String(base64Config));
        var credential = GoogleCredential.FromJson(jsonConfig);

        _db = new FirestoreDbBuilder { ProjectId = Environment.GetEnvironmentVariable("TODO_API_FIRESTOREDB_PROJECT"), Credential = credential }.Build();
    }
    
    [Authorize]
    [HttpGet("GetTodos")]
    public async Task<IEnumerable<Todo>> GetAll()
    {
        var todosCollection = _db.Collection(_firestoreSettings.Value.Collection);
        var snapshot = await todosCollection.GetSnapshotAsync();

        var todos = snapshot.Documents.Select(docSnapshot => docSnapshot.ToDictionary()).Select(todoDictionary => new Todo(todoDictionary["sys_id"] as string, todoDictionary["description"] as string, (bool)todoDictionary["completed"])).ToList();
        
        return todos;
    }
    
    [Authorize]
    [HttpPost("CreateTodo")]
    public async Task<IActionResult> Create([FromBody] Todo newTodo)
    {
        Dictionary<string, object> todoDictionary = new Dictionary<string, object>
        {
            { "sys_id", newTodo.SysId },
            { "description", newTodo.Description },
            { "completed", newTodo.Completed }
        };
        
        DocumentReference result = await _db.Collection(_firestoreSettings.Value.Collection).AddAsync(todoDictionary);
        
        return Created();
    }
}