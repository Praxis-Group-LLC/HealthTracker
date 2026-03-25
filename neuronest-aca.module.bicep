@description('The location for the resource(s) to be deployed.')
param location string = resourceGroup().location

param userPrincipalId string = ''

param tags object = { }

param neuronest_aca_acr_outputs_name string

resource neuronest_aca_mi 'Microsoft.ManagedIdentity/userAssignedIdentities@2024-11-30' = {
  name: take('neuronest_aca_mi-${uniqueString(resourceGroup().id)}', 128)
  location: location
  tags: tags
}

resource neuronest_aca_acr 'Microsoft.ContainerRegistry/registries@2025-04-01' existing = {
  name: neuronest_aca_acr_outputs_name
}

resource neuronest_aca_acr_neuronest_aca_mi_AcrPull 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(neuronest_aca_acr.id, neuronest_aca_mi.id, subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '7f951dda-4ed3-4680-a7ca-43fe172d538d'))
  properties: {
    principalId: neuronest_aca_mi.properties.principalId
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '7f951dda-4ed3-4680-a7ca-43fe172d538d')
    principalType: 'ServicePrincipal'
  }
  scope: neuronest_aca_acr
}

resource neuronest_aca_law 'Microsoft.OperationalInsights/workspaces@2025-02-01' = {
  name: take('neuronestacalaw-${uniqueString(resourceGroup().id)}', 63)
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
  }
  tags: tags
}

resource neuronest_aca 'Microsoft.App/managedEnvironments@2025-01-01' = {
  name: take('neuronestaca${uniqueString(resourceGroup().id)}', 24)
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: neuronest_aca_law.properties.customerId
        sharedKey: neuronest_aca_law.listKeys().primarySharedKey
      }
    }
    workloadProfiles: [
      {
        name: 'consumption'
        workloadProfileType: 'Consumption'
      }
    ]
  }
  tags: tags
}

output AZURE_LOG_ANALYTICS_WORKSPACE_NAME string = neuronest_aca_law.name

output AZURE_LOG_ANALYTICS_WORKSPACE_ID string = neuronest_aca_law.id

output AZURE_CONTAINER_REGISTRY_NAME string = neuronest_aca_acr.name

output AZURE_CONTAINER_REGISTRY_ENDPOINT string = neuronest_aca_acr.properties.loginServer

output AZURE_CONTAINER_REGISTRY_MANAGED_IDENTITY_ID string = neuronest_aca_mi.id

output AZURE_CONTAINER_APPS_ENVIRONMENT_NAME string = neuronest_aca.name

output AZURE_CONTAINER_APPS_ENVIRONMENT_ID string = neuronest_aca.id

output AZURE_CONTAINER_APPS_ENVIRONMENT_DEFAULT_DOMAIN string = neuronest_aca.properties.defaultDomain