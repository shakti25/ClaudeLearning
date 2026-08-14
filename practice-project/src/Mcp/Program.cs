using System.ComponentModel;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

var builder = Host.CreateApplicationBuilder(args);

// Transport stdio: stdout está reservado para los mensajes JSON-RPC del protocolo.
// Cualquier log tiene que ir a stderr, o corrompe la conversación MCP.
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Trace;
});

// Un único repositorio en memoria para todo el proceso del servidor, con datos de ejemplo
// (igual que hace la Api en Development) para tener algo que consultar/cancelar de entrada.
builder.Services.AddSingleton(_ =>
{
    var repo = new StudyTaskRepository();
    repo.SeedSampleData();
    return repo;
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

var app = builder.Build();
await app.RunAsync();

[McpServerToolType]
public static class StudyTaskTools
{
    [McpServerTool, Description("Lista las tareas de estudio pendientes (no completadas ni canceladas).")]
    public static IReadOnlyList<StudyTask> ListPendingTasks(StudyTaskRepository repo) =>
        repo.GetPending();

    [McpServerTool, Description("Cancela una tarea de estudio por su Id (GUID).")]
    public static string CancelTask(
        StudyTaskRepository repo,
        [Description("Id (GUID) de la tarea a cancelar")] Guid id)
    {
        return repo.Cancel(id)
            ? "Tarea cancelada correctamente."
            : "No se encontró ninguna tarea con ese Id.";
    }
}
