@description('Key Vault name')
param resourceName string

@description('Name of managed identity resource')
param managedIdentityName string

@description('The location for the Key Vault.')
param location string

@description('The SKU name of the Key Vault.')
param skuName string = 'standard'

// Use User Assigned Managed Identity instead of System Assigned.
// https://github.com/Azure/bicep/discussions/12056
resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' = {
  name: managedIdentityName
  location: location
}

resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' = {
  name: resourceName
  location: location
  properties: {
    enableSoftDelete: true
    softDeleteRetentionInDays: 90
    enableRbacAuthorization: true
    enabledForTemplateDeployment: true
    tenantId: subscription().tenantId
    sku: {
      family: 'A'
      name: skuName
    }
    accessPolicies: []
  }
  dependsOn: [managedIdentity]
}

@description('Assign the Key Vault Secret User role to the managed identity')
resource keyVaultSecretUserRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  scope: keyVault
  name: guid(resourceGroup().id, managedIdentity.id, '4633458b-17de-408a-b874-0445c86b69e6')
  properties: {
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', '4633458b-17de-408a-b874-0445c86b69e6')
    principalId: managedIdentity.properties.principalId
  }
}

output keyVaultId string = keyVault.id
