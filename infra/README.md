# Bouvet Developer Survey Infrastructure

* --

## Github Workflow:

The GitHub workflows are located in the `.github/workflows` folder at the root of the repository. It consists of 3 `.yml` files, each handling its own tasks:

- **Pull-Request.yml** has the task of building and testing the project before a pull request can be accepted.
- **Deploy-azure-resources.yml** handles deploying infrastructure files to the Azure Resource Group, starting with the QA test environment, and then the Production environment if approved by the repository owners.
- **Azure-deploy-api.yml** is responsible for pushing images and deploying the latest image to an Azure Container App. For the QA step, this happens automatically, but updating the production image requires approval from an admin, along with attaching a tag to it.


## Modules
bicep files...
