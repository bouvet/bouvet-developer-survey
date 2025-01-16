
# Bouvet Developer Survey Backend

## Setting up db
The testing and production enviroment uses a Azure Database, but for local development you can use a local database.
For creating a local db and connecting to it, follow these steps:

1. Open cmd and run the following command on this path: `C:\Windows\System32>
    * `sqllocaldb create "Local"`

2. Open Microsoft SQL Server Management Studio and connect to the local db:
    * Server name: (localdb)\Local
    * Authentication: Windows Authentication

3. Create a new database called whatever you want, but for this example we will call it BouvetDeveloperSurvey.

In the secrets.json, add the following connection string:
```json
{
  "ConnectionString": "Server=(localdb)\\Local;Initial Catalog=BouvetDeveloperSurvey;Integrated Security=True"
}
```

Then you can run the api, the database will be connected and migrations to create the tables will be run automatically.

## Connect to Azure OpenAI

The key vault has not been connected to the project yet, but the secrets to connect to the Azure OpenAI API should be stored in the secrets.json file. The OpenAiUrl and OpenAiKey should be stored in the secrets.json file.

```json
{
  "OpenAiUrl": "url",
  "OpenAiSecretKey": "key"
}
```

---

## Exporting the survey from Qualtrics

First of all, we will have to go to https://www.qualtrics.com/ where the survey is located.
We will have to export the survey structure and responses to a file.
* The structure file will be in the format of a .json file.
* The responses file will be in the format of a .csv file.

In order to export the survey structure, go to the survey you want to export and on the Survey tab, click the dropdown menu Tools and then Import/Export. And then click on Export Survey.

![img_6.png](img_6.png)

When the export is done, we will have a .qsf file that are a json file with the survey structure.

In order to export the survey responses, go to the survey you want to export and on the Data & Analysis tab.

![img_7.png](img_7.png)

There is a dropdown menu called Export Data. Click on it and then click on Export Data.

![img_8.png](img_8.png)

Then, use this export functionality to export the responses to a .csv file in the correct format.
It is important to use numeric values, as the .csv file does not provide data on multiple-choice questions if there are choice texts marked as export values!

![img_9.png](img_9.png)

---

## Endpoints

### Importing survey structure

For importing the survey structure with no responses, use the following endpoint:
![img_1.png](img_1.png)

For importing the survey responses, use the following endpoint:
![img_2.png](img_2.png)

### Retrieving survey results

For retrieving the surveys that have been imported, use the following endpoint:

![img_3.png](img_3.png)

With the following response body:

```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "string",
    "surveyId": "string",
    "createdAt": "2024-12-06T09:02:44.838Z",
    "updatedAt": "2024-12-06T09:02:44.838Z",
    "lastSyncedAt": "2024-12-06T09:02:44.838Z"
  }
]
```

For retrieving the survey structure with questions use the following endpoint:

![img_4.png](img_4.png)

Where the parameter surveyId is the Guid of the survey you want to retrieve the structure for. The response body will look like this:

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string",
  "surveyId": "string",
  "createdAt": "2024-12-06T09:19:25.516Z",
  "updatedAt": "2024-12-06T09:19:25.516Z",
  "lastSyncedAt": "2024-12-06T09:19:25.516Z",
  "surveyBlocks": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "surveyId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "description": "string",
      "createdAt": "2024-12-06T09:19:25.516Z",
      "updatedAt": "2024-12-06T09:19:25.516Z",
      "blockElements": [
        {
          "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "type": "string",
          "createdAt": "2024-12-06T09:19:25.516Z",
          "updatedAt": "2024-12-06T09:19:25.516Z",
          "questions": [
            {
              "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
              "blockElementId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
              "dataExportTag": "string",
              "questionText": "string",
              "questionDescription": "string",
              "isMultipleChoice": true,
              "createdAt": "2024-12-06T09:19:25.516Z",
              "updatedAt": "2024-12-06T09:19:25.516Z"
            }
          ]
        }
      ]
    }
  ]
}
```

And finally to retrieve the survey responses, use the following endpoint:
![img_5.png](img_5.png)

Where the parameter questionId is the Guid of the Question you want to retrieve the responses for. The response body will look like this:

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "dataExportTag": "string",
  "aiAnalyse": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "text": "string"
  },
  "questionText": "string",
  "questionDescription": "string",
  "isMultipleChoice": true,
  "createdAt": "2024-12-06T09:20:49.212Z",
  "updatedAt": "2024-12-06T09:20:49.212Z",
  "choices": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "text": "string",
      "indexId": "string",
      "createdAt": "2024-12-06T09:20:49.212Z",
      "updatedAt": "2024-12-06T09:20:49.212Z",
      "statistics": {
        "totalPercentage": 0,
        "admiredPercentage": 0,
        "desiredPercentage": 0
      }
    }
  ]
}
```
