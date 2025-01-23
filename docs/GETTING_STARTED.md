# Getting Started

Get the project up and running with
- docker
- docker-compose
- colima
- lazygit

<img src='./images-gettings-started/tldr.gif' />

</br>

```bash
# Commands
git clone git@github.com:bouvet/bouvet-developer-survey.git
cd bouvet-developer-survey
brew install colima docker docker-compose lazydocker
DB_PASSWORD=$(openssl rand --base64 16)
echo "DB_PASSWORD=$DB_PASSWORD" > .env
echo "DB_CONNECTION_STRING=Server=bds-db,1433;Database=bds-db;User ID=sa;Password=$DB_PASSWORD;TrustServerCertificate=True;" >> .env
colima start --memory 4 --cpu 4
docker-compose up -d
lazydocker
```

**Installation Requirements:**

- Install a container runtime like Docker runtime, Colima, Podman or similar container runtime.

> If you are not familiar with container runtimes, I would recommend installing [Docker Desktop](https://docs.docker.com/get-started/get-docker/) .</br>
> Docker Desktop includes everything you need (GUI, Docker runtime and docker-compose).</br> > **Rememeber**: In order to use Docker Desktop, you need to apply for a license with service desk,
> [bds.bouvet.no](bds.bouvet.no)

**Setting up the project**

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

**Running containers with Colima and docker-compose**

Install colima with your preferred package manager.

```bash
# Install colima, docker and docker-compose
# In this case we are on macOS using Homebrew
brew install colima docker docker-compose

# Start colima with 4gb ram, 4cpus and 100GB disk (default)
colima start --cpu 4 --memory 4

# Run docker-compose
docker-compose up -d
```

> Install lazydocker TUI to administrate the running containers.
> This is very useful to start, stop and debug containers.

```bash
brew install lazydocker

# start lazydocker in the terminal with the command
lazydocker
```

This will start the containers.

**Running the containers with Docker Desktop**

On the root of the project, you will see a `docker-compose.yaml` file.
This contains the services we want to run as docker containers.

```bash
# Build and start containers in the background
docker compose up -d

```

## Database import

Import survey and survey responses into the database.
You will find the import files in the Slack channel called `bds-team`.

In order to import, the applications have to run.
Go to `http://localhost:5001/index.html`. This is the GUI for the Open API specification of the backend API.

1. Click on `/api/v1/Imports/ImportSurvey`.
2. Press "Try it out".
3. Input the year 2024.
4. Input a Qualtrics file (.qsf). Ask a fellow developer if you don't have it.
5. Press "Execute".

If this request returns a `200`, the survey data should be imported into the database. Now we need to import user responses.

1. Click on `/api/v1/Results/Survey`.
2. Press "Try it out".
3. Press "Execute".
4. Copy the ID (GUID) from the response.

Last, but not least, we need to import the answers.

1. Click on `/api/v1/Imports/Results`.
2. Paste the ID from the previous step into the field.
3. Press "Try it out".
4. Input a CSV file. Ask a fellow developer if you don't have it.
5. Press "Execute".

Go to `http://localhost:3000`.

To stop the containers, type `docker-compose down`.

**Note**: If you happen to delete the volumes mounted in the SQL container, the data will be lost and you will have to do another import.
