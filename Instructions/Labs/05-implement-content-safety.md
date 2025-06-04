---
lab:
    title: 'Implement Azure AI Content Safety'
    description: 'Provision a Content Safety resource to secure your application against harmful content.'
---

# Implement Azure AI Content Safety

In this exercise, you will provision a Content Safety resource, test the resource in Azure AI Foundry, and test the resource in code.

## Provision a *Content Safety* resource

If you don't already have one, you'll need to provision a **Content Safety** resource in your Azure subscription.

1. Open the Azure portal at `https://portal.azure.com`, and sign in using the Microsoft account associated with your Azure subscription.
1. Select **Create a resource**.
1. In the search field, search for **Content Safety**. Then, in the results, select **Create** under **Azure AI Content Safety**.
1. Provision the resource using the following settings:
    - **Subscription**: *Your Azure subscription*.
    - **Resource group**: *Choose or create a resource group*.
    - **Region**: Select **East US**
    - **Name**: *Enter a unique name*.
    - **Pricing tier**: Select **F0** (*free*), or **S** (*standard*) if F0 is not available.
1. Select **Review + create**, then select **Create** to provision the resource.
1. Wait for deployment to complete, and then go to the resource.
1. Select **Access Control** in the left navigation bar, then select **+ Add** and **Add role assignment**.
1. Scroll down to choose the **Cognitive Services User** role and select **Next**.
1. Add your account to this role, and then select **Review + assign**.
1. Select **Resource Management** in the left hand navigation bar and select **Keys and Endpoint**. Leave this page open so you can copy the keys later.

## Use Azure AI Content Safety Prompt Shields

In this exercise you will use Azure AI Foundry to test Content Safety Prompt Shields with two sample inputs. One simulates a user prompt, and the other simulates a document with potentially unsafe text embedded into it.

1. In another browser tab, open the Content Safety page of [Azure AI Foundry](https://ai.azure.com/explore/contentsafety) and sign in.
1. Under **Moderate text content** select **Try it out**.
1. On the **Moderate text content** page, under **Azure AI Services** select the Content Safety resource you created earlier.
1. Select **Multiple risk categories in one sentence**. Review the document text for potential issues.
1. Select **Run test** and review the results.
1. Optionally, alter the threshold levels and select **Run test** again.
1. On the **Moderate text content** drop-down menu at the top of the page, select **Protected material detection for text**.
1. Select **Protected lyrics** and note that these are the lyrics of a published song.
1. Select **Run test** and review the results.
1. On the **Protected material detection for text** drop-down menu at the top of the page, select **Moderate image content**.
1. Select **Self-harm content**.
1. Notice that all images are blurred by default in AI Foundry. You should also be aware that the sexual content in the samples is very mild.
1. Select **Run test** and review the results.
1. On the **Moderate image content** drop-down menu at the top of the page, select **Prompt shields**.
1. On the **Prompt shields page**, under **Azure AI Services** select the Content Safety resource you created earlier.
1. Select **Prompt & document attack content**. Review the user prompt and document text for potential issues.
1. Select **Run test**.
1. In **View results**, verify that Jailbreak attacks were detected in both the user prompt and the document.

    > [!TIP]
    > Code is available for all of the samples in AI Foundry.

1. Under **Next steps**, under **View the code** select **View code**. The **Sample code** window is displayed.
1. Use the down arrow to select either Python or C# and then select **Copy** to copy the sample code to the clipboard.
1. Close the **Sample code** screen.

## Clone the repository in Cloud Shell

You'll develop your code using Cloud Shell from the Azure Portal. The code files for your app have been provided in a GitHub repo.

> **Tip**: If you have already cloned the **mslearn-ai-services** repo recently, you can skip this task. Otherwise, follow these steps to clone it to your development environment.

1. In the Azure Portal, use the **[\>_]** button to the right of the search bar at the top of the page to create a new Cloud Shell in the Azure portal, selecting a ***PowerShell*** environment. The cloud shell provides a command line interface in a pane at the bottom of the Azure portal.

    > **Note**: If you have previously created a cloud shell that uses a *Bash* environment, switch it to ***PowerShell***.

1. In the cloud shell toolbar, in the **Settings** menu, select **Go to Classic version** (this is required to use the code editor).

    > **Tip**: As you paste commands into the cloudshell, the ouput may take up a large amount of the screen buffer. You can clear the screen by entering the `cls` command to make it easier to focus on each task.

1. In the PowerShell pane, enter the following commands to clone the GitHub repo for this exercise:

    ```
    rm -r mslearn-ai-services -f
    git clone https://github.com/microsoftlearning/mslearn-ai-services mslearn-ai-services
    ```

1. After the repo has been cloned, navigate to the language-specific folder containing the application code files, based on the programming language of your choice (Python or C#):  

    **Python**

    ```
   cd mslearn-ai-services/Labfiles/05-implement-content-safety/Python
    ```

    **C#**

    ```
   cd mslearn-ai-services/Labfiles/05-implement-content-safety/C-Sharp
    ```

1. If you chose Python, enter the following command to install the library you'll use:

    **Python**

    ```
   python -m venv labenv
   ./labenv/bin/Activate.ps1
   pip install requests
    ```

1. Next, enter the following command to edit the application file that has been provided:

    **Python**

    ```
   code prompt-shield-py
    ```

    **C#**

    ```
   code Program.cs
    ```

    The file is opened in a code editor.

1. Scroll down to find *Replace with your own subscription _key and endpoint*.
1. In the Azure portal, on the Keys and Endpoint page, copy one of the Keys (1 or 2). Replace **<your_subscription_key>** with this value.
1. In the Azure portal, on the Keys and Endpoint page, copy the Endpoint. Paste this value into your code to replace **<your_endpoint_value>**.
1. In **Azure AI Foundry**, copy the **User prompt** value. Paste this into your code to replace **<test_user_prompt>** (Python) or **<test_content>** (C#).
1. Scroll down to **<this_is_a_document_source>** and delete this line of code.
1. In **Azure AI Foundry**, copy the **Document** value.
1. Scroll down to **<this_is_another_document_source>** and paste your document value.
1. After you've replaced the placeholders, use the **CTRL+S** command to save your changes and then use the **CTRL+Q** command to close the code editor while keeping the cloud shell command line open.
1. Run the program and verify that an attack was detected:

    **Python**

    ```
   python prompt-shield-py
    ```

    **C#**

    ```
   dotnet run
    ```

1. Optionally, you can experiment with different test content and document values.

## Clean up resources

If you're not using the Azure resources created in this lab for other training modules, you can delete them to avoid incurring further charges.

1. Open the Azure portal at `https://portal.azure.com`, and in the top search bar, search for the resources you created in this lab.

2. On the resource page, select **Delete** and follow the instructions to delete the resource. Alternatively, you can delete the entire resource group to clean up all resources at the same time.

