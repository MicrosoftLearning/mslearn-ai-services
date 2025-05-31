---
lab:
    title: 'Get started with Azure AI Services'
    module: 'Module 4 - Monitor Azure AI services'
    exercise: 'Monitor Azure AI services'
---

# Monitor Azure AI Services

Azure AI Services can be a critical part of an overall application infrastructure. It's important to be able to monitor activity and get alerted to issues that may need attention.

## Clone the repository in Visual Studio Code

You'll develop your code using Visual Studio Code. The code files for your app have been provided in a GitHub repo.

> **Tip**: If you have already cloned the **mslearn-ai-services** repo, open it in Visual Studio code. Otherwise, follow these steps to clone it to your development environment.

1. Start Visual Studio Code.
2. Open the palette (SHIFT+CTRL+P) and run a **Git: Clone** command to clone the `https://github.com/MicrosoftLearning/mslearn-ai-services` repository to a local folder (it doesn't matter which folder).
3. When the repository has been cloned, open the folder in Visual Studio Code.
4. Wait while additional files are installed to support the C# code projects in the repo, if necessary

    > **Note**: If you are prompted to add required assets to build and debug, select **Not Now**.

5. Expand the `Labfiles/03-monitor-ai-services` folder.

## Provision an Azure AI Services resource

If you don't already have one in your subscription, you'll need to provision an **Azure AI Services** resource.

1. Open the Azure portal at `https://portal.azure.com`, and sign in using the Microsoft account associated with your Azure subscription.
2. In the top search bar, search for *Azure AI services*, select **Azure AI Services**, and create an Azure AI services multi-service account resource with the following settings:
    - **Subscription**: *Your Azure subscription*
    - **Resource group**: *Choose or create a resource group (if you are using a restricted subscription, you may not have permission to create a new resource group - use the one provided)*
    - **Region**: *Choose any available region*
    - **Name**: *Enter a unique name*
    - **Pricing tier**: Standard S0
3. Select any required checkboxes and create the resource.
4. Wait for deployment to complete, and then view the deployment details.
5. When the resource has been deployed, go to it and view its **Keys and Endpoint** page. Make a note of the endpoint URI - you will need it later.

## Configure an alert

Let's start monitoring by defining an alert rule so you can detect activity in your Azure AI services resource.

1. In the Azure portal, go to your Azure AI services resource and view its **Alerts** page (in the **Monitoring** section).
2. Select **+ Create** dropdown, and select **Alert rule**
3. In the **Create an alert rule** page, under **Scope**, verify that the your Azure AI services resource is listed. (Close **Select a signal** pane if open)
4. Select **Condition** tab, and select on the **See all signals** link to show the **Select a signal** pane that appears on the right, where you can select a signal type to monitor.
5. In the **Signal type** list, scroll down to the **Activity Log** section, and then select **List Keys (Cognitive Services API Account)**. Then select **Apply**.
6. Review the activity over the past 6 hours.
7. Select the **Actions** tab. Note that you can specify an *action group*. This enables you to configure automated actions when an alert is fired - for example, sending an email notification. We won't do that in this exercise; but it can be useful to do this in a production environment.
8. In the **Details** tab, set the **Alert rule name** to **Key List Alert**.
9. Select **Review + create**. 
10. Review the configuration for the alert. Select **Create** and wait for the alert rule to be created.
11. Now you can use the following command to get the list of Azure AI services keys, replacing *&lt;resourceName&gt;* with the name of your Azure AI services resource, and *&lt;resourceGroup&gt;* with the name of the resource group in which you created it.

    ```
    az cognitiveservices account keys list --name <resourceName> --resource-group <resourceGroup>
    ```

    The command returns a list of the keys for your Azure AI services resource.

    > **Note** If you haven't logged into Azure CLI, you may need to run `az login` before the list keys command will work.

12. Switch back to the browser containing the Azure portal, and refresh your **Alerts page**. You should see a **Sev 4** alert listed in the table (if not, wait up to five minutes and refresh again).
13. Select the alert to see its details.

## Visualize a metric

As well as defining alerts, you can view metrics for your Azure AI services resource to monitor its utilization.

1. In the Azure portal, in the page for your Azure AI services resource, select **Metrics** (in the **Monitoring** section).
2. If there is no existing chart, select **+ New chart**. Then in the **Metric** list, review the possible metrics you can visualize and select **Total Calls**.
3. In the **Aggregation** list, select **Count**.  This will enable you to monitor the total calls to you Azure AI Service resource; which is useful in determining how much the service is being used over a period of time.
4. To generate some requests to your Azure AI service, you will use **curl** - a command line tool for HTTP requests. In your editor, open **rest-test.cmd** and edit the **curl** command it contains (shown below), replacing *&lt;yourEndpoint&gt;* and *&lt;yourKey&gt;* with your endpoint URI and **Key1** key to use the Text Analytics API in your Azure AI services resource.

    ```
    curl -X POST "<your-endpoint>/language/:analyze-text?api-version=2023-04-01" -H "Content-Type: application/json" -H "Ocp-Apim-Subscription-Key: <your-key>" --data-ascii "{'analysisInput':{'documents':[{'id':1,'text':'hello'}]}, 'kind': 'LanguageDetection'}"
    ```

5. Save your changes and then run the following command:

    ```
    ./rest-test.cmd
    ```

    The command returns a JSON document containing information about the language detected in the input data (which should be English).

6. Re-run the **rest-test** command multiple times to generate some call activity (you can use the **^** key to cycle through previous commands).
7. Return to the **Metrics** page in the Azure portal and refresh the **Total Calls** count chart. It may take a few minutes for the calls you made using *curl* to be reflected in the chart - keep refreshing the chart until it updates to include them.

## Clean up resources

If you're not using the Azure resources created in this lab for other training modules, you can delete them to avoid incurring further charges.

1. Open the Azure portal at `https://portal.azure.com`, and in the top search bar, search for the resources you created in this lab.

2. On the resource page, select **Delete** and follow the instructions to delete the resource. Alternatively, you can delete the entire resource group to clean up all resources at the same time.

## More information

One of the options for monitoring Azure AI services is to use *diagnostic logging*. Once enabled, diagnostic logging captures rich information about your Azure AI services resource usage, and can be a useful monitoring and debugging tool. It can take over an hour after setting up diagnostic logging to generate any information, which is why we haven't explored it in this exercise; but you can learn more about it in the [Azure AI Services documentation](https://docs.microsoft.com/azure/ai-services/diagnostic-logging).
