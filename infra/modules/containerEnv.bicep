@description('The name of the Container App Environment')
param containerAppEnvironmentName string

@description('The location to deploy resource')
param location string

@description('The Log Analytics workspace ID')
param logAnalyticsClientId string

@description('The Log Analytics workspace shared key')
param logAnalyticsSharedKey string

resource env 'Microsoft.App/managedEnvironments@2023-08-01-preview' = {
  name: containerAppEnvironmentName
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalyticsClientId
        sharedKey: logAnalyticsSharedKey
      }
    }
  }
}

output id string = env.id
