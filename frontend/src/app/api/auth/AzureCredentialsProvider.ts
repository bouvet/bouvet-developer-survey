import { SecretClient } from "@azure/keyvault-secrets";
import {
  // AzureCliCredential,
  ChainedTokenCredential,
  ManagedIdentityCredential,
  // DefaultAzureCredential
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
      clientId = process.env.AZURE_AD_CLIENT_ID as string;
      clientSecret = process.env.AZURE_AD_CLIENT_SECRET as string;
      tenantId = process.env.AZURE_AD_TENANT_ID as string;
      backendClientId = process.env.AZURE_AD_BACKEND_CLIENT_ID as string;
      nextAuthSecret = process.env.NEXTAUTH_SECRET as string;
    } else {

      if (process.env.SKIP_KEYVAULT_FETCH !== "true") {
        try {

          console.log("STARTING CREDENTIAL");
          //https://learn.microsoft.com/en-us/azure/key-vault/secrets/quick-create-node?tabs=azure-cli%2Clinux&pivots=programming-language-typescript
          const credential = new ChainedTokenCredential(
            new ManagedIdentityCredential({
              objectId: "87415693-0d82-4d96-a402-0bc4e8e5e152",
            })
          );
          // const credential = new DefaultAzureCredential();
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
