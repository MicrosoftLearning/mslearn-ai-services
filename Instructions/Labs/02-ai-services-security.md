---
lab:
  title: "Manage Azure AI Services Security"
  module: "Module 2 - Developing AI Apps with Azure AI Services"
---

# Manage Azure AI Services Security

Security is a critical consideration for any application, and as a developer you should ensure that access to resources such as Azure AI services is restricted to only those who require it.

Access to Azure AI services is typically controlled through authentication keys, which are generated when you initially create an Azure AI services resource.

## Clone the repository for this course in Cloud Shell

Open up a new browser tab to work with Cloud Shell. If you haven't cloned this repository to Cloud Shell recently, follow the steps below to make sure you have the most recent version. Otherwise, open Cloud Shell and navigate to your clone.

1. In the [Azure portal](https://portal.azure.com?azure-portal=true), select the **[>_]** (_Cloud Shell_) button at the top of the page to the right of the search box. A Cloud Shell pane will open at the bottom of the portal.

   ![Screenshot of starting Cloud Shell by clicking on the icon to the right of the top search box.](../media/cloudshell-launch-portal.png#lightbox)

2. The first time you open the Cloud Shell, you may be prompted to choose the type of shell you want to use (_Bash_ or _PowerShell_). Select **Bash**. If you don't see this option, skip the step.

3. If you're prompted to create storage for your Cloud Shell, ensure your subscription is specified and select **Create storage**. Then wait a minute or so for the storage to be created.

4. Make sure the type of shell indicated on the top left of the Cloud Shell pane is switched to _Bash_. If it's _PowerShell_, switch to _Bash_ by using the drop-down menu.

5. Once the terminal starts, enter the following command to download the sample application and save it to a folder called `labs`.

   ```bash
   git clone https://github.com/MicrosoftLearning/mslearn-ai-services labs
   ```

6. The files are downloaded to a folder named **labs**. Navigate to the lab files for this exercise using the following command.

   ```bash
   cd labs/Labfiles/02-ai-services-security
   ```

Use the following command to open the lab files in the built-in code editor.

```bash
code .
```

Code for both C# and Python has been provided.

## Provision an Azure AI Services resource

If you don't already have one in your subscription, you'll need to provision an **Azure AI Services** resource.

1. Open the Azure portal at `https://portal.azure.com`, and sign in using the Microsoft account associated with your Azure subscription.
2. In the top search bar, search for _Azure AI services_, select **Azure AI Services**, and create an Azure AI services multi-service account resource with the following settings:
   - **Subscription**: _Your Azure subscription_
   - **Resource group**: _Choose or create a resource group (if you are using a restricted subscription, you may not have permission to create a new resource group - use the one provided)_
   - **Region**: _Choose any available region_
   - **Name**: _Enter a unique name_
   - **Pricing tier**: Standard S0
3. Select the required checkboxes and create the resource.
4. Wait for deployment to complete, and then view the deployment details.

## Manage authentication keys

When you created your Azure AI services resource, two authentication keys were generated. You can manage these in the Azure portal or by using the Azure command line interface (CLI).

1. In the Azure portal, go to your Azure AI services resource and view its **Keys and Endpoint** page. This page contains the information that you will need to connect to your resource and use it from applications you develop. Specifically:
   - An HTTP _endpoint_ to which client applications can send requests.
   - Two _keys_ that can be used for authentication (client applications can use either of the keys. A common practice is to use one for development, and another for production. You can easily regenerate the development key after developers have finished their work to prevent continued access).
   - The _location_ where the resource is hosted. This is required for requests to some (but not all) APIs.
2. Now you can use the following command to get the list of Azure AI services keys, replacing _&lt;resourceName&gt;_ with the name of your Azure AI services resource, and _&lt;resourceGroup&gt;_ with the name of the resource group in which you created it.

   ```
   az cognitiveservices account keys list --name <resourceName> --resource-group <resourceGroup>
   ```

   The command returns a list of the keys for your Azure AI services resource - there are two keys, named **key1** and **key2**.

3. To test your Azure AI service, you can use **curl** - a command line tool for HTTP requests. In the **02-ai-services-security** folder, open **rest-test.sh** and edit the **curl** command it contains (shown below), replacing _&lt;yourEndpoint&gt;_ and _&lt;yourKey&gt;_ with your endpoint URI and **Key1** key to use the Text Analytics API in your Azure AI services resource.

   ```bash
   curl -X POST "<your-endpoint>/text/analytics/v3.1/languages?" -H "Content-Type: application/json" -H "Ocp-Apim-Subscription-Key: <your-key>" --data-ascii "{'documents':[{'id':1,'text':'hello'}]}"
   ```

4. Save your changes, and then run the following command:

   ```
   sh rest-test.sh
   ```

The command returns a JSON document containing information about the language detected in the input data (which should be English).

5. If a key becomes compromised, or the developers who have it no longer require access, you can regenerate it in the portal or by using the Azure CLI. Run the following command to regenerate your **key1** key (replacing _&lt;resourceName&gt;_ and _&lt;resourceGroup&gt;_ for your resource).

   ```
   az cognitiveservices account keys regenerate --name <resourceName> --resource-group <resourceGroup> --key-name key1
   ```

The list of keys for your Azure AI services resource is returned - note that **key1** has changed since you last retrieved them.

6. Re-run the **rest-test** command with the old key (you can use the **^** arrow on your keyboard to cycle through previous commands), and verify that it now fails.
7. Edit the _curl_ command in **rest-test.sh** replacing the key with the new **key1** value, and save the changes. Then rerun the **rest-test** command and verify that it succeeds.

> **Tip**: In this exercise, you used the full names of Azure CLI parameters, such as **--resource-group**. You can also use shorter alternatives, such as **-g**, to make your commands less verbose (but a little harder to understand). The [Azure AI Services CLI command reference](https://docs.microsoft.com/cli/azure/cognitiveservices?view=azure-cli-latest) lists the parameter options for each Azure AI services CLI command.

## Secure key access with Azure Key Vault

You can develop applications that consume Azure AI services by using a key for authentication. However, this means that the application code must be able to obtain the key. One option is to store the key in an environment variable or a configuration file where the application is deployed, but this approach leaves the key vulnerable to unauthorized access. A better approach when developing applications on Azure is to store the key securely in Azure Key Vault, and provide access to the key through a _managed identity_ (in other words, a user account used by the application itself).

### Create a key vault and add a secret

First, you need to create a key vault and add a _secret_ for the Azure AI services key.

1. Make a note of the **key1** value for your Azure AI services resource (or copy it to the clipboard).
2. In the Azure portal, on the **Home** page, select the **&#65291;Create a resource** button, search for _Key Vault_, and create a **Key Vault** resource with the following settings:

   - **Basics** tab

     - **Subscription**: _Your Azure subscription_
     - **Resource group**: _The same resource group as your Azure AI service resource_
     - **Key vault name**: _Enter a unique name_
     - **Region**: _The same region as your Azure AI service resource_
     - **Pricing tier**: Standard

   - **Access configuration** tab
     - **Permission model**: Vault access policy
     - Scroll down to the **Access policies** section and select your user using the checkbox on the left. Then select **Review + create**, and select **Create** to create your resource.

3. Wait for deployment to complete and then go to your key vault resource.
4. In the left navigation pane, select **Secrets** (in the Objects section).
5. Select **+ Generate/Import** and add a new secret with the following settings :
   - **Upload options**: Manual
   - **Name**: AI-Services-Key _(it's important to match this exactly, because later you'll run code that retrieves the secret based on this name)_
   - **Value**: _Your **key1** Azure AI services key_
6. Select **Create**.

### Create a service principal

To access the secret in the key vault, your application must use a service principal that has access to the secret. You'll use the Azure command line interface (CLI) to create the service principal, find its object ID, and grant access to the secret in Azure Vault.

1. Run the following Azure CLI command, replacing _&lt;spName&gt;_ with a unique suitable name for an application identity (for example, _ai-app_ with your initials appended on the end; the name must be unique within your tenant). Also replace _&lt;subscriptionId&gt;_ and _&lt;resourceGroup&gt;_ with the correct values for your subscription ID and the resource group containing your Azure AI services and key vault resources:

   > **Tip**: If you are unsure of your subscription ID, use the **az account show** command to retrieve your subscription information - the subscription ID is the **id** attribute in the output. If you see an error about the object already existing, please choose a different unique name.

   ```
   az ad sp create-for-rbac -n "api://<spName>" --role owner --scopes subscriptions/<subscriptionId>/resourceGroups/<resourceGroup>
   ```

The output of this command includes information about your new service principal. It should look similar to this:

    ```
    {
        "appId": "abcd12345efghi67890jklmn",
        "displayName": "ai-app",
        "name": "http://ai-app",
        "password": "1a2b3c4d5e6f7g8h9i0j",
        "tenant": "1234abcd5678fghi90jklm"
    }
    ```

Make a note of the **appId**, **password**, and **tenant** values - you will need them later (if you close this terminal, you won't be able to retrieve the password; so it's important to note the values now - you can paste the output into a new text file on your local machine to ensure you can find the values you need later!)

2. To get the **object ID** of your service principal, run the following Azure CLI command, replacing _&lt;appId&gt;_ with the value of your service principal's app ID.

   ```
   az ad sp show --id <appId>
   ```

3. Copy the `id` value in the json returned in response.
4. To assign permission for your new service principal to access secrets in your Key Vault, run the following Azure CLI command, replacing _&lt;keyVaultName&gt;_ with the name of your Azure Key Vault resource and _&lt;objectId&gt;_ with the value of your service principal's ID value you've just copied.

   ```
   az keyvault set-policy -n <keyVaultName> --object-id <objectId> --secret-permissions get list
   ```

### Use the service principal in an application

Now you're ready to use the service principal identity in an application, so it can access the secret Azure AI services key in your key vault and use it to connect to your Azure AI services resource.

> **Note**: In this exercise, we'll store the service principal credentials in the application configuration and use them to authenticate a **ClientSecretCredential** identity in your application code. This is fine for development and testing, but in a real production application, an administrator would assign a _managed identity_ to the application so that it uses the service principal identity to access resources, without caching or storing the password.

1. In your terminal, switch to the **C-Sharp** or **Python** folder depending on your language preference. By running `cd C-Sharp` or `cd Python`. Then run `cd keyvault_client` for **C-Sharp** or `cd keyvault-client` for **Python**.
2. Install the packages you will need to use for Azure Key Vault and the Text Analytics API in your Azure AI services resource by running the appropriate command for your language preference:

   **C#**

   ```
   dotnet add package Azure.AI.TextAnalytics --version 5.3.0
   dotnet add package Azure.Identity --version 1.5.0
   dotnet add package Azure.Security.KeyVault.Secrets --version 4.2.0-beta.3
   ```

   **Python**

   ```
   pip install azure-ai-textanalytics==5.3.0
   pip install azure-identity==1.5.0
   pip install azure-keyvault-secrets==4.2.0
   ```

3. View the contents of the **keyvault-client** folder, and note that it contains a file for configuration settings:

   - **C#**: appsettings.json
   - **Python**: .env

   Open the configuration file and update the configuration values it contains to reflect the following settings:

   - The **endpoint** for your Azure AI Services resource
   - The name of your **Azure Key Vault** resource
   - The **tenant** for your service principal
   - The **appId** for your service principal
   - The **password** for your service principal

   Save your changes by pressing **CTRL+S**.

4. Note that the **keyvault-client** folder contains a code file for the client application:

   - **C#**: Program.cs
   - **Python**: keyvault-client.py

   Open the code file and review the code it contains, noting the following details:

   - The namespace for the SDK you installed is imported
   - Code in the **Main** function retrieves the application configuration settings, and then it uses the service principal credentials to get the Azure AI services key from the key vault.
   - The **GetLanguage** function uses the SDK to create a client for the service, and then uses the client to detect the language of the text that was entered.

5. Enter the following command to run the program:

   **C#**

   ```
   dotnet run
   ```

   **Python**

   ```
   python keyvault-client.py
   ```

6. When prompted, enter some text and review the language that is detected by the service. For example, try entering "Hello", "Bonjour", and "Gracias".
7. When you have finished testing the application, enter "quit" to stop the program.

## More information

For more information about securing Azure AI services, see the [Azure AI Services security documentation](https://docs.microsoft.com/azure/ai-services/security-features).
