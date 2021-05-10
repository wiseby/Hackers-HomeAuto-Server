[![Build status](https://github.com/wiseby/Hackers-HomeAuto.Server/actions/workflows/dotnet.yml/badge.svg)](https://github.com/wiseby/Hackers-HomeAuto.Server/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/wiseby/Hackers-HomeAuto.Server/LICENSE)

# Hackers-HomeAuto-Server

## What's this?

The Hackers-HomeAuto is in short a simple home-automation system capable of monitoring and controlling the home environment.

I'm also trying to make this project as a base template, implementing best practices and concepts that is relevant in the early stages when learning to write software. It should be easy to contribute and expand, tailored to fit any home-automation need.

## HowTo

Install [.Net SDK 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) or later

Install [Docker Engine for Ubuntu/Debian](https://docs.docker.com/engine/install/debian/), [Docker for Windows](https://docs.docker.com/docker-for-windows/install/) or [Docker for Mac](https://docs.docker.com/docker-for-mac/install/).
This is to run the MongoDB in docker container. The hole environment should have a dev-environment in docker in the future.

To start the MongoDB container, while standing in the root directory (where the docker-compose.yml is located) run the following command:

```bash
docker-compose up -d
```

_For linux users maybe sudo is required_

_More details can be found in the docker-compose.yml file_

Clone the repo:

```bash
git clone https://github.com/wiseby/Hackers-HomeAuto.Server.git
```

Start WebApi:

```bash
dotnet run --project src/WebApi/
```

Start MqttServer:

```bash
dotnet run --project src/MqttServer/
```

The MQTT Server uses [MQTTNet](https://github.com/chkr1011/MQTTnet/blob/master/README.md).

Accessing WebApp at WebApi root on https://localhost:5001/

## Development Workflow

Install [VS Code IDE](https://code.visualstudio.com/) (either stable or insiders) along with these extensions:

- [C# for Visual Studio Code (powered by OmniSharp).](https://code.visualstudio.com/docs/languages/csharp)
- [Prettier - Code formatter](https://marketplace.visualstudio.com/items?itemName=esbenp.prettier-vscode)

To test and debug the application you need to install [.Net SDK 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) or later

The client application is a Angular project in /src/WebApi/ClientApp. Further instructions can be located as a README in that folder.

## Contribute

Fork and build your custom tailored system by using this repo as a template to get started!

You think there is something missing? For more generic improvments/features it is encouraged to contribute according to the guidelines that should be written soon ;) Check the [Project](https://github.com/wiseby/Hackers-HomeAuto.Server/projects) section for work in progress or start a new issue if it's missing.
