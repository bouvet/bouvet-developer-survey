@description('Key Vault module name')
param resourceName string

@description('The location for the Key Vault.')
param location string

@description('The SKU name of the Key Vault.')
param skuName string = 'standard'

// Use User Assigned Managed Identity instead of System Assigned.
// https://github.com/Azure/bicep/discussions/12056
resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' = {
  name: 'bds-test-managed-identity'
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
    accessPolicies: [
      {
        tenantId: subscription().tenantId
        objectId: managedIdentity.properties.principalId
        permissions: {
          secrets: ['get', 'list']
        }
      }
    ]
  }
}

output keyVaultId string = keyVault.id
