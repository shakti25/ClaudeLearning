---
name: gen-test-xunit
description: Genera un test xUnit nuevo para una clase del dominio de practice-project, siguiendo el patrón de nombres y estructura ya usado en tests/Domain.Tests (Arrange-Act-Assert, nombre "Metodo_Comportamiento_Cuando"). Usar cuando el usuario pide crear, agregar o generar un test para algo del dominio.
allowed-tools:
  - Read
  - Glob
  - Grep
  - Edit
  - Write
  - Bash(dotnet test:*)
  - Bash(dotnet build:*)
---

# Generar test xUnit siguiendo el patrón del proyecto

## Cuándo se usa

El usuario pide crear/agregar un test para una clase o método del dominio de
`practice-project` (ej. "agregá un test para el método X", "necesito cubrir Y con un test").

## Pasos

1. **Confirmar el patrón vigente.** Antes de escribir nada, leer 1-2 tests existentes en
   `tests/Domain.Tests/` (no asumir el patrón de memoria — puede haber cambiado desde que
   se escribió esta skill). Prestar atención a:
   - Convención de nombres: `Metodo_Comportamiento_Cuando` (ej. `Cancel_MarksTaskAsCancelled_AndRemovesFromPending`).
   - Estructura: Arrange-Act-Assert, sin comentarios que separen las secciones.
   - Un archivo de tests por clase bajo prueba (`<Clase>Tests.cs`).

2. **Identificar el método/comportamiento a testear.** Si el usuario no fue específico sobre
   qué casos cubrir, proponer al menos: caso feliz, caso de error/borde evidente (ej. entidad
   no encontrada), y confirmar antes de generar varios tests a la vez si la lista es larga.

3. **Ubicar o crear el archivo de test correspondiente.**
   - Si ya existe `tests/Domain.Tests/<Clase>Tests.cs`, agregar el `[Fact]` ahí, respetando el
     orden y estilo de los métodos existentes.
   - Si no existe, crear el archivo siguiendo el mismo `namespace`/`using` que los demás
     archivos de `tests/Domain.Tests/`.

4. **Verificar.** Correr `dotnet test tests/Domain.Tests/Domain.Tests.csproj` y confirmar que
   compila y el test pasa (o falla por la razón esperada, si se está testeando un bug conocido
   aún no arreglado — en ese caso decirlo explícitamente, no asumir que "pasa" es siempre el
   resultado correcto).

## Qué NO hacer

- No inventar una convención de nombres distinta a la ya usada, aunque parezca "mejor".
- No generar tests para código que no existe todavía — si el método/clase pedido no existe,
  avisar en vez de inventarlo.
- No mezclar la generación del test con cambios al código de producción en el mismo paso,
  salvo que el usuario lo haya pedido explícitamente.
