@description('The location for the resource(s) to be deployed.')
param location string = resourceGroup().location

param neuronest_aca_outputs_azure_container_apps_environment_default_domain string

param neuronest_aca_outputs_azure_container_apps_environment_id string

param backend_containerimage string

param backend_identity_outputs_id string

param backend_containerport string

param postgres_outputs_connectionstring string

param postgres_outputs_hostname string

param backend_identity_outputs_clientid string

param neuronest_aca_outputs_azure_container_registry_endpoint string

param neuronest_aca_outputs_azure_container_registry_managed_identity_id string

resource backend 'Microsoft.App/containerApps@2025-02-02-preview' = {
  name: 'backend'
  location: location
  properties: {
    configuration: {
      activeRevisionsMode: 'Single'
      ingress: {
        external: false
        targetPort: int(backend_containerport)
        transport: 'http'
      }
      registries: [
        {
          server: neuronest_aca_outputs_azure_container_registry_endpoint
          identity: neuronest_aca_outputs_azure_container_registry_managed_identity_id
        }
      ]
      runtime: {
        dotnet: {
          autoConfigureDataProtection: true
        }
      }
    }
    environmentId: neuronest_aca_outputs_azure_container_apps_environment_id
    template: {
      containers: [
        {
          image: backend_containerimage
          name: 'backend'
          env: [
            {
              name: 'OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EXCEPTION_LOG_ATTRIBUTES'
              value: 'true'
            }
            {
              name: 'OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EVENT_LOG_ATTRIBUTES'
              value: 'true'
            }
            {
              name: 'OTEL_DOTNET_EXPERIMENTAL_OTLP_RETRY'
              value: 'in_memory'
            }
            {
              name: 'ASPNETCORE_FORWARDEDHEADERS_ENABLED'
              value: 'true'
            }
            {
              name: 'HTTP_PORTS'
              value: backend_containerport
            }
            {
              name: 'ConnectionStrings__HealthTracker'
              value: '${postgres_outputs_connectionstring};Database=HealthTracker'
            }
            {
              name: 'HEALTHTRACKER_HOST'
              value: postgres_outputs_hostname
            }
            {
              name: 'HEALTHTRACKER_PORT'
              value: '5432'
            }
            {
              name: 'HEALTHTRACKER_URI'
              value: 'postgresql://${postgres_outputs_hostname}/HealthTracker'
            }
            {
              name: 'HEALTHTRACKER_JDBCCONNECTIONSTRING'
              value: 'jdbc:postgresql://${postgres_outputs_hostname}/HealthTracker?sslmode=require&authenticationPluginClassName=com.azure.identity.extensions.jdbc.postgresql.AzurePostgresqlAuthenticationPlugin'
            }
            {
              name: 'HEALTHTRACKER_DATABASENAME'
              value: 'HealthTracker'
            }
            {
              name: 'AZURE_CLIENT_ID'
              value: backend_identity_outputs_clientid
            }
            {
              name: 'AZURE_TOKEN_CREDENTIALS'
              value: 'ManagedIdentityCredential'
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
      '${backend_identity_outputs_id}': { }
      '${neuronest_aca_outputs_azure_container_registry_managed_identity_id}': { }
    }
  }
}