@description('The name of the Azure Container Registry.')
param acrName string = 'bouvetSurveyContainerRegistryTest'

@description('Key Vault module name')
param keyVaultName string = 'bds-test-kv'

@description('The location for the registry.')
param location string = 'norwayeast'

@description('OpenAi location for the AI Services.')
param openAiLocation string = 'swedencentral'

@description('The name of the SQL Server.')
param serverName string = 'bds-test-sqlserver'

@description('The name of the SQL Database.')
param sqlDBName string = 'bds-test-sqldb'

@description('The username for the SQL Server.')
param sqlServerUsername string = 'bdsadmin'

@secure()
@description('The administrator password used for the sql server instance created.')
param sqlServerPassword string



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

module openAi 'modules/openAiService.bicep' = {
  name: 'bds-test-openai'
  params: {
    aiServicesName: 'bds-test-openai'
    location: openAiLocation
    deploymentName: 'gpt-4o-mini'
  }
} 
