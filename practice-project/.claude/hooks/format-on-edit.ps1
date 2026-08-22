# Hook PostToolUse: corre tras cada Edit/Write.
# Recibe por stdin un JSON con info de la tool call (tool_name, tool_input, etc.).
# Si el archivo tocado es .cs, corre dotnet format sobre la solución.

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
    dotnet format "$repoRoot\PracticeProject.slnx" --include "$filePath" 2>&1 | Out-Null
    Add-Content -Path $logPath -Value "[$timestamp] dotnet format completado."
} else {
    Add-Content -Path $logPath -Value "[$timestamp] Hook disparado pero archivo no es .cs (o no se pudo leer file_path): $filePath"
}

exit 0
