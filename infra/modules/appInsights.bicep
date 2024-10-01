@description('Log analytics workspace name')
param appInsightsName string

@description('Location for the Log Analytics workspace')
param location string

resource appInsights 'microsoft.insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}
