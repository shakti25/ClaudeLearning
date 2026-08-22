# Hook PostToolUse: corre tras cada Edit/Write.
# Recibe por stdin un JSON con info de la tool call (tool_name, tool_input, etc.).
# Si el archivo tocado es .cs, corre dotnet format sobre la solución.
#
# Nota: este script debe ser compatible con Windows PowerShell 5.1 (motor .NET Framework),
# no solo pwsh 7+ — el hook se invoca con `powershell`, no `pwsh`. Por eso el cálculo de
# ruta relativa es manual (Substring) en vez de [System.IO.Path]::GetRelativePath, que
# no existe en 5.1.

$ErrorActionPreference = "SilentlyContinue"
$logPath = "$PSScriptRoot\format-on-edit.log"
$repoRoot = "d:\Shishino\Development\09-Claude\ClaudeLearning\practice-project"

$stdinJson = [Console]::In.ReadToEnd()
$timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"

try {
    $data = $stdinJson | ConvertFrom-Json
    $filePath = $data.tool_input.file_path
} catch {
    $filePath = $null
}

if ($filePath -and $filePath -like "*.cs") {
    Add-Content -Path $logPath -Value "[$timestamp] Hook disparado para: $filePath -> corriendo dotnet format"

    # --include resuelve rutas relativas contra el cwd del proceso, no contra la solución,
    # así que primero nos posicionamos en repoRoot.
    Push-Location $repoRoot
    $relativePath = $filePath.Substring($repoRoot.Length).TrimStart('\', '/') -replace '\\', '/'
    dotnet format "PracticeProject.slnx" --include "$relativePath" 2>&1 | Out-Null
    Pop-Location

    Add-Content -Path $logPath -Value "[$timestamp] dotnet format completado (include: $relativePath)."
} else {
    Add-Content -Path $logPath -Value "[$timestamp] Hook disparado pero archivo no es .cs (o no se pudo leer file_path): $filePath"
}

exit 0
