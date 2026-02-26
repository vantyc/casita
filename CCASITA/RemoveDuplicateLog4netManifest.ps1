# Remove duplicate log4net 1.2.10 (Crystal) dependency from ClickOnce app manifest to fix RefDefValidation.
# Also updates LaCasita.application with the new hash/size of the modified exe.manifest.
param([Parameter(Mandatory=$true)][string]$AppFilesPath)

$versionDir = Get-ChildItem $AppFilesPath -Directory -ErrorAction SilentlyContinue | Where-Object { $_.Name -like 'LaCasita_*' } | Select-Object -First 1
if (-not $versionDir) { exit 0 }
$manifestPath = Join-Path $versionDir.FullName 'LaCasita.exe.manifest'
if (-not (Test-Path $manifestPath)) { exit 0 }
$content = [IO.File]::ReadAllText($manifestPath)
# Remove the <dependency> block that contains log4net version 1.2.10.0 (wrong identity from Crystal)
$pattern = '(?s)<dependency>\s*<dependentAssembly[^>]*codebase="log4net\.dll"[^>]*>.*?version="1\.2\.10\.0".*?</dependentAssembly>\s*</dependency>'
$newContent = $content -replace $pattern, ''
if ($newContent -ne $content) {
    [IO.File]::WriteAllText($manifestPath, $newContent)
    Write-Host "RemoveDuplicateLog4netManifest: removed duplicate log4net 1.2.10 entry from manifest."
}

# Update deployment manifest (.application) with new hash and size of exe.manifest so ClickOnce validation passes
$deployPath = Join-Path (Split-Path $AppFilesPath -Parent) 'LaCasita.application'
if (Test-Path $deployPath) {
    $bytes = [IO.File]::ReadAllBytes($manifestPath)
    $size = $bytes.Length
    $sha = [System.Security.Cryptography.SHA256]::Create()
    $hashB64 = [Convert]::ToBase64String($sha.ComputeHash($bytes))
    $appContent = [IO.File]::ReadAllText($deployPath)
    # Replace size and DigestValue for the dependency that references LaCasita.exe.manifest
    $appContent = $appContent -replace '(<dependentAssembly dependencyType="install" codebase="Application Files\\LaCasita_1_0_0_86\\LaCasita\.exe\.manifest" size=")\d+(">)', "`${1}$size`$2"
    $appContent = $appContent -replace '(<dsig:DigestValue>)[^<]+(</dsig:DigestValue>)', "`${1}$hashB64`$2"
    [IO.File]::WriteAllText($deployPath, $appContent)
    Write-Host "RemoveDuplicateLog4netManifest: updated LaCasita.application with new manifest hash (size=$size)."
}
