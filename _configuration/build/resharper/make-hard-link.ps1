$ErrorActionPreference = "Stop"
[console]::OutputEncoding = [Text.Encoding]::Utf8

Import-Module '.\_make-hard-link.psm1' -Force

$fileName = "Mimer.sln.DotSettings"
$root = $PSScriptRoot
$newFile = Join-Path $root "..\..\..\$fileName"
$existingFile = Join-Path $root $fileName

Set-HardLink $existingFile $newFile