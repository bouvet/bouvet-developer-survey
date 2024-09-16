@description('Storage account name')
param storageAccountName string

@description('Storage account name')
param storageAccountNameCopy string

@allowed([
  'Standard_LRS'
  'Standard_GRS'
])
@description('Storage account sku')
param storageAccountSku string

@description('Storage account location')
param location string = resourceGroup().location

@description('is Hierarchical namespace enabled')
param isHnsEnabled bool = true

@description('is https traffic only enabled')
param supportsHttpsTrafficOnly bool = true

module storageAccount 'modules/storage-account.bicep' = {
  name: 'deploy-${storageAccountName}'
  params: {
    storageAccountName: storageAccountName 
    storageAccountSku: storageAccountSku
    location: location
    isHnsEnabled: isHnsEnabled
    supportsHttpsTrafficOnly: supportsHttpsTrafficOnly

  }
}


module storageAccountCopy 'modules/storage-account.bicep' = {
  name: 'deploy-${storageAccountNameCopy}'
  params: {
    storageAccountName: storageAccountNameCopy
    storageAccountSku: storageAccountSku
    location: location
    isHnsEnabled: isHnsEnabled
    supportsHttpsTrafficOnly: supportsHttpsTrafficOnly

  }
}

output storageAccountName string = storageAccount.outputs.storageAccountName
output storageAccountNameId string = storageAccount.outputs.storageAccountId

output storageAccountNameCopy string = storageAccountCopy.outputs.storageAccountName
output storageAccountNameCopyId string = storageAccountCopy.outputs.storageAccountId
