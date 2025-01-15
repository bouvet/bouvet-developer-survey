@description('The location to deploy resource');
param location string

@description('The name of the Container App Environment')
param containerAppName string

@description('Key Vault module name')
param keyVaultName string = 'bds-test-keyvault'

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
resource managedId 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' existing = {
  name: 'bds-test-managed-identity'
}

@description('This is the built-in Key Vault Secret User role. See https://docs.microsoft.com/azure/role-based-access-control/built-in-roles')
resource keyVaultSecretUserRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-04-01' existing = {
  scope: resourceGroup()
  name: '4633458b-17de-408a-b874-0445c86b69e6'
}

resource keyVaultSecretUserRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  scope: keyVault
  name: guid(resourceGroup().id, managedId.id, keyVaultSecretUserRoleDefinition.id)
  properties: {
    roleDefinitionId: keyVaultSecretUserRoleDefinition.id
    principalId: managedId.properties.principalId
  }
}

resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyVaultName
}

// Bicep does not support conditional creating. So secrets are created manually.
// This only ensures that this template will fail if this secret does not exist
resource connectionStringSecret 'Microsoft.KeyVault/vaults/secrets@2023-07-01' existing = {
  parent: keyVault
  name: 'sql-server-connection-string'
}

resource containerApp 'Microsoft.App/containerApps@2023-08-01-preview' = {
  name: containerAppName
  location: location
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${managedId.id}': {}
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
          name: 'sql-server-connection-string'
          keyVaultUrl: 'https://${keyVaultName}.vault.azure.net/secrets/sql-server-connection-string'
          identity: managedId.id
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
              name: 'CONNECTION_STRING'
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
