@description('The location for the registry.')
param location string = 'norwayeast'

@description('The name of the container app.')
param containerName string = 'bds-test-container'

@description('The name of the container app.')
param containerAppName string = 'bds-test-containerapp'

@description('The name of the Log Analytics workspace.')
param logAnalyticsWorkspaceName string = 'bds-test-loganalytics'

@description('The name of the ACR login server.')
param acrServer string

@description('The name of the ACR username.')
param acrUsername string

@secure()
@description('The name of the ACR password.')
param acrPassword string

@description('The name of the container image.')
param containerImage string

module containerApps 'modules/containerApp.bicep' = {
  name: containerName
  params: {
    location: location
    appSuffix: containerName
    containerAppName: containerAppName
    logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
    acrLoginServer: acrServer
    acrUsername: acrUsername
    acrPassword: acrPassword
    containerImage: containerImage          
  }
}
