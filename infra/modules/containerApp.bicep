@description('The location to deploy resource')
param location string

@description('The name of the Container App Environment')
param containerAppName string

@description('Key Vault module name')
param keyVaultName string = 'bds-test-kv'

@description('The ID of the Container App Environment')
param containerAppEnvironmentId string

@description('The name of the ACR login server.')
param acrLoginServer string

@description('The name of the ACR username.')
param acrUsername string

@description('The name of the ACR password.')
param acrPassword string

@description('The name of the container image.')
param containerImage string

@description('The target port for the container app.')
param targetPort int

resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyVaultName
}

resource containerApp 'Microsoft.App/containerApps@2023-08-01-preview' = {
  name: containerAppName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    managedEnvironmentId: containerAppEnvironmentId
    configuration: {
      activeRevisionsMode: 'Multiple'
      ingress: {
        external: true
        targetPort: targetPort
        allowInsecure: false
      }
      secrets: [
        {
          name: 'container-registry-password'
          value: acrPassword
        }
        {
          name: 'sql-server-connection-string-kv'
          keyVaultUrl: 'https://${keyVault.name}.vault.azure.net/secrets/ConnectionString'
          identity: 'system'
        }
      ]
      registries: [
        {
          //TODO: use managed identity instead
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
          env: [
            {
              name: 'kek'
              secretRef: 'keke'
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

var keyVaultSecretUserRoleId = subscriptionResourceId(
  'Microsoft.Authorization/roleDefinitions',
  '4633458b-17de-408a-b874-0445c86b69e6'
)

resource keyVaultSecretUserRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(containerApp.id, keyVaultSecretUserRoleId)
  scope: keyVault
  properties: {
    principalId: containerApp.identity.principalId
    roleDefinitionId: keyVaultSecretUserRoleId
    principalType: 'ServicePrincipal'
  }
}

output fqdn string = containerApp.properties.configuration.ingress.fqdn
