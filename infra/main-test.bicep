@description('The name of the Azure Container Registry.')
param acrName string = 'bouvetSurveyContainerRegistry'

@description('Key Vault module name')
param keyVaultName string = 'bds-test-keyvault'

@description('The location for the registry.')
param location string = 'norwayeast'

@description('The name of the SQL Server.')
param serverName string = 'bds-test-sqlserver'

@description('The name of the SQL Database.')
param sqlDBName string = 'bds-test-sqldb'

@description('The username for the SQL Server.')
param sqlServerUsername string = 'bdsadmin'

@secure()
@description('The administrator password used for the sql server instance created.')
param sqlServerPassword string

// @description('The name of the app insights.')
// param appInsightsName string = 'bds-test-appinsights'

// @description('The name of the ACR login server.')
// param acrLoginServer string

// @description('The image tag for the container.')
// param imageTag string

// @description('OpenAi location for the AI Services.')
// param openAiLocation string = 'swedencentral'

// @description('The deployment name of the AI Services.')
// param aiServiceName string = 'bds-est-openai'

// @description('The deployment name of the AI Services.')
// param deploymentName string = 'gpt-4o-mini'

// TODO: documentation.
// The resources are split up infor multiple files.
// container-

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

// module appInsights 'modules/appInsights.bicep' = {
//   name: appInsightsName
//   params: {
//     appInsightsName: appInsightsName
//     location: location
//   }
// }

// module openAi 'modules/openAiService.bicep' = {
//   name: aiServiceName
//   params: {
//     aiServicesName: aiServiceName
//     location: openAiLocation
//     deploymentName: deploymentName
//   }
// }
