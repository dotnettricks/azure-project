# DotNetTricks Azure Project

Tasks To DO:
1. #Task: Create SQL Azure to Save Data.
2. #Task: Create CosmosDB to Save Cart and Received Orders Data.
3. #Task: Create Azure KeyVault to Save Credentials.
4. #Task: Integrate Application Insights to monitor application.
6. #Task: Create Azure Function to Send Email Upon SignUp and Payment Confirmation.
7. #Task: Create Service Bus to notify to Email service (Azure Function) for sending email for SignUp and Payment Confirmation.
8. #Task: Create a Service To Manage Images to Azure Storage.
9. #Task: Create a Azure Redis Cache to Cache Items List.
10. #Task: Add a Global Search to search Items using Azure Search.
11. #Task: Implement OAuth Login Using Azure AD.
12. #Task: Create a CI/CD Pipelines using Azure DevOps for Build and Release.


Note: 
1. To Use the Azure Functions for sending Email Upon SignUp and Payment Confirmation, Add the following to your local.setting.json or Azure function configuration(creating variables as the names suggest) depending on from where we are running our function app and also *pre-requisite of creating the ServiceBus Queues named as -> 'emailqueue' and 'paymentqueue':

"Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "smtp": "smtp-mail.outlook.com",
    "port": 587,
    "email": "your email id",
    "password": "password",
    "ServiceBusConnection": "paste your access key connection from the portal"
  }
  
  2. Similarly for storing the image to Azure we need to have a storage account created at the azure portal and acces key to be put in the appsettings.json
  3. For Redis Cache we need to create an Azure Cache for Redis in the portal and use the key for the same in user secrets.
  4. Also, for Redis Cache the defualt time for which the cache will be persisted is 5 mins to change the same pass parameter to set the absoluteExpirationTime
