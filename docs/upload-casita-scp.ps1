# Subir publicacion ClickOnce a lacasitadelpan.com con SCP
# Ejecutar despues de publicar desde Visual Studio (Publish).
# La carpeta de publicacion por defecto es: C:\products\casita\assembly\86

$PublishDir = "C:\products\casita\assembly\86"
$RemoteUser = "dev"
$RemoteHost = "lacasitadelpan.com"
$RemoteTmp = "/tmp/casita-upload"

if (-not (Test-Path $PublishDir)) {
    Write-Error "No existe la carpeta de publicacion: $PublishDir. Publica primero desde Visual Studio."
    exit 1
}

$files = Get-ChildItem $PublishDir -Recurse -File
if ($files.Count -eq 0) {
    Write-Error "La carpeta esta vacia. Publica primero desde Visual Studio."
    exit 1
}

Write-Host "Subiendo contenido de $PublishDir a $RemoteUser@${RemoteHost}:$RemoteTmp ..."
scp -r "${PublishDir}\*" "${RemoteUser}@${RemoteHost}:${RemoteTmp}/"

if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host "Moviendo en el servidor a /var/www/casita (requiere sudo)..."
ssh "${RemoteUser}@${RemoteHost}" "echo 'M3xico70`$' | sudo -S rm -rf /var/www/casita/* ; echo 'M3xico70`$' | sudo -S mv ${RemoteTmp}/* /var/www/casita/ ; echo 'M3xico70`$' | sudo -S rmdir ${RemoteTmp} 2>/dev/null ; echo 'M3xico70`$' | sudo -S chown -R www-data:www-data /var/www/casita"
Write-Host "Listo. Comprueba: https://lacasitadelpan.com/casita/"
