@description('The name of the SQL Server.')
param serverName string

@description('The name of the SQL Database.')
param sqlDBName string

@description('The location for the SQL Server.')
param location string

@description('The administrator login for the SQL Server.')
param administratorLogin string

@secure()
@description('The administrator login password for the SQL Server.')
param administratorLoginPassword string

resource sqlServer 'Microsoft.Sql/servers@2022-05-01-preview' = {
  name: serverName
  location: location
  properties: {
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
  }
}

resource sqlDB 'Microsoft.Sql/servers/databases@2022-05-01-preview' = {
  parent: sqlServer
  name: sqlDBName
  location: location
  sku: {
    name: 'S0'
    tier: 'Standard'
  }
}
