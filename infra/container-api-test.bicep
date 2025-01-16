@description('The location for the registry.')
param location string = 'norwayeast'

@description('Name of managed identity resource')
param managedIdentityName string = 'bds-test-managedidentity'

@description('The name of the container environment.')
param containerName string = 'bds-test-containerenv-api'

@description('The name of the container app.')
param containerAppName string = 'bds-test-containerapp-api'

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

module containerEnv 'modules/containerEnv.bicep' = {
  name: containerName
  params: {
    containerAppEnvironmentName: containerName
    location: location
    logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
  }
}

module containerApp 'modules/containerApp.bicep' = {
  name: containerAppName
  params: {
    location: location
    managedIdentityName: managedIdentityName
    containerAppName: containerAppName
    containerAppEnvironmentId: containerEnv.outputs.id
    acrLoginServer: acrServer
    acrUsername: acrUsername
    acrPassword: acrPassword
    containerImage: containerImage
    targetPort: 5001
  }
}

output fqdn string = containerApp.outputs.fqdn
