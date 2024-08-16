# GouGo AI - Landing - API

Con la finalidad de que pueda dar mantenimiento a la API de la landing se comparte a continuación guía técnica sobre el
desarrollo.

## Tecnología

Para el desarrollo se utilizó [.Net](https://dotnet.microsoft.com/en-us/).

## Requisitos

Para poder utilizar es necesario contar en la maquina de desarrollo con:

- .Net v8+.
- Editor de código como [VSCode](https://code.visualstudio.com/), configurado
  para [.Net](https://code.visualstudio.com/docs/languages/dotnet).

## Comandos importantes

A continuación se listan los comandos más importantes que se pueden ejecutar en la terminal dentro del folder
`./src/gougo-api`:

| Commando         | Acción                                                                                          |
|:-----------------|:------------------------------------------------------------------------------------------------|
| `dotnet run`     | Inicia un servidor local para desarrollo con hot-reload en `localhost:5074`.                    |
| `dotnet publish` | Compila la solución para publicar a producción en `./src/gougo-api/bin/Release/net8.0/publish/` |

## Notas

- El archivo de configuración se puede encontrar en `./src/gougo-api/appsettings.json`. Se recomienda colocar la
  configuración correcta de SMTP (`MailConfigOption`) para envío de emails.
