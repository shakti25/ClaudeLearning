# Progreso — Plan de Estudio Claude

> Este archivo es el registro vivo del avance. Pide "resumen del plan de estudio" en cualquier momento y se usa esto para ponerte al día.

## Estado actual

- **Plan:** [plan-estudio-claude.md](plan-estudio-claude.md) — 🟢 Aprobado (2026-08-07), 17 sesiones
- **Sesión actual:** Ninguna iniciada todavía
- **Última sesión completada:** Día 7 — Crear sub-agentes propios + selección de modelo (2026-08-09)
- **Próxima sesión sugerida:** Día 8 — MCP: fundamentos
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
| 2026-08-09 | Día 5 | Contenido: loop ReAct (razonar-actuar-observar) con nombre formal, planner-executor, distinción agente vs. sub-agente (contexto propio, rol acotado), qué agrega el Agent SDK sobre Claude Code. Ejercicio: el usuario escribió `docs/dia5-loop-agentico.md` reflexionando sobre la tarea real del Día 1 (cancelación de tareas), identificando decisiones autónomas del agente; surgió una observación valiosa sobre riesgo de "test gaming" (debilitar/eliminar tests para lograr verde en vez de arreglar la causa), conectada a Día 16 (evaluación) y Día 11 (seguridad). Se discutieron palancas de prevención disponibles ya (pedir comparación explícita de opciones, modo Plan, extended thinking, sub-agente revisor). | Arrancar Día 6 en la próxima sesión — sub-agentes built-in y patrón orquestador. |
| 2026-08-09 | Día 6 | Contenido: aislamiento de contexto de sub-agentes, agentes built-in (`Explore`/`Plan`/`general-purpose`), model tiering (costo/latencia/calidad). Ejercicio: se lanzó `Explore` sobre `practice-project/src/Api` (no investigada desde el Día 2) — encontró que `Cancel` (agregado Día 1) nunca se expuso por HTTP, que no hay tests de la capa Api, y que `CreateTask` no valida `Title` vacío. Se simuló el patrón orquestador asignando modelo (Haiku/Opus/Sonnet) a 3 tareas reales derivadas de esos hallazgos, con buen criterio del usuario. Profundización adicional: mecánica real de invocación (`subagent_type`/`model`/`run_in_background`), diferencia sub-agente vs. skill vs. slash command (contexto aislado, paralelismo, independencia de criterio para auditar el propio trabajo), sub-agentes pueden usar skills (si tienen la tool `Skill`) pero no "invocan" slash commands (son expansión de texto a nivel de input humano, no tool call). | Arrancar Día 7 en la próxima sesión — crear sub-agente propio con selección de modelo real, no simulada. Candidatos de práctica ya identificados: exponer `Cancel` por HTTP, crear `Api.Tests`, validación de inputs. |
| 2026-08-09 | — (ajuste al plan) | El usuario notó que los Días 1-6 se quedaron en teoría+concepto sin cubrir la mecánica real de invocación (cómo se usa cada cosa en la práctica, no solo qué es). Se actualizó `plan-estudio-claude.md`: cada día desde el Día 7 en adelante suma una sección **"Mecánica de invocación"** con comandos concretos, sintaxis real y ejemplos, siguiendo el mismo nivel de detalle que se dio recién para sub-agentes (Día 6). | Sin pendientes — el ajuste ya quedó aplicado en el plan. |
| 2026-08-09 | Día 7 | Contenido: estructura de `.claude/agents/*.md` (frontmatter `name`/`description`/`tools`/`model`), por qué restringir `tools` (menor privilegio), diferencias Haiku/Sonnet/Opus, extended thinking y prompt caching como palancas de costo, `isolation: worktree`. Ejercicio: se creó `revisor-dotnet` (solo lectura, sin `Edit`/`Write`) con `model: sonnet`, justificado por requerir juicio sobre consistencia entre capas y no solo pattern-matching contra `CLAUDE.md`. El agente propio no se auto-registró en la sesión (ni tras reiniciar VSCode — la conversación siguió siendo la misma sesión de fondo), a diferencia de la skill del Día 4 que sí apareció en caliente; se corrió el ejercicio con `general-purpose` + mismo modelo/rol como equivalente funcional. Reporte real obtenido: confirmó `Cancel` sin exponer por HTTP (alta), detectó falta de `StudyTaskTests.cs` dedicado violando convención propia del `CLAUDE.md` (alta), riesgo de `Title` nulo vía JSON binding pese a `Nullable` habilitado (media), cobertura floja de `IsCancelled` en el test de seed (baja). | Confirmar en una sesión de Claude Code genuinamente nueva (no solo ventana de VSCode reiniciada) si `revisor-dotnet` se auto-detecta. Arrancar Día 8 en la próxima sesión — MCP fundamentos. Los 4 hallazgos del revisor quedan como backlog real de `practice-project/` para ejercicios futuros. |

## Bloqueos o dudas abiertas

_(vacío)_
