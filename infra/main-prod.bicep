@description('The name of the Azure Container Registry.')
param acrName string = 'bouvetSurveyContainerRegistryProd'

@description('Name of managed identity resource')
param managedIdentityName string = 'bds-prod-managedidentity'

@description('Key Vault module name')
param keyVaultName string = 'bds-prod-keyvault'

@description('The location for the registry.')
param location string = 'norwayeast'

@description('The name of the SQL Server.')
param serverName string = 'bds-prod-sqlserver'

@description('The name of the SQL Database.')
param sqlDBName string = 'bds-prod-sqldb'

@description('The username for the SQL Server.')
param sqlServerUsername string = 'bdsadmin'

@description('The name of the VNet')
param vnetName string = 'bds-prod-vnet'

@description('The name of the subnet')
param subnetName string = 'bds-prod-subnet'

@description('The name of the private endpoint')
param sqlPrivateEndpointName string = 'bds-prod-privateendpoint-sqlserver'

@description('The name of the private endpoint connection')
param sqlServerConnectionName string = 'bds-prod-sqlserver-connection'

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
