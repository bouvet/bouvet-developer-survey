@description('Name of managed identity resource')
param resourceName string

@description('The location to deploy resource')
param location string

// Use User Assigned Managed Identity instead of System Assigned.
// https://github.com/Azure/bicep/discussions/12056
resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' = {
  name: resourceName
  location: location
}

@description('This is the built-in Key Vault Secret User role. See https://docs.microsoft.com/azure/role-based-access-control/built-in-roles')
resource keyVaultSecretUserRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-04-01' = {
  scope: resourceGroup()
  name: '4633458b-17de-408a-b874-0445c86b69e6'
}

resource keyVaultSecretUserRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  scope: resourceGroup()
  name: guid(resourceGroup().id, managedIdentity.id, keyVaultSecretUserRoleDefinition.id)
  properties: {
    roleDefinitionId: keyVaultSecretUserRoleDefinition.id
    principalId: managedIdentity.properties.principalId
  }
}
