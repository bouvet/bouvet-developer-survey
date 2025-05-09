@description('The location for the registry.')
param location string = 'norwayeast'

@description('The name of the container app environment.')
param containerName string = 'bds-prod-containerenv-frontend'

@description('The name of the container app.')
param containerAppName string = 'bds-prod-containerapp-frontend'

@description('The name of the Log Analytics workspace.')
param logAnalyticsWorkspaceName string = 'bds-prod-loganalytics'

@description('The name of the ACR login server.')
param acrServer string

@description('The name of the ACR username.')
param acrUsername string

@description('The name of the VNet.')
param vnetName string = 'bds-prod-vnet'

@secure()
@description('The name of the ACR password.')
param acrPassword string

@description('The name of the container image.')
param containerImage string

module containerEnv 'modules/containerEnv.bicep' = {
  name: containerName
  params: {
    vnetName: vnetName
    containerAppEnvironmentName: containerName
    location: location
    logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
  }
}

resource containerApp 'Microsoft.App/containerApps@2023-08-01-preview' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: containerEnv.outputs.id
    configuration: {
      activeRevisionsMode: 'Multiple'
      ingress: {
        external: true
        targetPort: 3000
        allowInsecure: false
        customDomains: [
          {
            bindingType: 'SniEnabled'
            certificateId: 'survey.bouvetapps.io-bds-prod-250120105916'
            name: 'survey.bouvetapps.io'
          }
        ]
      }
      secrets: [
        {
          name: 'container-registry-password'
          value: acrPassword
        }
      ]
      registries: [
        {
          server: acrServer
          passwordSecretRef: 'container-registry-password'
          username: acrUsername
        }
      ]
    }
    template: {
      containers: [
        {
          name: containerAppName
          image: containerImage
          resources: {
            cpu: json('0.5')
            memory: '1Gi'
          }
        }
      ]
      scale: {
        minReplicas: 0
        maxReplicas: 2
      }
    }
  }
}

output fqdn string = containerApp.properties.configuration.ingress.fqdn
