

@description('The location to deploy resource')
param location string

@description('The name of the Container App Environment')
param containerAppName string

@description('The ID of the Container App Environment')
param containerAppEnvironmentId string

@description('The name of the ACR login server.')
param acrLoginServer string

@description('The name of the ACR username.')
param acrUsername string

@secure()
@description('The name of the ACR password.')
param acrPassword string

@description('The name of the container image.')
param containerImage string

resource containerApp 'Microsoft.App/containerApps@2023-08-01-preview' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: containerAppEnvironmentId
    configuration: {
      activeRevisionsMode: 'Multiple'
      ingress: {
        external: true
        targetPort: 5001
        allowInsecure: false
      }
      secrets: [
        {
          name: 'container-registry-password'
          value: acrPassword
        }
      ]      
      registries: [
        {
          server: 'bouvetsurveycontainerregistry.azurecr.io'
          passwordSecretRef: 'container-registry-password'
          username: 'bouvetSurveyContainerRegistry'
        }
      ]
    }
    template: {
      containers: [
        {
          name: containerAppName
          image: 'bouvetsurveycontainerregistry.azurecr.io/backend-image:3bfb05120ccb6ae843a3b0c65132c174509805e6'
          resources: {
            cpu: json('1.0')
            memory: '2Gi'
          }
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 3
      }
    }
  }
}

output fqdn string = containerApp.properties.configuration.ingress.fqdn
