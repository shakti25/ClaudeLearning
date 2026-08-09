namespace Domain;

/// <summary>
/// Repositorio en memoria, deliberadamente simple.
/// Punto de extensión pensado para los ejercicios de MCP / tool use
/// (ej. exponer estos métodos como "tools" que Claude pueda llamar).
/// </summary>
public class StudyTaskRepository
{
    private readonly List<StudyTask> _tasks = [];

    public StudyTask Add(string title, DateOnly? dueDate = null)
    {
        var task = new StudyTask { Title = title, DueDate = dueDate };
        _tasks.Add(task);
        return task;
    }

    public IReadOnlyList<StudyTask> GetAll() => _tasks.AsReadOnly();

    public IReadOnlyList<StudyTask> GetPending() =>
        _tasks.Where(t => !t.IsCompleted && !t.IsCancelled).ToList();

    public bool Complete(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task is null) return false;
        task.Complete();
        return true;
    }

    public bool Cancel(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task is null) return false;
        task.Cancel();
        return true;
    }

    /// <summary>
    /// Carga data de prueba para no arrancar con el repositorio vacío.
    /// Pensado solo para desarrollo/ejercicios, no para producción.
    /// </summary>
    public void SeedSampleData()
    {
        Add("Configurar CLAUDE.md del proyecto", DateOnly.FromDateTime(DateTime.Today.AddDays(1)));
        Add("Crear skill para generar tests xUnit", DateOnly.FromDateTime(DateTime.Today.AddDays(3)));
        var completed = Add("Explorar sub-agentes built-in (Explore, Plan)");
        completed.Complete();
    }
}
