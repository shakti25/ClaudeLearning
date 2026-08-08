using Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<StudyTaskRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.Services.GetRequiredService<StudyTaskRepository>().SeedSampleData();
}

app.UseHttpsRedirection();

app.MapGet("/tasks", (StudyTaskRepository repo) => repo.GetAll())
    .WithName("GetTasks");

app.MapGet("/tasks/pending", (StudyTaskRepository repo) => repo.GetPending())
    .WithName("GetPendingTasks");

app.MapPost("/tasks", (StudyTaskRepository repo, CreateTaskRequest request) =>
    {
        var task = repo.Add(request.Title, request.DueDate);
        return Results.Created($"/tasks/{task.Id}", task);
    })
    .WithName("CreateTask");

app.MapPost("/tasks/{id:guid}/complete", (StudyTaskRepository repo, Guid id) =>
        repo.Complete(id) ? Results.NoContent() : Results.NotFound())
    .WithName("CompleteTask");

app.Run();

record CreateTaskRequest(string Title, DateOnly? DueDate);
