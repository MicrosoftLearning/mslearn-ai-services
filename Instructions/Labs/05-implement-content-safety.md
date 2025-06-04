---
lab:
    title: 'Implement Azure AI Content Safety'
---

# Implement Azure AI Content Safety

In this exercise, you will provision a Content Safety resource, test the resource in Azure AI Studio, and test the resource in code.

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

In this exercise you will use Azure AI Studio to test Content Safety Prompt Shields with two sample inputs. One simulates a user prompt, and the other simulates a document with potentially unsafe text embedded into it.

1. In another browser tab, open the Content Safety page of [Azure AI Studio](https://ai.azure.com/explore/contentsafety) and sign in.
1. Under **Moderate text content** select **Try it out**.
1. On the **Moderate text content** page, under **Azure AI Services** select the Content Safety resource you created earlier.
1. Select **Multiple risk categories in one sentence**. Review the document text for potential issues.
1. Select **Run test** and review the results.
1. Optionally, alter the threshold levels and select **Run test** again.
1. On the left navigation bar, select **Protected material detection for text**.
1. Select **Protected lyrics** and note that these are the lyrics of a published song.
1. Select **Run test** and review the results.
1. On the left navigation bar, select **Moderate image content**.
1. Select **Self-harm content**.
1. Notice that all images are blurred by default in AI Studio. You should also be aware that the sexual content in the samples is very mild.
1. Select **Run test** and review the results.
1. On the left navigation bar, select **Prompt shields**.
1. On the **Prompt shields page**, under **Azure AI Services** select the Content Safety resource you created earlier.
1. Select **Prompt & document attack content**. Review the user prompt and document text for potential issues.
1. Select **Run test**.
1. In **View results**, verify that Jailbreak attacks were detected in both the user prompt and the document.

    > [!TIP]
    > Code is available for all of the samples in AI Studio.

1. Under **Next steps**, under **View the code** select **View code**. The **Sample code** window is displayed.
1. Use the down arrow to select either Python or C# and then select **Copy** to copy the sample code to the clipboard.
1. Close the **Sample code** screen.

### Configure your application

You will now create an application in either C# or Python.

#### C#

##### Prerequisites

* [Visual Studio Code](https://code.visualstudio.com/) on one of the [supported platforms](https://code.visualstudio.com/docs/supporting/requirements#_platforms).
* [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) is the target framework for this exercise.
* The [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) for Visual Studio Code.

##### Setting up

Perform the following steps to prepare Visual Studio Code for the exercise.

1. Start Visual Studio Code and in the Explorer view, click **Create .NET Project** selecting **Console App**.
1. Select a folder on your computer, and give the project a name. Select **Create project** and acknowledge the warning message.
1. In the Explorer pane, expand Solution Explorer and select **Program.cs**.
1. Build and run the project by selecting **Run** -> **Run without Debugging**. 
1. Under Solution Explorer, right-click the C# project and select **Add NuGet Package.**
1. Search for **Azure.AI.TextAnalytics** and select the latest version.
1. Search for a second NuGet Package: **Microsoft.Extensions.Configuration.Json 8.0.0**. The project file should now list two NuGet packages.

##### Add code

1. Paste the sample code you copied earlier under the **ItemGroup** section.
1. Scroll down to find *Replace with your own subscription _key and endpoint*.
1. In the Azure portal, on the Keys and Endpoint page, copy one of the Keys (1 or 2). Replace **<your_subscription_key>** with this value.
1. In the Azure portal, on the Keys and Endpoint page, copy the Endpoint. Paste this value into your code to replace **<your_endpoint_value>**.
1. In **Azure AI Studio**, copy the **User prompt** value. Paste this into your code to replace **<test_user_prompt>**.
1. Scroll down to **<this_is_a_document_source>** and delete this line of code.
1. In **Azure AI Studio**, copy the **Document** value.
1. Scroll down to **<this_is_another_document_source>** and paste your document value.
1. Select **Run** -> **Run without Debugging** and verify that an attack was detected. 

#### Python

##### Prerequisites

* [Visual Studio Code](https://code.visualstudio.com/) on one of the [supported platforms](https://code.visualstudio.com/docs/supporting/requirements#_platforms).

* The [Python extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python) is installed for Visual Studio Code.

* The [requests module](https://pypi.org/project/requests/) is installed.

1. Create a new Python file with a **.py** extension and give it a suitable name.
1. Paste the sample code you copied earlier.
1. Scroll down to find the section titled *Replace with your own subscription _key and endpoint*.
1. In the Azure portal, on the Keys and Endpoint page, copy one of the Keys (1 or 2). Replace **<your_subscription_key>** with this value.
1. In the Azure portal, on the Keys and Endpoint page, copy the Endpoint. Paste this value into your code to replace **<your_endpoint_value>**.
1. In **Azure AI Studio**, copy the **User prompt** value. Paste this into your code to replace **<test_user_prompt>**.
1. Scroll down to **<this_is_a_document_source>** and delete this line of code.
1. In **Azure AI Studio**, copy the **Document** value.
1. Scroll down to **<this_is_another_document_source>** and paste your document value.
1. From the integrated terminal for your file, run the program, eg:

    - `.\prompt-shield.py`

1. Validate that an attack is detected.
1. Optionally, you can experiment with different test content and document values.
