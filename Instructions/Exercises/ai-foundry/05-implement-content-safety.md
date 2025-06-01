---
lab:
    title: 'Get Started with Azure AI Services'
    module: 'Module 6 - Use AI responsibly with Azure AI Foundry Content Safety'
    exercise: 'Implement Azure AI Content Safety'
---
## Prerequisites
### Provision an Azure AI Services Resource

If you don't already have one in your subscription, you'll need to provision an **Azure AI Services** resource.

1. Open the Azure portal at [https://portal.azure.com](https://portal.azure.com), and sign in using the Microsoft account associated with your Azure subscription.
2. In the top search bar, search for **Azure AI services**.
3. Select **Azure AI Services**, and choose to **Create** a new multi-service account resource.
4. Configure the resource with the following settings:

   * **Subscription**: Your Azure subscription
   * **Resource group**: Choose or create a resource group (If you're using a restricted subscription, use the provided one if you're unable to create a new one)
   * **Region**: Choose any available region (e.g., East US)
   * **Name**: Enter a unique name
   * **Pricing tier**: Standard S0
5. Check the required boxes (e.g., terms of service) and click **Create**.
6. Wait for the deployment to complete, then view the deployment details.

### Create a Project in Azure AI Foundry

Azure AI Studio has been renamed to **Azure AI Foundry** as of May 2025. All development now happens in this new interface.

#### Access Azure AI Foundry

1. Go to [https://ai.azure.com/](https://ai.azure.com/)
2. Sign in using your Microsoft account associated with your Azure subscription.

#### Create a New Foundry Project

1. On the dashboard, click **Create Project**.
2. Fill in the project details:

   * **Project Name**: Enter a unique name (e.g., `content-safety-lab`)
   * **Region**: Select **East US**
   * **Description**: Optional
3. Click **Create** to finalize the project setup.

Once created, you will be taken into the Azure AI Foundry project workspace.

### Locate Keys and Endpoint in Azure AI Foundry

1. In your Azure AI Foundry project, look to the left navigation menu.
2. Under **My Assets**, select **Models + endpoints**.
3. Here, you will find your **Endpoint URL** and the **API keys** required for authentication when using SDKs or REST APIs.

Be sure to copy these values and store them securely—they are necessary to configure your applications or run code samples.

---

## Test Text and Image Content 

In this section, you will evaluate sample content for safety risks using built-in tools in Azure AI Foundry.
## Use an SDK

You can write code that consumes Azure AI services REST APIs directly, but there are software development kits (SDKs) for many popular programming languages, including Microsoft C#, Python, Java, and Node.js. Using an SDK can greatly simplify development of applications that consume Azure AI services.

1. In Visual Studio Code, expand the `Labfiles/05-implement-content-safety` folder under the **C-Sharp** or **Python** folder, depending on your language preference. Then run `cd ../../../Labfiles/05-implement-content-safety/python` to change into the relevant **sdk-client** folder.

2. Install the contentsafety Analytics SDK package by running the appropriate command for your language preference:

    **C#**

    ```
    dotnet add package Azure.AI.ContentSafety --version 1.0.0
    ```

    **Python**

    ```
    pip install azure-ai-contentsafety python-dotenv

    ```

3. View the contents of the folders, and note that it contains a file for configuration settings:

    - **C#**: .env
    - **Python**: .env

    Open the configuration file and update the configuration values it contains to reflect the **endpoint** and an authentication **key** for your Azure AI services resource. Save your changes.
    
4. Note that the **sdk-client** folder contains a code file for the client application:

    - **C#**: Program.cs
    - **Python**: sdk-client.py       //multiple sample files to analize text and image

    Open the code file and review the code it contains, noting the following details:
    - The namespace for the SDK you installed is imported
    - Code in the **Main** function retrieves the endpoint and key for your Azure AI services resource - these will be used with the SDK to create a client for the content safety service.
    

5. Return to the terminal, ensure you are in the **sdk-client** folder, and enter the following command to run the program:

    **C#**
    ```
    # Load environment variables in terminal (Linux/macOS)
    export $(cat .env | xargs) && dotnet run
    
    # Load environment variables in Windows PowerShell
    Get-Content .env | ForEach-Object { $name, $value = $_ -split '=' [System.Environment]::SetEnvironmentVariable($name, $value, 'Process')}

    dotnet run
    ```

    **Python**

    ```
    python sample_analyze_image.py
    python sample_analyze_text_2.py
    python sample_analyze_text.py
    ```


> **Note**: You might encounter issue with packages so install it and rerun cmd.

To experiment further:

* You can modify the test inputs and rerun evaluations.

---

## Clean Up 

After completing the lab, you should clean up your Azure resources to avoid unnecessary charges.

### Delete the Azure AI Foundry Project

1. Navigate to [https://ai.azure.com](https://ai.azure.com).
2. On your Foundry dashboard, locate your project.
3. Click the ellipsis (…) next to your project name and select **Delete project**.
4. Confirm the deletion when prompted.

### Delete the Azure AI Services Resource

1. Go to [https://portal.azure.com](https://portal.azure.com).
2. In the search bar, type **Azure AI Services**.
3. Locate the resource you created.
4. Select it, then click **Delete** at the top.
5. Confirm the deletion.

Cleaning up these resources ensures you are not billed beyond the scope of the lab.

---
