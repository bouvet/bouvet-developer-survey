# Bouvet Developer Survey Infrastructure

- [Summary](#summar)
- [Bicep](#bicep)
- [Github Workflow](#github-workflow)
- []()

## Summary

This project uses infrastructure as code (IaC), and CI/CD pipelines.
This allows for automationg and simplifying the process of managing infrastructure,
and deploying code.

We use Bicep for IAC and Github workflows for CI/CD pipeline.

__Resources currently in Azure__

- One subscription
- Two resource groups __bds-test__ and __bds__ (production).
- Managed Identity for autentication and authorization.
- Key Vault for managing secrets.
- SQL Server
- Container Environment
- Container Apps

## Bicep

The `/infra` folder contains all the Bicep infrastructure code.

## Github Workflow

The GitHub workflows are located in the `.github/workflows` folder at the root of the repository. It consists of 3 `.yml` files, each handling its own tasks:

- **Pull-Request.yml** has the task of building and testing the project before a pull request can be accepted.
- **Deploy-azure-resources.yml** handles deploying infrastructure files to the Azure Resource Group, starting with the QA test environment, and then the Production environment if approved by the repository owners.
- **Azure-deploy-api.yml** is responsible for pushing images and deploying the latest image to an Azure Container App. For the QA step, this happens automatically, but updating the production image requires approval from an admin, along with attaching a tag to it.

- sql server firewall rules [done]
- container apps does not have static ip [don't need]
- need a Vnet and private endpoints for this. [don't need]

- frontend container app test workflow now gets the FQDN from the backend. [done]
This means we don't have to configure it ourself.

- I need to either recreate the database or fix it in using
the query editor in Azure. Year column does not exist. [done]

- Send mail til Kristin om tvilling SIM-kort. [done]

- Deploy infra til prod

- Print ut ting som Hanna har sent (selve brettet). []
- Trenger vi en ny pipeline som deployer både dev og prod.
