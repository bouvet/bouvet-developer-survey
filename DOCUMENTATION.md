# Project Documentation

- [Architecture](###architecture)
- [Workflow](#workflow)
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

**Containerization**
Describe Docker containerization

**CI/CD**
Describe pipeline

**Hosting**
Describe Azure hosting

**IAC**
Describe IAC


