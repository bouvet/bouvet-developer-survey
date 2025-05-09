@description('The location to deploy resource')
param location string

@description('Name of managed identity resource')
param managedIdentityName string

@description('The name of the Container App Environment')
param containerAppName string

@description('Key Vault module name')
param keyVaultName string

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

// Use User Assigned Managed Identity instead of System Assigned.
// https://github.com/Azure/bicep/discussions/12056
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
    managedEnvironmentId: containerAppEnvironmentId
    configuration: {
      activeRevisionsMode: 'Multiple'
      ingress: {
        external: true
        targetPort: targetPort
        allowInsecure: false
      }
      registries: [
        {
          server: acrLoginServer
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
        {
          name: 'open-ai-url'
          keyVaultUrl: 'https://${keyVaultName}.vault.azure.net/secrets/OpenAiUrl'
          identity: managedIdentity.id
        }
        {
          name: 'open-ai-secret-key'
          keyVaultUrl: 'https://${keyVaultName}.vault.azure.net/secrets/OpenAiSecretKey'
          identity: managedIdentity.id
        }
        {
          name: 'AZURE-AD-CLIENT-SECRET'
          keyVaultUrl: 'https://${keyVaultName}.vault.azure.net/secrets/AZURE-AD-CLIENT-SECRET'
          identity: managedIdentity.id
        }
        {
          name: 'AZURE-AD-CLIENT-ID'
          keyVaultUrl: 'https://${keyVaultName}.vault.azure.net/secrets/AZURE-AD-CLIENT-ID'
          identity: managedIdentity.id
        }
        {
          name: 'AZURE-AD-TENANT-ID'
          keyVaultUrl: 'https://${keyVaultName}.vault.azure.net/secrets/AZURE-AD-TENANT-ID'
          identity: managedIdentity.id
        }
        {
          name: 'AZURE-AD-BACKEND-CLIENT-ID'
          keyVaultUrl: 'https://${keyVaultName}.vault.azure.net/secrets/AZURE-AD-BACKEND-CLIENT-ID'
          identity: managedIdentity.id
        }
        {
          name: 'NEXTAUTH-SECRET'
          keyVaultUrl: 'https://${keyVaultName}.vault.azure.net/secrets/NEXTAUTH-SECRET'
          identity: managedIdentity.id
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
              name: 'ConnectionString'
              secretRef: 'sql-server-connection-string'
            }
            {
              name: 'OpenAiUrl'
              secretRef: 'open-ai-url'
            }
            {
              name: 'OpenAiSecretKey'
              secretRef: 'open-ai-secret-key'
            }
            {
              name: 'AZURE-AD-CLIENT-SECRET'
              secretRef: 'AZURE-AD-CLIENT-SECRET'
            }
            {
              name: 'AZURE-AD-CLIENT-ID'
              secretRef: 'AZURE-AD-CLIENT-ID'
            }
            {
              name: 'AZURE-AD-TENANT-ID'
              secretRef: 'AZURE-AD-TENANT-ID'
            }
            {
              name: 'AZURE-AD-BACKEND-CLIENT-ID'
              secretRef: 'AZURE-AD-BACKEND-CLIENT-ID'
            }
            {
              name: 'NEXTAUTH-SECRET'
              secretRef: 'NEXTAUTH-SECRET'
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
