@description('Azure AI Services module name')
param aiServicesName string

@description('The location for the AI Services.')
param location string

@description('The deployment name of the AI Services.')
param deploymentName string

@description('The SKU name of the AI Services.')
param sku string = 'S0'

@description('The capacity of the AI Services.')
param capacity int = 30

resource openai 'Microsoft.CognitiveServices/accounts@2023-05-01' = {
  name: aiServicesName
  location: location
  sku: {
    name: sku
  }
  kind: 'OpenAI'
}

resource openaideployment 'Microsoft.CognitiveServices/accounts/deployments@2023-05-01' = {
  name: deploymentName
  sku: {
    name: 'Standard'
    capacity: capacity
  }
  parent: openai
  properties: {
    model: {
      name: 'gpt-4o-mini'
      format: 'OpenAI'
      version: '2024-07-18'
    }
    raiPolicyName: 'Microsoft.Default'
    versionUpgradeOption: 'OnceCurrentVersionExpired'
  }
}
