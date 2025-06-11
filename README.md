# ELifeRPG Core

The core project is responsible for persistence, configuration and administration of the game modification. 

[![Releases](https://img.shields.io/github/v/release/ELifeRPG/Core)](https://github.com/ELifeRPG/Core/releases)
[![MIT License](https://img.shields.io/apm/l/atomic-design-ui.svg?)](https://github.com/ELifeRPG/Core/blob/main/LICENSE)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/98f780fd051a443680a3a87ca6af2967)](https://www.codacy.com/gh/ELifeRPG/Core/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=ELifeRPG/Core&amp;utm_campaign=Badge_Grade)
[![Codacy Badge](https://app.codacy.com/project/badge/Coverage/98f780fd051a443680a3a87ca6af2967)](https://www.codacy.com/gh/ELifeRPG/Core/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=ELifeRPG/Core&amp;utm_campaign=Badge_Coverage)


## Features
- Components:
  - Rest-API for the modification
  - Web-UI for several target-audiences like Players, Moderators, Administrators
- Accounts/Players, Characters, Companies, Economy configuration
- Auditing of important state changes


## Getting started

### Setup

Please refer to the [server setup guide](https://github.com/ELifeRPG/ELifeRPG/blob/main/docs/server-setup.md) for installing ELifeRPG to your server.

### Prepare environment

#### Dependencies

A list of dependencies which can be started by using `(docker compose)|(podman-compose) up -d`
- PostgreSQL on port 5432
  - additionally Adminer on port [8080](http://localhost:8080/)

#### Local secrets

You need to configure your local secrets which are not meant to be shared among developers using `dotnet user-secrets`.
Since the projects do use the same user-secrets-id, we can do it only for the most common project, which is the Migrator:
```sh
# required for Web-UI: discord application secret
 dotnet user-secrets --project src/Migrator set "OIDC:Discord:ClientSecret" "foo"
# optional: override connection-string to postgresql
dotnet user-secrets --project src/Migrator set "ConnectionStrings:Database" "Host=localhost;Database=foo;Username=bar;Password=baz"
```

### Development hints

#### EF Migrations

To use dotnet-ef tool, you need to specify the project and startup project like this:
```sh
dotn <command>
```
