@description('The name of the container app.')
param containerName string = 'envbds-test-container'

@description('The name of the container app.')
param containerAppName string = 'bds-test-containerapp'

@description('The name of the Log Analytics workspace.')
param logAnalyticsWorkspaceName string = 'bds-test-loganalytics'

@description('The location for the registry.')
param location string = 'norwayeast'

@description('The name of the ACR login server.')
param acrLoginServer string

@description('The image tag for the container.')
param imageTag string

module containerApps 'modules/containerApp.bicep' = {
  name: containerName
  params: {
    location: location
    appSuffix: containerName
    containerAppName: containerAppName
    logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
    acrLoginServer: acrLoginServer
    imageTag: imageTag             
  }
}
