

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

@description('The target port for the container app.')
param targetPort int

resource containerApp 'Microsoft.App/containerApps@2023-08-01-preview' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: containerAppEnvironmentId
    configuration: {
      activeRevisionsMode: 'Multiple'
      ingress: {
        external: true
        targetPort: targetPort
        allowInsecure: false
        // customDomains: [
          // {
             // name: 'bds-api-test.com'
            //  certificateId: '/subscriptions/{subscription-id}/resourceGroups/{resource-group}/providers/Microsoft.Web/certificates/{certificate-name}'
          // }
        // ]
      }
      secrets: [
        {
          name: 'container-registry-password'
          value: acrPassword
        }
      ]
      registries: [
        {
          server: acrLoginServer
          passwordSecretRef: 'container-registry-password'
          username: acrUsername
        }
      ]
    }
    template: {
      containers: [
        {
          name: containerAppName
          image: '${acrLoginServer}/backend-image:${containerImage}'
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
