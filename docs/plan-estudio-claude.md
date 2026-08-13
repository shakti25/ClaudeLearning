# Plan de Estudio: Profundizando en Claude (Claude Code + Claude API)

> **Estado:** 🟢 Aprobado
> **Creado:** 2026-08-07 · **Última actualización:** 2026-08-07 · **Aprobado:** 2026-08-07
> **Duración:** ~2.5 semanas · ~2h/día (~34h totales, 17 sesiones)
> **Enfoque:** Claude Code (uso avanzado del CLI/agentes) con peso mayor, + fundamentos de la API/SDK de Claude para integrarlo en apps .NET
> **Perfil:** Developer .NET, ya usa Claude vía prompting básico

---

## 🎯 Objetivos del plan

Al terminar, deberías poder:

1. Configurar y aprovechar Claude Code a nivel avanzado (memoria, permisos, hooks, contexto).
2. Entender qué hace "agéntico" a un sistema y aplicar ese razonamiento a tu propio trabajo.
3. Crear y usar **Skills** propias adaptadas a tu flujo de trabajo .NET.
4. Diseñar y orquestar **Sub-agentes**, incluyendo estrategias de **model tiering** (orquestador con Opus/Sonnet delegando a Haiku/Sonnet).
5. Entender y conectar servidores **MCP** (Model Context Protocol), incluyendo construir uno propio en .NET.
6. Aplicar buenas prácticas de **seguridad** al dar herramientas/datos a un agente.
7. Empaquetar todo lo anterior en un **Plugin** reutilizable.
8. Usar la **Claude API / Agent SDK** para integrar Claude directamente en una app .NET (tool use, function calling, agentic loop).
9. Tener una noción básica de cómo **evaluar** si un agente funciona de forma confiable.
10. Tener un proyecto .NET de práctica (`practice-project/`) donde cada tema se aplicó con un ejercicio real.

---

## 🗂️ Estructura del plan

4 bloques, 17 sesiones de ~2h. Cada sesión tiene: **objetivo**, **contenido**, **ejercicio práctico** y una casilla para marcar como completada.

Para actualizar tu progreso, marca `- [ ]` como `- [x]` y anota la fecha real. También puedes decirme "actualiza el progreso" y lo hago junto con [progreso.md](progreso.md).

---

## Bloque 1 — Fundamentos avanzados de Claude Code (días 1-4)

### Día 1 — Cómo piensa y opera Claude Code
- [x] **Objetivo:** Entender el modelo de contexto, herramientas, permisos y el ciclo agentic (leer → planear → actuar → verificar).
- **Contenido:** Arquitectura de Claude Code, tool calling, modos de permiso, gestión de contexto/compactación.
- **Ejercicio:** Ejecutar una tarea simple en tu proyecto .NET dejando que Claude explore el código sin darle instrucciones exactas de archivos; observar qué herramientas usa y por qué.
- **Fecha real:** 2026-08-08

### Día 2 — CLAUDE.md, configuración y memoria
- [x] **Objetivo:** Dominar `CLAUDE.md`, `settings.json`, permisos granulares y memoria persistente entre sesiones.
- **Contenido:** Jerarquía de configuración (usuario/proyecto/local), buenas prácticas para `CLAUDE.md`, cómo funciona la memoria de proyecto.
- **Ejercicio:** Crear un `CLAUDE.md` real para `practice-project/` con convenciones de tu stack .NET (estilo, estructura de carpetas, comandos de build/test).
- **Fecha real:** 2026-08-09

### Día 3 — Slash Commands y Skills (introducción)
- [x] **Objetivo:** Entender qué es una Skill, cuándo se activa, y la diferencia con un slash command simple.
- **Contenido:** Anatomía de una skill (frontmatter, triggers, instrucciones), skills que ya usas sin saberlo (dataviz, code-review, etc.).
- **Ejercicio:** Explorar 2-3 skills existentes en tu entorno y documentar cómo están estructuradas.
- **Fecha real:** 2026-08-09

### Día 4 — Crear tu propia Skill para .NET
- [x] **Objetivo:** Construir una skill personalizada que resuelva algo repetitivo en tu flujo .NET.
- **Contenido:** Estructura de carpeta de una skill, buenas prácticas de triggers, testing manual de la skill.
- **Ejercicio:** Crear una skill propia (ej. "generar un test unitario xUnit siguiendo el patrón del proyecto" o "revisar convenciones de nombres C#") y probarla en `practice-project/`.
- **Fecha real:** 2026-08-09

---

## Bloque 2 — Agentes, Sub-agentes y Orquestación (días 5-8)

### Día 5 — Agentes: el concepto general
- [x] **Objetivo:** Entender qué hace que un sistema sea "agéntico" y distinguir *agente* de *sub-agente*.
- **Contenido:** Loop percibir → decidir → actuar → verificar, patrones comunes (ReAct, planner-executor), Claude Code como agente, qué añade el Agent SDK si quisieras construir un agente fuera de Claude Code.
- **Ejercicio:** Escribir en tus palabras (doc corto) el loop agéntico aplicado a una tarea real que le pediste antes a Claude por simple prompting — identificar qué decisiones tomó por su cuenta.
- **Fecha real:** 2026-08-09

### Día 6 — Sub-agentes built-in y el patrón orquestador
- [x] **Objetivo:** Entender los sub-agentes disponibles por defecto y el patrón de **model tiering**: un modelo capaz (Opus/Sonnet) orquesta y planea, delega ejecución a modelos más baratos/rápidos (Haiku/Sonnet) según la complejidad de cada sub-tarea.
- **Contenido:** Diferencia entre agente principal y sub-agente, aislamiento de contexto, agentes built-in (Explore, Plan, general-purpose), por qué delegar a un modelo distinto tiene sentido en costo/latencia/calidad.
- **Ejercicio:** Usar un sub-agente existente (ej. `Explore`) para investigar una parte de `practice-project/`; luego simular manualmente el patrón orquestador: tú decides qué modelo usarías para cada paso de una tarea real y por qué.
- **Fecha real:** 2026-08-09

### Día 7 — Crear sub-agentes propios + selección de modelo
- [ ] **Objetivo:** Diseñar un sub-agente personalizado con un rol específico y elegir su modelo con criterio (no por defecto).
- **Contenido:** Definición de agentes (`.claude/agents/*.md`), frontmatter (tools, model), diferencias prácticas Haiku/Sonnet/Opus (costo, velocidad, capacidad), extended thinking y prompt caching como palancas de costo, cuándo usar `isolation: worktree`, ejecución en background vs síncrona.
- **Ejercicio:** Crear un sub-agente "revisor-dotnet" (rol acotado) usando el modelo más barato que cumpla el trabajo, y ejecutarlo sobre un cambio real en `practice-project/`. Justificar la elección de modelo por escrito.
- **Fecha real:** _—_

### Día 8 — MCP: fundamentos
- [ ] **Objetivo:** Entender qué resuelve el Model Context Protocol y cómo se diferencia de una skill o tool nativa.
- **Contenido:** Arquitectura cliente-servidor de MCP, servidores MCP existentes (filesystem, GitHub, bases de datos), cómo se conectan a Claude Code.
- **Ejercicio:** Conectar un servidor MCP existente (ej. uno de filesystem o GitHub) a tu Claude Code y probar 2-3 llamadas.
- **Fecha real:** _—_

---

## Bloque 3 — MCP en profundidad, Seguridad y Plugins (días 9-13)

### Día 9 — Consumir MCP en un flujo real
- [ ] **Objetivo:** Integrar un servidor MCP a tu trabajo diario .NET (ej. acceso a una base de datos o a issues de un repo).
- **Contenido:** Configuración de servidores MCP en `settings.json`, scopes (usuario/proyecto).
- **Ejercicio:** Configurar un MCP server útil para tu contexto y resolver una tarea real usándolo.
- **Fecha real:** _—_

### Día 10 — Construir tu propio servidor MCP en .NET
- [ ] **Objetivo:** Entender el protocolo lo suficiente para exponer herramientas propias vía MCP desde una app .NET.
- **Contenido:** Especificación mínima de un servidor MCP (transport, tools/list, tools/call), SDKs/librerías disponibles para .NET (oficial o comunidad).
- **Ejercicio:** Crear un servidor MCP mínimo en `practice-project/` que exponga 1-2 herramientas simples (ej. "consultar estado de builds" o "listar entidades del dominio").
- **Fecha real:** _—_

### Día 11 — Seguridad y guardrails para agentes
- [ ] **Objetivo:** Entender los riesgos de dar herramientas/datos a un agente y cómo mitigarlos.
- **Contenido:** Prompt injection (directa e indirecta, ej. vía contenido de archivos/MCP), permisos granulares, sandboxing, por qué nunca confiar ciegamente en output de una tool externa, principio de menor privilegio al diseñar sub-agentes y MCP servers.
- **Ejercicio:** Revisar el servidor MCP y el sub-agente que creaste (días 7 y 10) e identificar qué pasaría si una fuente de datos que consumen fuera maliciosa; ajustar permisos/scopes en consecuencia.
- **Fecha real:** _—_

### Día 12 — Hooks: automatización del flujo
- [ ] **Objetivo:** Automatizar comportamientos con hooks (pre/post tool use, on-stop, etc.).
- **Contenido:** Tipos de hooks, casos de uso (formatear código automáticamente, correr tests antes de un commit, notificaciones).
- **Ejercicio:** Configurar un hook que corra `dotnet format` o `dotnet test` automáticamente tras cada edición en `practice-project/`.
- **Fecha real:** _—_

### Día 13 — Plugins: empaquetar todo
- [ ] **Objetivo:** Entender cómo un plugin agrupa skills, agentes, comandos y hooks en una unidad distribuible.
- **Contenido:** Estructura de un plugin, instalación/distribución, cuándo conviene crear uno vs. usar piezas sueltas.
- **Ejercicio:** Empaquetar la skill y el sub-agente creados en días 4 y 7 dentro de un plugin propio "toolkit-dotnet".
- **Fecha real:** _—_

---

## Bloque 4 — Claude API / Agent SDK, Evaluación y Capstone (días 14-17)

### Día 14 — Fundamentos de la Claude API
- [ ] **Objetivo:** Entender la Messages API, tool use/function calling y streaming desde el punto de vista de integración en código.
- **Contenido:** Autenticación, modelos disponibles, tool use loop manual, prompt caching aplicado a costos reales de producción.
- **Ejercicio:** Hacer una llamada simple a la API desde un script o consola .NET (sin SDK todavía) usando `HttpClient`.
- **Fecha real:** _—_

### Día 15 — Integrar Claude en una app .NET
- [ ] **Objetivo:** Construir un mini agentic loop en .NET: Claude decide llamar una "tool" tuya (método C#) y tú ejecutas el resultado.
- **Contenido:** Definición de tools en la API, manejo del loop de tool_use/tool_result, comparación con lo que hace Claude Code internamente.
- **Ejercicio:** En `practice-project/`, crear un endpoint o comando de consola donde Claude usa una tool C# real (ej. consultar datos en memoria) y responde con el resultado.
- **Fecha real:** _—_

### Día 16 — Evaluación de agentes
- [ ] **Objetivo:** Tener criterio básico para saber si un agente (o sub-agente) funciona de forma confiable, más allá de "lo probé una vez y anduvo".
- **Contenido:** Qué es un eval simple (casos de entrada/salida esperada), métricas básicas (¿resuelve la tarea?, ¿usa las tools correctas?, ¿es consistente?), cuándo vale la pena formalizar evals vs. probar manualmente.
- **Ejercicio:** Definir 3-5 casos de prueba para el sub-agente "revisor-dotnet" (día 7) o para la integración de API (día 15), y correrlos para ver consistencia de resultados.
- **Fecha real:** _—_

### Día 17 — Capstone: todo integrado
- [ ] **Objetivo:** Cerrar el plan con un ejercicio que combine varios temas.
- **Contenido:** Repaso general, identificar qué reforzar.
- **Ejercicio:** En `practice-project/`, resolver una tarea de punta a punta usando: tu `CLAUDE.md`, tu skill personalizada, tu sub-agente revisor (con el modelo adecuado), tu servidor MCP y su hardening de seguridad — documentando el proceso en `docs/capstone.md`.
- **Fecha real:** _—_

---

## 📚 Temas cubiertos (resumen)

| Tema | Bloque |
|---|---|
| Arquitectura y ciclo agentic de Claude Code | 1 |
| `CLAUDE.md`, configuración, memoria | 1 |
| Slash Commands y Skills | 1 |
| Agentes (concepto general) | 2 |
| Sub-agentes, orquestación y model tiering | 2 |
| Selección de modelo, costos, extended thinking | 2 |
| MCP (consumo y construcción) | 2-3 |
| Seguridad y guardrails para agentes | 3 |
| Hooks | 3 |
| Plugins | 3 |
| Claude API / Agent SDK, tool use | 4 |
| Evaluación de agentes | 4 |

## 🛠️ Proyecto de práctica

Crearemos `practice-project/`: una solución .NET simple (ej. API mínima + dominio pequeño) que sirva de "campo de pruebas" para todos los ejercicios. Se crea al aprobar este plan.

## 📝 Cómo llevar el progreso

- Progreso detallado sesión a sesión: ver [progreso.md](progreso.md).
- Al final de cada sesión dime "actualiza el progreso" (o hazlo tú marcando las casillas) y yo actualizo ambos archivos y te doy un resumen breve.
- Si pasan varios días, solo pídeme "resumen del plan de estudio" y leo `progreso.md` para ponerte al día.

---

## ✅ Aprobación

- [x] He revisado el plan y lo apruebo tal cual. *(2026-08-07)*
- [ ] He revisado el plan y pido ajustes (ver notas abajo).

**Notas / ajustes pedidos:**
_(vacío — plan aprobado sin cambios adicionales)_
