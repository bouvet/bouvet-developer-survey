# Getting Started

Get the project up and running on your own machine using Docker and docker-compose.

__Installation Requirements:__

- Install a container runtime like Docker runtime, Colima, Podman or similar container runtime.

> If you are not familiar with container runtimes, I would recommend installing [Docker Desktop](https://docs.docker.com/get-started/get-docker/) .</br>
> Docker Desktop includes everything you need (GUI, Docker runtime and docker-compose).</br>
> __Rememeber__: In order to use Docker Desktop, you need to apply for a license with service desk,
> [bds.bouvet.no](bds.bouvet.no)

__Setting up the project__
```bash
# clone repo
git clone git@github.com:bouvet/bouvet-developer-survey.git

cd bouvet-developer-survey

touch .env
```
Open your favorite editor and add thes two secrets.

```bash
DB_PASSWORD=<your-password>
DB_CONNECTION_STRING=Server=bds-db,1433;Database=bds-db;User ID=sa;Password=<your-password>;TrustServerCertificate=True;
```
If you have the package install, you can create a password
from the command line using the command `openssl rand --base64 16`

__Running the containers with Colima__

Install colima with your preferred package manager.

```
# Install colima, docker and docker-compose
# In this case we are on macOS using Homebrew
brew install colima docker docker-compose

# Start colima with 4gb ram, 4cpus and 100GB disk (default)
colima start --cpu 4 --memory 4

# Run docker-compose
docker-compose up -d
```

> Install lazydocker TUI to inspect the running containers.
> This is useful to start and stop containers as well
> as viewing logs.

```
brew install lazydocker

# start lazydocker in the terminal with the command
lazydocker
```

This will start the containers.

TODO: Why does the import csv return 400? when it all goes through?

A usefull tool to ins

__Running the containers with Docker Desktop__

On the root of the project, you will see a `docker-compose.yaml` file.
This contains the services we want to run as docker containers.

```bash
# Build and start containers in the background
docker compose up -d

```
__Import data__

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

To stop the containers type `docker compose down`.

If you happen to delete the volumes mounted in the sql container,
the data will be lost and you will have to do another import.
