# practice-project

Campo de pruebas .NET para el [plan de estudio de Claude](../docs/plan-estudio-claude.md).
Dominio de "tareas de estudio", deliberadamente simple, usado en ejercicios de skills,
sub-agentes, MCP, hooks y tool use.

## Stack

- .NET 10, C# (`Nullable` y `ImplicitUsings` habilitados en todos los proyectos).
- ASP.NET Core (API mínima) para `src/Api`.
- xUnit para tests.

## Estructura de carpetas

```
practice-project/
├── PracticeProject.slnx
├── src/
│   ├── Domain/        # Entidades y lógica de negocio, sin dependencias externas.
│   │   ├── StudyTask.cs             # Entidad: Id, Title, IsCompleted, IsCancelled, DueDate
│   │   └── StudyTaskRepository.cs   # Repositorio en memoria (sin persistencia real)
│   └── Api/            # API mínima ASP.NET Core que expone el dominio por HTTP
│       └── Program.cs               # Endpoints: GET/POST /tasks, POST /tasks/{id}/complete
└── tests/
    └── Domain.Tests/   # Tests xUnit del dominio
```

`src/Domain` no depende de `src/Api` ni de ningún framework — mantenerlo así.

## Comandos

```bash
dotnet build PracticeProject.slnx          # compilar todo
dotnet test tests/Domain.Tests/Domain.Tests.csproj   # correr tests del dominio
dotnet format PracticeProject.slnx         # aplicar estilo de .editorconfig
dotnet run --project src/Api               # levantar la API (con seed data en Development)
```

Antes de dar por terminado un cambio: `dotnet test` debe pasar en verde.

## Estilo de código

Definido en `.editorconfig` (raíz de `practice-project/`), aplicable con `dotnet format`.
Puntos clave:
- 4 espacios de indentación, llaves en línea nueva (Allman).
- Campos privados: `_camelCase`. Tipos: `PascalCase`.
- `var` cuando el tipo es evidente; preferir tipos predefinidos de C# (`string` en vez de `String`).
- Miembros expresados con `=>` cuando entran en una sola línea (ver `Complete()`/`Cancel()` en `StudyTask`).

## Convenciones del dominio

- Los métodos de mutación viven en la entidad, no en el repositorio (ej. `StudyTask.Complete()`,
  no lógica de completado inline en `StudyTaskRepository`). El repositorio solo busca y delega.
- Estados de una tarea son flags booleanos independientes (`IsCompleted`, `IsCancelled`), no un
  enum — así se estableció con la primera propiedad y se mantuvo por consistencia al día 1.
- `GetPending()` excluye tanto completadas como canceladas.
- `SeedSampleData()` es solo para desarrollo/ejercicios (se llama en `Program.cs` bajo
  `Environment.IsDevelopment()`), nunca debe usarse como dato real.

## Tests

- Un archivo de tests por clase bajo prueba (`StudyTaskRepositoryTests.cs`), método
  `Metodo_Comportamiento_Cuando` como nombre de test (ver tests existentes).
- Patrón Arrange-Act-Assert sin comentarios explícitos separando las secciones.
