targetScope = 'subscription'

param resourceGroupName string

param location string

param principalId string

resource rg 'Microsoft.Resources/resourceGroups@2023-07-01' = {
  name: resourceGroupName
  location: location
}

module xenonest_aca_acr 'xenonest-aca-acr/xenonest-aca-acr.bicep' = {
  name: 'xenonest-aca-acr'
  scope: rg
  params: {
    location: location
  }
}

module xenonest_aca 'xenonest-aca/xenonest-aca.bicep' = {
  name: 'xenonest-aca'
  scope: rg
  params: {
    location: location
    xenonest_aca_acr_outputs_name: xenonest_aca_acr.outputs.name
    userPrincipalId: principalId
  }
}

module postgres 'postgres/postgres.bicep' = {
  name: 'postgres'
  scope: rg
  params: {
    location: location
  }
}

module backend_identity 'backend-identity/backend-identity.bicep' = {
  name: 'backend-identity'
  scope: rg
  params: {
    location: location
  }
}

module backend_roles_postgres 'backend-roles-postgres/backend-roles-postgres.bicep' = {
  name: 'backend-roles-postgres'
  scope: rg
  params: {
    location: location
    postgres_outputs_name: postgres.outputs.name
    principalId: backend_identity.outputs.principalId
    principalName: backend_identity.outputs.principalName
  }
}

output xenonest_aca_AZURE_CONTAINER_APPS_ENVIRONMENT_DEFAULT_DOMAIN string = xenonest_aca.outputs.AZURE_CONTAINER_APPS_ENVIRONMENT_DEFAULT_DOMAIN

output xenonest_aca_AZURE_CONTAINER_APPS_ENVIRONMENT_ID string = xenonest_aca.outputs.AZURE_CONTAINER_APPS_ENVIRONMENT_ID

output xenonest_aca_AZURE_CONTAINER_REGISTRY_ENDPOINT string = xenonest_aca.outputs.AZURE_CONTAINER_REGISTRY_ENDPOINT

output xenonest_aca_AZURE_CONTAINER_REGISTRY_MANAGED_IDENTITY_ID string = xenonest_aca.outputs.AZURE_CONTAINER_REGISTRY_MANAGED_IDENTITY_ID

output backend_identity_id string = backend_identity.outputs.id

output postgres_connectionString string = postgres.outputs.connectionString

output postgres_hostName string = postgres.outputs.hostName

output backend_identity_clientId string = backend_identity.outputs.clientId