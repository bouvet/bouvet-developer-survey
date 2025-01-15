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

__Running the containers__

On the root of the project, you will see a `docker-compose.yaml` file.
This contains the services we want to run as docker containers.

```bash
# Build and start containers in the background
docker compose up -d

# Stop the containers
docker compose down
```
__Import data__

We need to import data into the database.

Go to `http://localhost:5001/index.html`. This is the GUI for the
Open API specification of the backend API.

- Click on `/api/v1/Imports/ImportSurvey`.
- Press "Try it out"
- Input the year 2024
- Input a Qualtics file (.qsf). Ask a fellow developer if you don't have it.
- Press "Execute".

If this request gave a 200, the survey data should be imported into,
the database. Now we need to import user responses.

- Click on `/api/v1/Imports/Results`.
- Press "Try it out"
- Input a CSV file. Ask a fellow developer if you don't have it.
- Press "Execute".

Go to `http://localhost:3000`.
