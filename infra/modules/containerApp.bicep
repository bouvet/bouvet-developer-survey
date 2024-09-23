

@description('The location to deploy resource')
param location string

@description('Application suffix that will be applied to all resources')
param appSuffix string = uniqueString(resourceGroup().id)

@description('The name of the Container App Environment')
param containerAppName string

@description('The name of the Container App Environment')
param containerAppEnvironmentName string = 'env${appSuffix}'

@description('Log analytics workspace name')
param logAnalyticsWorkspaceName string

@description('The name of the ACR login server.')
param acrLoginServer string

@description('The image tag for the container.')
param imageTag string

resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: logAnalyticsWorkspaceName
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
  }
}

resource env 'Microsoft.App/managedEnvironments@2023-08-01-preview' = {
  name: containerAppEnvironmentName
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalytics.properties.customerId
        sharedKey: logAnalytics.listKeys().primarySharedKey
      }
    }
  }
}

resource containerApp 'Microsoft.App/containerApps@2023-08-01-preview' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: env.id
    configuration: {
      ingress: {
        external: true
        targetPort: 80
        allowInsecure: false
        traffic: [
          {
            latestRevision: true
            weight: 100
          }
        ]
      }
    }
    template: {
      containers: [
        {
          name: containerAppName
          image: '${acrLoginServer}/backend-image:${imageTag}'
          resources: {
            cpu: json('1.0')
            memory: '2Gi'
          }
        }
      ]
      scale: {
        minReplicas: 0
        maxReplicas: 3
      }
    }
  }
}
