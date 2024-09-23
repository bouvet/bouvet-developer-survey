@description('Log analytics workspace name')
param logAnalyticsWorkspaceName string

@description('Location for the Log Analytics workspace')
param location string

resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: logAnalyticsWorkspaceName
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
  }
}

