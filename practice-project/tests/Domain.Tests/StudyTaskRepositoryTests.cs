using Domain;

namespace Domain.Tests;

public class StudyTaskRepositoryTests
{
    [Fact]
    public void Add_CreatesTaskAsPending()
    {
        var repo = new StudyTaskRepository();

        var task = repo.Add("Día 1 - Cómo piensa Claude Code");

        Assert.False(task.IsCompleted);
        Assert.Contains(task, repo.GetPending());
    }

    [Fact]
    public void Add_WithoutDueDate_LeavesDueDateNull()
    {
        var repo = new StudyTaskRepository();

        var task = repo.Add("Día 4 - Crear skill propia");

        Assert.Null(task.DueDate);
    }

    [Fact]
    public void Complete_MarksTaskAsCompleted_AndRemovesFromPending()
    {
        var repo = new StudyTaskRepository();
        var task = repo.Add("Día 2 - CLAUDE.md y memoria");

        var result = repo.Complete(task.Id);

        Assert.True(result);
        Assert.DoesNotContain(task, repo.GetPending());
    }

    [Fact]
    public void Complete_ReturnsFalse_WhenTaskDoesNotExist()
    {
        var repo = new StudyTaskRepository();

        var result = repo.Complete(Guid.NewGuid());

        Assert.False(result);
    }

    [Fact]
    public void Cancel_MarksTaskAsCancelled_AndRemovesFromPending()
    {
        var repo = new StudyTaskRepository();
        var task = repo.Add("Día 1 - Ejercicio de exploración");

        var result = repo.Cancel(task.Id);

        Assert.True(result);
        Assert.True(task.IsCancelled);
        Assert.DoesNotContain(task, repo.GetPending());
    }

    [Fact]
    public void SeedSampleData_AddsTasksWithAtLeastOneCompleted()
    {
        var repo = new StudyTaskRepository();

        repo.SeedSampleData();

        Assert.NotEmpty(repo.GetAll());
        Assert.Contains(repo.GetAll(), t => t.IsCompleted);
        Assert.NotEmpty(repo.GetPending());
    }
}
