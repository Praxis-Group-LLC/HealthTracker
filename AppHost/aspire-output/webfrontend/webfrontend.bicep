@description('The location for the resource(s) to be deployed.')
param location string = resourceGroup().location

param xenonest_aca_outputs_azure_container_apps_environment_default_domain string

param xenonest_aca_outputs_azure_container_apps_environment_id string

param webfrontend_containerimage string

param xenonest_aca_outputs_azure_container_registry_endpoint string

param xenonest_aca_outputs_azure_container_registry_managed_identity_id string

resource webfrontend 'Microsoft.App/containerApps@2025-01-01' = {
  name: 'webfrontend'
  location: location
  properties: {
    configuration: {
      activeRevisionsMode: 'Single'
      ingress: {
        external: false
        targetPort: 8000
        transport: 'http'
      }
      registries: [
        {
          server: xenonest_aca_outputs_azure_container_registry_endpoint
          identity: xenonest_aca_outputs_azure_container_registry_managed_identity_id
        }
      ]
    }
    environmentId: xenonest_aca_outputs_azure_container_apps_environment_id
    template: {
      containers: [
        {
          image: webfrontend_containerimage
          name: 'webfrontend'
          env: [
            {
              name: 'NODE_ENV'
              value: 'production'
            }
            {
              name: 'PORT'
              value: '8000'
            }
            {
              name: 'BACKEND_HTTP'
              value: 'https://backend.${xenonest_aca_outputs_azure_container_apps_environment_default_domain}'
            }
            {
              name: 'services__backend__http__0'
              value: 'https://backend.${xenonest_aca_outputs_azure_container_apps_environment_default_domain}'
            }
            {
              name: 'BACKEND_HTTPS'
              value: 'https://backend.${xenonest_aca_outputs_azure_container_apps_environment_default_domain}'
            }
            {
              name: 'services__backend__https__0'
              value: 'https://backend.${xenonest_aca_outputs_azure_container_apps_environment_default_domain}'
            }
          ]
        }
      ]
      scale: {
        minReplicas: 1
      }
    }
  }
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${xenonest_aca_outputs_azure_container_registry_managed_identity_id}': { }
    }
  }
}