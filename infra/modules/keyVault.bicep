@description('Key Vault name')
param resourceName string

@description('The location for the Key Vault.')
param location string

@description('The SKU name of the Key Vault.')
param skuName string = 'standard'

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
}

output keyVaultId string = keyVault.id
