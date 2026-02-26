# Despliegue ClickOnce (La Casita) en lacasitadelpan.com/casita

Guía para servir la aplicación La Casita vía ClickOnce en el VPS de Hetzner (lacasitadelpan.com), conviviendo con Apache/Tomcat.

## Arquitectura

- **Nginx** escucha en 80/443 y actúa como única entrada.
- La ruta **/casita** se sirve como archivos estáticos (salida de ClickOnce).
- El resto del tráfico se envía por proxy a Apache/Tomcat.

```
Cliente → Nginx (80/443) → /casita  → /var/www/casita (estáticos)
                        → resto   → Apache/Tomcat (ej. 127.0.0.1:8080)
```

## Requisitos

- Nginx instalado en el VPS (Debian/Ubuntu: `apt install nginx`).
- Apache/Tomcat escuchando en un puerto local (ej. 8080) para no chocar con 80/443.

## 1. Carpeta para ClickOnce

Crear la carpeta donde se subirán los archivos publicados:

```bash
sudo mkdir -p /var/www/casita
sudo chown www-data:www-data /var/www/casita
```

Tras cada publicación desde Visual Studio, subir el contenido de la carpeta de publicación (p. ej. `C:\products\casita\assembly\86\`) a `/var/www/casita` (`.application`, `.manifest`, `.deploy`, `index.html`, etc.).

## 2. Configuración Nginx para lacasitadelpan.com

Crear o editar el server block (ej. `/etc/nginx/sites-available/lacasitadelpan.com`):

```nginx
# Redirección HTTP → HTTPS (opcional)
server {
    listen 80;
    server_name lacasitadelpan.com;
    return 301 https://$server_name$request_uri;
}

server {
    listen 443 ssl http2;
    server_name lacasitadelpan.com;

    # Certificados SSL (Let's Encrypt con certbot)
    ssl_certificate     /etc/letsencrypt/live/lacasitadelpan.com/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/lacasitadelpan.com/privkey.pem;

    # Incluir tipos MIME por defecto; para ClickOnce añadir en /etc/nginx/mime.types:
    #   application/x-ms-application application;
    #   application/x-ms-manifest   manifest;
    include mime.types;

    # ClickOnce: archivos estáticos en /casita
    location /casita/ {
        alias /var/www/casita/;
    }

    # Resto del sitio → Apache/Tomcat (ajustar puerto según tu instalación)
    location / {
        proxy_pass http://127.0.0.1:8080;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

Habilitar el sitio y comprobar la configuración:

```bash
sudo ln -sf /etc/nginx/sites-available/lacasitadelpan.com /etc/nginx/sites-enabled/
sudo nginx -t && sudo systemctl reload nginx
```

## 3. SSL con Let's Encrypt

Si aún no hay certificado:

```bash
sudo apt install certbot python3-certbot-nginx
sudo certbot --nginx -d lacasitadelpan.com
```

Certbot configurará los paths de `ssl_certificate` y `ssl_certificate_key`; si usa un server block propio, ajustar el bloque anterior para que apunte a los mismos paths.

## 4. Apache/Tomcat en localhost

Para que Nginx sea el único que escuche en 80/443, Apache (o Tomcat) debe escuchar solo en localhost, por ejemplo en el puerto 8080. Ajustar la configuración de Apache/Tomcat según tu instalación (ej. en Tomcat: `server.xml` con `port="8080"` y asegurando que no haya un Connector en 80/443). El `proxy_pass` del bloque anterior debe apuntar al mismo puerto.

## 5. Comprobar

- **ClickOnce:** abrir `https://lacasitadelpan.com/casita/` y verificar que se muestra la página de instalación y que el enlace `.application` funciona.
- **Resto del sitio:** comprobar que el resto de rutas siguen yendo correctamente a la aplicación Java/Tomcat.

## Resumen de tareas por publicación

1. Publicar La Casita desde Visual Studio (Publish).
2. Subir el contenido de la carpeta de publicación a `/var/www/casita` en el VPS (rsync, SCP, SFTP, etc.).
