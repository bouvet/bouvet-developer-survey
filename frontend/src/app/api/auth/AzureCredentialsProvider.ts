import { SecretClient } from "@azure/keyvault-secrets";
import {
  // AzureCliCredential,
  // ChainedTokenCredential,
  // ManagedIdentityCredential,
  DefaultAzureCredential
} from "@azure/identity";

interface IAzureCredentialsProvider {
  clientSecret: string;
  clientId: string;
  tenantId: string;
  backendClientId: string;
  nextAuthSecret: string;
}

const AzureCredentialsProvider =
  async (): Promise<IAzureCredentialsProvider> => {
    let clientSecret = "",
      clientId = "",
      tenantId = "",
      backendClientId = "",
      nextAuthSecret = "";

    console.log("SKIP KEYVAULT FETCH", process.env.SKIP_KEYVAULT_FETCH);

    if (process.env.NODE_ENV === "development") {
      
    } else {

      if (process.env.SKIP_KEYVAULT_FETCH === "true") {
        try {

          console.log("STARTING CREDENTIAL");
          //https://learn.microsoft.com/en-us/azure/key-vault/secrets/quick-create-node?tabs=azure-cli%2Clinux&pivots=programming-language-typescript
          
          clientId = process.env.AZURE_CLIENT_ID as string;
          tenantId = process.env.AZURE_TENANT_ID as string;
          const subsId = process.env.AZURE_SUBSCRIPTION_ID as string;
          const nexturl = process.env.NEXTAUTH_URL as string;

          console.log("AZURE_CLIENT_ID", clientId);
          console.log("AZURE_TENANT_ID", tenantId);
          console.log("AZURE_SUBSCRIPTION_ID", subsId);
          console.log("NEXTAUTH_URL", nexturl);

          // const credential = new DefaultAzureCredential();


          // use clientid, tenatant and subscription id + oidc github action auth as azure credentials

          const credential = new DefaultAzureCredential({
            managedIdentityClientId: process.env.AZURE_CLIENT_ID,
          });

          console.log("CREDENTIAL DefaultAzureCredential");
          const url = "https://bds-prod-keyvault.vault.azure.net";
          const client = new SecretClient(url, credential);
          console.log("CREDENTIAL CLIENT", client);

          const clientSecretPromise = await client.getSecret(
            "AZURE-AD-CLIENT-SECRET"
          );
          const clientIdPromise = await client.getSecret("AZURE-AD-CLIENT-ID");
          const tenantIdPromise = await client.getSecret("AZURE-AD-TENANT-ID");
          const backendClientIdPromise = await client.getSecret(
            "AZURE-AD-BACKEND-CLIENT-ID"
          );
          const nextAuthSecretPromise = await client.getSecret("NEXTAUTH-SECRET");
          console.log("CREDENTIAL AFTER GETS");

          clientSecret = clientSecretPromise.value!;
          clientId = clientIdPromise.value!;
          tenantId = tenantIdPromise.value!;
          backendClientId = backendClientIdPromise.value!;
          nextAuthSecret = nextAuthSecretPromise.value!;
        } catch (error) {
          console.error("Error fetching from Azure key vault", error);
        }
      }
    }
    return {
      backendClientId,
      clientId,
      clientSecret,
      tenantId,
      nextAuthSecret,
    };
  };

export default AzureCredentialsProvider;
