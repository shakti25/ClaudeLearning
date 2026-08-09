namespace Domain;

/// <summary>
/// Entidad simple del dominio: una tarea de estudio.
/// Sirve como "campo de pruebas" para los ejercicios del plan de estudio
/// (docs/plan-estudio-claude.md) — skills, sub-agentes, MCP, tool use, etc.
/// </summary>
public class StudyTask
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public bool IsCancelled { get; set; }
    public DateOnly? DueDate { get; set; }

    public void Complete() => IsCompleted = true;
    public void Cancel() => IsCancelled = true;
}
