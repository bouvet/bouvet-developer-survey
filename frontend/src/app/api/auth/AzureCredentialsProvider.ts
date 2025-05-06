import { SecretClient } from "@azure/keyvault-secrets";
import { DefaultAzureCredential } from "@azure/identity";

interface IAzureCredentialsProvider {
  clientSecret: string;
  clientId: string;
  tenantId: string;
  backendClientId: string;
}

const AzureCredentialsProvider =
  async (): Promise<IAzureCredentialsProvider> => {
    let clientSecret = "",
      clientId = "",
      tenantId = "",
      backendClientId = "";

    if (process.env.NODE_ENV !== "development") {
      clientId = process.env.AZURE_AD_CLIENT_ID as string;
      clientSecret = process.env.AZURE_AD_CLIENT_SECRET as string;
      tenantId = process.env.AZURE_AD_TENANT_ID as string;
      backendClientId = process.env.AZURE_AD_BACKEND_CLIENT_ID as string;
    } else {
      try {
        console.log("STARTING CREDENTIAL");
        //https://learn.microsoft.com/en-us/azure/key-vault/secrets/quick-create-node?tabs=azure-cli%2Clinux&pivots=programming-language-typescript
        const credential = new DefaultAzureCredential();
        console.log("CREDENTIAL DefaultAzureCredential");
        const url = "https://bds-prod-keyvault.vault.azure.net";
        const client = new SecretClient(url, credential);
        console.log("CREDENTIAL CLIENT");

        const clientSecretPromise = await client.getSecret(
          "AZURE-AD-CLIENT-SECRET"
        );
        const clientIdPromise = await client.getSecret("AZURE-AD-CLIENT-ID");
        const tenantIdPromise = await client.getSecret("AZURE-AD-TENANT-ID");
        const backendClientIdPromise = await client.getSecret(
          "AZURE-AD-BACKEND-CLIENT-ID"
        );
        console.log("CREDENTIAL AFTER GETS");

        clientSecret = clientSecretPromise.value!;
        clientId = clientIdPromise.value!;
        tenantId = tenantIdPromise.value!;
        backendClientId = backendClientIdPromise.value!;
      } catch (error) {
        console.error("Error fetching from Azure key vault", error);
      }
    }
    return {
      backendClientId,
      clientId,
      clientSecret,
      tenantId,
    };
  };

export default AzureCredentialsProvider;
