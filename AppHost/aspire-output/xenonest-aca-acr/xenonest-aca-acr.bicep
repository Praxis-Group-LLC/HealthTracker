@description('The location for the resource(s) to be deployed.')
param location string = resourceGroup().location

resource xenonest_aca_acr 'Microsoft.ContainerRegistry/registries@2025-04-01' = {
  name: take('xenonestacaacr${uniqueString(resourceGroup().id)}', 50)
  location: location
  sku: {
    name: 'Basic'
  }
  tags: {
    'aspire-resource-name': 'xenonest-aca-acr'
  }
}

output name string = xenonest_aca_acr.name

output loginServer string = xenonest_aca_acr.properties.loginServer