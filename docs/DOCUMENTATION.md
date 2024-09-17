# Project Documentation

- [Application architecture](#application-architecture)
- [Project Workflow & Methology](#project-workflow-and-methology)
- [Daily git workflow for trunk based development](#daily-git-workflow-for-trunk-based-development)
- [Containerization](#containerization)
- [CI/CD](#ci/cd)
- [Azure hosting and services](#azure-hosting-and-services)
- [Authentication and security](#authentication-and-security)
- [IaC](#iac)
- [Release strategy](#release-strategy)
- [Tools](#tools)

## Application architecture

The application consists of two main parts. A backend built with .NET and a frontend using Next.js. Both the backend and frontend are packaged as separate Docker containers. The Docker images are stored in Azure Container Registry and deployed to Azure Container Apps Service

### Frontend

- The frontend application is build using Next.js with TailwindCSS.
- Uses a charts js library for visualizing survey results
- Uses ESLint for linting
- Deployed through Github Actions pipeline
- Runs in a Docker container on Azure Container Apps
- Purpose: Retreive and present survey results using charts etc, in a responsive layout

### Backend

- The backend application is built on ASP.NET with Entity Framework Core
- Fetches survey results from Qualtrics API regularly, async.
- Uses an Azure SQL Database for storing survey results
- Uses Azure AI for analysis on survey results
- Provides a REST API which is exposed for frontend application
- Deployed through Github Actions pipeline
- Runs in a Docker container on Azure Container Apps
- Purpose: Provide a platform for storing survey results, manipulating them with AI and serving to frontend through an API

## Project Workflow and Methology

- Scrum, 2 weeks sprints
- Sprint planning at the start of each sprint
- Sprint revew at the end of each sprint
- Demo on demand
- Retrospective on demand

## Daily workflow for trunk based development

### Quick rundown:
1.	Morning Sync: Pull the latest trunk, review the updates.
2.	Work on Task: Focus on a small task, implement a feature or fix a bug.
3.	Local Testing: Run tests locally to ensure functionality.
4.	Commit Changes: Make small commits with clear messages.
5.	Merge to Trunk: Sync with the latest trunk and resolve conflicts, if any.
6.	Pull Request/Code Review: Open a pull request and address feedback.
7.	Monitor CI/CD: Check for successful builds and fix any issues promptly.
8.	End the Day: Ensure no work-in-progress code affects the trunk.

Suggestion of daily git work flow. Please comment :)

### Create and work on feature branch

1. Chekout main branch:
`git checkout main`

2. Pull latest changes:
`git pull origin main`

3. Create short lived feature branch:
`git checkout -b feature/my-feature`

5. Work on your feature and test it locally. Once ready, stage and commit your changes:
`git add .`
`git commit -m "Add my feature"`

### Push your work to main and create PR

6. Before merging your branch back into main, pull the latest change to make sure your branch is up to date, and resolve any conflicts:

`# Switch to the main branch`
`git checkout main`

`# Pull the latest changes`
`git pull origin main`

`# Switch back to your feature branch`
`git checkout feature/my-feature`

`# Rebase the feature branch with the latest main branch (this keeps history linear)`
`git rebase main`


6. Push feature branch to remote
`git push -u origin feature/my-feature`

7. If there are any conflicts during the rebase, Git will pause, and you’ll need to resolve them manually. After resolving, continue the rebase:

`# Add the resolved files`
`git add .`

`# Continue the rebase`
`git rebase --continue`


8. Push your feature branch to the remote repo and create a Pull Request:
`git push origin feature/my-task-name`

9. Create pull request in Github:
https://github.com/bouvet/bouvet-developer-survey/pulls

10. End of day cleanup. Make sure your feature branch is merged to main via PR, or hidde behind a feature toggle if unfinished.

`git checkout main`
`git pull origin main`
`git branch -d feature/my-feature`
`git push origin --delete feature/my-feature`

## Containerization

- Docker is used for containerization. Image is built from Github Actions workflow and pushed to Azure container registry. Please reference Dockerfiles and workflow .yml file for details.

## CI/CD

- Describe pipeline

## Azure hosting and services

Please see Bicep templates at /infra for infrastructure documentation.

1. Azure Container Apps (ACA)
    - Used to host the containerized frontend and backend applications
    - Handles scaling, load balancing and managing the containers
    - Frontend and backend services are deployed as separate microservices, but operate as part of the same overall application
    - Ingress is used to expose container apps to each other, and www

2. Azure Container Registry (ACR)
    - ACR is used to store and manage the docker images for frontend and backend
    - Docker images are build and pushed to ACR with Github Actions
    - ACA pulls the images from ACR to run the application

3. Azure Key Vault (AKV)
    - Secrets like connection string, api keys and credtionals are stored in AKV.
    - Frontend and backend containers receive secrets from here during runtime

4. Azure SQL Database
    - Used for storage and querying of survey data.

5. Resource group
    - A dedicated resource group is created for this project

6. Subscriptiong
    - A dedicated subscription is created for this project

## Authentication and security

- Describe authentication and security

## IAC

- This project uses Bicep for IaC. Please find more information and templates under /infra

## Release strategy

- Describe our release strategy

## Tools

- ESLint
- Prettier