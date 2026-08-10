# Progreso — Plan de Estudio Claude

> Este archivo es el registro vivo del avance. Pide "resumen del plan de estudio" en cualquier momento y se usa esto para ponerte al día.

## Estado actual

- **Plan:** [plan-estudio-claude.md](plan-estudio-claude.md) — 🟢 Aprobado (2026-08-07), 17 sesiones
- **Sesión actual:** Ninguna iniciada todavía
- **Última sesión completada:** Día 4 — Crear tu propia Skill para .NET (2026-08-09)
- **Próxima sesión sugerida:** Día 5 — Agentes: el concepto general (cierra el Bloque 1, arranca Bloque 2)
- **Proyecto de práctica (`practice-project/`):** ✅ Creado (2026-08-07)

## Resumen general

Plan aprobado el 2026-08-07 con 17 sesiones (~2h/día). Se creó `practice-project/`: solución
.NET (`PracticeProject.slnx`) con `src/Domain` (entidad `StudyTask` + `StudyTaskRepository` en
memoria), `src/Api` (API mínima ASP.NET Core sobre el dominio) y `tests/Domain.Tests` (xUnit,
3 tests pasando). Build y tests verificados OK. Este dominio de "tareas de estudio" es el que
se usará en los ejercicios de skills, sub-agentes, MCP, hooks y tool use a lo largo del plan.

Día 1 completado el 2026-08-08: ciclo agentic (leer → planear → actuar → verificar), modelo de
contexto y herramientas/permisos, aplicado con un ejercicio real sobre `practice-project/`
(agregar cancelación de `StudyTask` sin indicar archivos exactos).

## Bitácora de sesiones

| Fecha | Día del plan | Qué se hizo | Pendientes / notas |
|---|---|---|---|
| 2026-08-07 | — (setup) | Plan aprobado. Se creó `practice-project/` (Domain + Api + tests xUnit), build y tests verificados. | Arrancar Día 1 en la próxima sesión. |
| 2026-08-07 | — (setup) | Se agregó `SeedSampleData()` al repositorio (3 tareas de ejemplo, 1 completada) y se ejecuta automáticamente en `Development`. Nuevo test agregado (4/4 pasando). | — |
| 2026-08-08 | Día 1 | Contenido: ciclo agentic, modelo de contexto, tool calling, permisos. Ejercicio: se le pidió a Claude agregar cancelación de tareas (`IsCancelled`/`Cancel()`) sin nombrar archivos; exploró (`Read`/`Glob`), siguió el patrón existente (`Complete()`), editó `StudyTask.cs`, `StudyTaskRepository.cs` y los tests, y verificó con `dotnet test` (5/5 pasando). | Arrancar Día 2 en la próxima sesión. |
| 2026-08-09 | Día 2 | Contenido: jerarquía de `CLAUDE.md` (usuario/proyecto/carpeta), trade-off de contexto, `settings.json` vs. modo de permiso de la UI, `/memory` vs. sistema de memoria (`memory/`+`MEMORY.md`), buenas/malas prácticas para inferir convenciones en repos grandes (reporte + aprobación humana + enforcement en build/CI). Ejercicio: se creó `.editorconfig` para `practice-project/`, se aplicó con `dotnet format` (5/5 tests OK) y se escribió `CLAUDE.md` documentando stack, estructura, comandos, estilo y convenciones del dominio. Se probó `/memory` (delega a terminal en la extensión IDE) y se guardó una memoria real de preferencia del usuario (explicaciones didácticas). | Arrancar Día 3 en la próxima sesión. |
| 2026-08-09 | Día 3 | Contenido: diferencia slash command personalizado (siempre manual, 1 archivo) vs. skill (puede autoactivarse por `description`, carpeta con múltiples archivos, `allowed-tools`, `disable-model-invocation`); helpers existentes para generar skills/commands (recomendador `claude-automation-recommender`, creador guiado); preview de sub-agentes usando skills y gestión de contexto entre sub-agentes (se profundiza Día 6). Ejercicio: se exploraron 3 skills reales instaladas (`claude-security`, `frontend-design`, `hookify/writing-rules`) comparando su anatomía. | Arrancar Día 4 en la próxima sesión — crear skill propia para .NET. |
| 2026-08-09 | Día 4 | Contenido: estructura de carpeta de una skill (`.claude/skills/<nombre>/SKILL.md`, convención carpeta=`name`), frontmatter YAML, ubicación de `disable-model-invocation`. Ejercicio: se creó la skill `gen-test-xunit` en `practice-project/.claude/skills/` (genera tests xUnit siguiendo el patrón de `Domain.Tests`, con `allowed-tools` acotado y verificación con `dotnet test`). Se probó invocación explícita vía `Skill` tool — funcionó de punta a punta, generó `Add_WithoutDueDate_LeavesDueDateNull` (6/6 tests OK). Quedó pendiente confirmar auto-activación silenciosa en una sesión nueva. | Arrancar Día 5 en la próxima sesión (cierra Bloque 1, arranca Bloque 2 de Agentes). |

## Bloqueos o dudas abiertas

_(vacío)_
