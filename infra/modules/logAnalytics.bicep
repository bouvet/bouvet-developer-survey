@description('Log analytics workspace name')
param logAnalyticsWorkspaceName string

@description('Location for the Log Analytics workspace')
param location string

resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: logAnalyticsWorkspaceName
  location: location
  properties: any({
    retentionInDays: 30
    features: {
      searchVersion: 1
    }
    sku: {
      name: 'PerGB2018'
    }
  })
}

output logAnalyticsWorkspaceId string = logAnalytics.properties.customerId
output clientSecret string = logAnalytics.listKeys().primarySharedKey
