@description('The name of the Azure Container Registry.')
param acrName string = 'bouvetSurveyContainerRegistry'

@description('Key Vault module name')
param keyVaultName string = 'bds-prod-keyvault'

@description('The location for the registry.')
param location string = 'norwayeast'

@description('OpenAi location for the AI Services.')
param openAiLocation string = 'swedencentral'

@description('The name of the SQL Server.')
param serverName string = 'bds-prod-sqlserver'

@description('The name of the SQL Database.')
param sqlDBName string = 'bds-prod-sqldb'

@description('The username for the SQL Server.')
param sqlServerUsername string = 'bdsadmin'

@secure()
@description('The administrator password used for the sql server instance created.')
param sqlServerPassword string

@description('The deployment name of the AI Services.')
param aiServiceName string = 'bds-prod-ai'

@description('The deployment name of the AI Services.')
param deploymentName string = 'gpt-4o-mini'



module containerRegistry 'modules/containerRegistry.bicep' = {
  name: acrName
  params: {
    acrName: acrName
    location: location
  }
}

module keyVault 'modules/keyVault.bicep' = {
  name: keyVaultName
  params: {
    resourceName: keyVaultName
    location: location
  }
}

module sqlServer 'modules/sqlServer.bicep' = {
  name: serverName
  params: {
    serverName: serverName
    sqlDBName: sqlDBName
    location: location
    administratorLogin: sqlServerUsername
    administratorLoginPassword: sqlServerPassword
  }
}

// module openAi 'modules/openAiService.bicep' = {
//   name: aiServiceName
//   params: {
//     aiServicesName: aiServiceName
//     location: openAiLocation
//     deploymentName: deploymentName
//   }
// } 
