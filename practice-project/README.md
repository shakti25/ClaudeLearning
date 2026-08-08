# Practice Project

Proyecto .NET usado como campo de pruebas para el [plan de estudio de Claude](../docs/plan-estudio-claude.md).

## Estructura

```
practice-project/
├── src/
│   ├── Domain/   -> Entidad StudyTask + StudyTaskRepository (en memoria)
│   └── Api/      -> API mínima (ASP.NET Core) sobre el dominio
└── tests/
    └── Domain.Tests/ -> Tests xUnit del dominio
```

## Comandos

```bash
dotnet build
dotnet test
dotnet run --project src/Api
```

## Data de prueba

En `Development`, `Program.cs` llama a `StudyTaskRepository.SeedSampleData()` al arrancar
la API para no empezar con el repositorio vacío. Es solo para desarrollo/ejercicios.

## Endpoints (API)

- `GET  /tasks` — lista todas las tareas
- `GET  /tasks/pending` — lista tareas pendientes
- `POST /tasks` — crea una tarea (`{ "title": "...", "dueDate": "2026-08-10" }`)
- `POST /tasks/{id}/complete` — marca una tarea como completada

Este dominio simple (tareas de estudio) es intencional: a lo largo del plan se usa para
practicar skills, sub-agentes, un servidor MCP propio, hooks, y la integración con la
Claude API (tool use) — ver `docs/plan-estudio-claude.md` en la raíz del repo.
