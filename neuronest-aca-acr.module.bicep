@description('The location for the resource(s) to be deployed.')
param location string = resourceGroup().location

resource neuronest_aca_acr 'Microsoft.ContainerRegistry/registries@2025-04-01' = {
  name: take('neuronestacaacr${uniqueString(resourceGroup().id)}', 50)
  location: location
  sku: {
    name: 'Basic'
  }
  tags: {
    'aspire-resource-name': 'neuronest-aca-acr'
  }
}

output name string = neuronest_aca_acr.name

output loginServer string = neuronest_aca_acr.properties.loginServer