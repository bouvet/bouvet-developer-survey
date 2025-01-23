@description('The location for the registry.')
param location string = 'norwayeast'

@description('Name of managed identity resource')
param managedIdentityName string = 'bds-prod-managedidentity'

@description('Key Vault module name')
param keyVaultName string = 'bds-prod-keyvault'

@description('The name of the container environment.')
param containerEnvName string = 'bds-prod-containerenv-api'

@description('The name of the container app.')
param containerAppName string = 'bds-prod-containerapp-api'

@description('The name of the Log Analytics workspace.')
param logAnalyticsWorkspaceName string = 'bds-prod-loganalytics'

@description('The name of the VNet.')
param vnetName string = 'bds-prod-vnet'

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
  name: containerEnvName
  params: {
    vnetName: vnetName
    containerAppEnvironmentName: containerEnvName
    location: location
    logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
  }
}

resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' existing = {
  name: managedIdentityName
}

resource containerApp 'Microsoft.App/containerApps@2023-08-01-preview' = {
  name: containerAppName
  location: location
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${managedIdentity.id}': {}
    }
  }
  properties: {
    managedEnvironmentId: containerEnv.outputs.id
    configuration: {
      activeRevisionsMode: 'Multiple'
      ingress: {
        external: true
        targetPort: 5001
        allowInsecure: false
        customDomains: [
          {
            bindingType: 'SniEnabled'
            certificateId: 'surveyapi.bouvetapps.io-bds-prod-250120104816'
            name: 'surveyapi.bouvetapps.io'
          }
        ]
      }
      registries: [
        {
          server: acrServer
          passwordSecretRef: 'container-registry-password'
          username: acrUsername
        }
      ]
      secrets: [
        {
          name: 'container-registry-password'
          value: acrPassword
        }
        {
          name: 'sql-server-connection-string' // github workflow will fail if this does not exist.
          keyVaultUrl: 'https://${keyVaultName}.vault.azure.net/secrets/sql-server-connection-string'
          identity: managedIdentity.id
        }
      ]
    }
    template: {
      containers: [
        {
          name: containerAppName
          image: '${acrServer}/backend-image:${containerImage}'
          resources: {
            cpu: json('0.5')
            memory: '1Gi'
          }
          env: [
            {
              name: 'ConnectionString'
              secretRef: 'sql-server-connection-string'
            }
          ]
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
