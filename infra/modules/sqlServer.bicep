@description('The name of the SQL Server.')
param serverName string

@description('The name of the SQL Database.')
param sqlDBName string

@description('The location for the SQL Server.')
param location string

@description('The name of the VNet')
param vnetName string

@description('The name of the subnet')
param subnetName string

@description('The name of the private endpoint')
param sqlPrivateEndpointName string

@description('The name of the private endpoint connection')
param sqlServerConnectionName string

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
    name: 'GP_S_Gen5_1'
    tier: 'GeneralPurpose'
    capacity: 1
    family: 'Gen5'
  }
  properties: {
    autoPauseDelay: 60
    minCapacity: 1
  }
}

resource vnet 'Microsoft.Network/virtualNetworks@2023-04-01' = {
  name: vnetName
  location: location
  properties: {
    addressSpace: {
      addressPrefixes: [
        '10.0.0.0/23'
      ]
    }
    subnets: [
      {
        name: subnetName
        properties: {
          addressPrefix: '10.0.0.0/23'
        }
      }
    ]
  }
}

resource sqlPrivateEndpoint 'Microsoft.Network/privateEndpoints@2024-05-01' = {
  name: sqlPrivateEndpointName
  location: location
  properties: {
    subnet: {
      id: vnet.properties.subnets[0].id
    }
    privateLinkServiceConnections: [
      {
        name: sqlServerConnectionName
        properties: {
          privateLinkServiceConnectionState: {
            status: 'Approved'
            description: 'Private endpoint for SQL server'
          }
          privateLinkServiceId: sqlServer.id
          groupIds: ['sqlServer']
        }
      }
    ]
  }
}
