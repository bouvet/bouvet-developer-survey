@description('The name of the Azure Container Registry.')
param acrName string = 'bouvetSurveyContainerRegistry'

@description('Name of managed identity resource')
param managedIdentityName string = 'bds-test-managedidentity'

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

@description('The name of the VNet')
param vnetName string = 'bds-test-vnet'

@description('The name of the subnet')
param subnetName string = 'bds-test-subnet'

@description('The name of the private endpoint')
param sqlPrivateEndpointName string = 'bds-test-privateendpoint-sqlserver'

@description('The name of the private endpoint connection')
param sqlServerConnectionName string = 'bds-test-sqlserver-connection'

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
    managedIdentityName: managedIdentityName
    resourceName: keyVaultName
    location: location
  }
}

module sqlServer 'modules/sqlServer.bicep' = {
  name: serverName
  params: {
    vnetName: vnetName
    subnetName: subnetName
    sqlPrivateEndpointName: sqlPrivateEndpointName
    sqlServerConnectionName: sqlServerConnectionName
    serverName: serverName
    sqlDBName: sqlDBName
    location: location
    administratorLogin: sqlServerUsername
    administratorLoginPassword: sqlServerPassword
  }
}
