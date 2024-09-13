# Project Documentation

- [Architecture](#architecture)
- [Workflow](#workflow)
- [Daily git workflow for trunk based development](#gitworkflow)
- [Work methology](#work-methology)
- [Pipeline](#pipeline)
- [Release strategy](#release-strategy)
- [Tools](#tools)

## Architecture

**Suggestion for high level description of project arhictecture**

The application consists of two main parts. A backend built with .NET and a frontend using Next.js. Both the backend and frontend are packaged as separate Docker containers. The Docker images are stored in Azure Container Registry and deployed to Azure Container Apps Service

**Frontend**

- The frontend application is build using Next.js with TailwindCSS.
- Uses ESLint for linting
- Deployed through Github Actions pipeline
- Runs in a Docker container on Azure Container Apps
- Purpose: Retreive and present survey results from backend api.

**Backend**

- The backend application is built on ASP.NET with Entity Framework Core
- Fetches survey results from Qualtrics API regularly, async.
- Uses an Azure SQL Database for storing survey results
- Uses Azure AI for analysis on survey results
- Provides a REST API which is exposed for frontend application
- Runs in a Docker container on Azure Container Apps

**Workflow**

- Describe our workflow

**Daily Git workflow for trunk based development**

Suggestion of daily git work flow. Please comment :)

**Create and work on feature branch**

1. Chekout main branch:
`git checkout main`

2. Pull latest changes:
`git pull origin main`

3. Create short lived feature branch:
`git checkout -b feature/my-feature`

5. Work on your feature and test it locally. Once ready, stage and commit your changes:
`git add .`
`git commit -m "Add my feature"`

**Push your work to main and create PR**

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

**Containerization**

- Describe Docker containerization

**CI/CD**

- Describe pipeline

**Hosting**

- Describe Azure hosting

**IAC**

- Describe IAC


