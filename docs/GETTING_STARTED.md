# Getting Started

__Clone repo__
```bash
# clone repo
git clone git@github.com:bouvet/bouvet-developer-survey.git

cd bouvet-developer-survey
```

## Docker setup

Get the project up and running on your own machine using Docker and docker-compose.

__Installation Requirements:__

- Install a container runtime like Docker runtime, Colima, Podman or similar container runtime.

> If you are not familiar with container runtimes, I would recommend installing [Docker Desktop](https://docs.docker.com/get-started/get-docker/) .</br>
> Docker Desktop includes everything you need (GUI, Docker runtime and docker-compose).</br>
> __Rememeber__: In order to use Docker Desktop, you need to apply for a license with service desk,
> [bds.bouvet.no](bds.bouvet.no)

__Create .env file__
```bash
touch .env
```
Open your favorite editor and add the secrets.

```bash
DB_PASSWORD=<your-password>
DB_CONNECTION_STRING=Server=bds-db,1433;Database=bds-db;User ID=sa;Password=<your-password>;TrustServerCertificate=True;
OpenAiUrl = <url-from-azure>
OpenAiSecretKey = <secret-key-from-azure>
```
If you have the package install, you can create a password
from the command line using the command `openssl rand --base64 16`

The secrets for the OpenAI test service can be found at bds-test-openai -> Resource Management -> Keys and Endpoint.

### Option 1: Running the containers with Colima

Install colima with your preferred package manager.

```bash
# Install colima, docker and docker-compose
# In this case we are on macOS using Homebrew
brew install colima docker docker-compose

# Start colima with 4gb ram, 4cpus and 100GB disk (default)
colima start --cpu 4 --memory 4
```

Then start the containers

```bash
# Run docker-compose
docker-compose up -d
# If caching issues, run this instead to force a build
docker-compose up --build -d
```



Install lazydocker TUI to administrate the running containers. This is very useful to start, stop and debug containers.

```bash
brew install lazydocker

# start lazydocker in the terminal with the command
lazydocker
```

### Option 2: Running the containers with Docker Desktop

On the root of the project, you will see a `docker-compose.yaml` file.
This contains the services we want to run as docker containers.

```bash
# Build and start containers in the background
docker compose up -d
```

## Running (mostly) without docker

Follow this guide to run the backend and frontend using IDE/CLI. In this guide, the database will still be run using docker for convinience. To run the database using docker, first complete the docker setup.

Instead of using the .env file for local secrets, the secrets.json file will be used instead. This file is not directly a part of the project and is located on another part of your machine.

First create the file. A refrence to the file will be added to your .csproj file.

```bash
cd Bouvet.Developer.Survey.Backend/Bouvet.Developer.Survey.Api
dotnet user-secrets init
```

Then add the secrets

```bash
dotnet user-secrets set "OpenAiUrl" "<same value as in the .env file>"
dotnet user-secrets set "OpenAiSecretKey" "<same value as in the .env file>"
dotnet user-secrets set "ConnectionString" "Server=localhost,1433;Database=bds-db;User ID=sa;Password=<your-password-from-the-.env-file>;TrustServerCertificate=True;"
```

The Microsoft docs if anything is not working as stated: https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows

To start only the database using docker-compose:

```bash
docker-compose up bds-db -d
```

Then run the backend and frontend using the CLI or IDE as you normally would.

## Import data

Import survey and survey responses into the database.

> Where do I find the files? Slack -> bds-team

Go to `http://localhost:5001/index.html`. This is the GUI for the
Open API specification of the backend API.

- Click on `/api/v1/Imports/ImportSurvey`.
- Press "Try it out"
- Input the year 2024
- Input a Qualtics file (.qsf). Ask a fellow developer if you don't have it.
- Press "Execute".

If this request gave a 200, the survey data should be imported into,
the database. Now we need to import user responses.

- Click on `/api/v1/Resulst/Survey`.
- Press "Try it out"
- Press "Execute".
- Copy the id (GUID) from the response.

- Click on `/api/v1/Imports/Results`.
- Paste the id from the previous step into field.
- Press "Try it out"
- Input a CSV file. Ask a fellow developer if you don't have it.
- Press "Execute".

Go to `http://localhost:3000`.

To stop the containers type `docker-compose down`.

If you happen to delete the volumes mounted in the sql container,
the data will be lost and you will have to do another import.

## Known issues
When stopping, and then starting the database using docker-compose, the database throws an error. To fix this, stop the containers and the delete volumes:

```bash
docker-compose down -v
```

And then delete any volumes left manually. In lazydocker this can be done be marking the volume and pressing `d` and then `enter`.

Doing this requires a new import of data into the database.