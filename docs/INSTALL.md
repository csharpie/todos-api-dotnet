# Getting Started Locally

## 🔧 Prerequisites
- .NET 9 SDK
- A Firestore project, a database and service account credentials

## 📦 Clone the Repository
```bash
git clone https://github.com/csharpie/todos-api.git
cd todos-api
```
## 🔐 Configure Secrets
This API uses dotnet user-secrets to securely store the JWT encryption key during development.

### Step 1: Initialize user-secrets
```bash
dotnet user-secrets init
```
This adds a `UserSecretsId` to your .csproj file.

### Step 2: Set the encryption key
```bash
dotnet user-secrets set "AppSettings:EncryptionKey" "SUPER_SECRET_KEY"
```
> Replace "SUPER_SECRET_KEY" with the key that is generated in your terminal. This key is used to sign JWT tokens.

## 🔥 Configure Firestore
1. Create a Firestore project in <a href="https://console.cloud.google.com/" target="_blank">Google Cloud Console</a>.
2. Follow <a href="https://cloud.google.com/firestore/native/docs/manage-databases" target="_blank">these instructions</a> for creating the database the and collection.
2. Generate a service account key and save the JSON file.
3. Set the `GOOGLE_APPLICATION_CREDENTIALS` environment variable:
### Instructions for MacOS or Linux
```bash
export GOOGLE_APPLICATION_CREDENTIALS="/path/to/your/service-account.json"
```

### Instructions for Windows (*run as Adminstrator*)
```pwsh
$ENV:GOOGLE_APPLICATION_CREDENTIALS = "/path/to/your/service-account.json"
```

> Refresh your shell or, if you are opening this in an IDE (Integrated Development Environment) such as Visual Studio or JetBrains Rider [or a text editor such as Visual Studio Code], close it and reopen the project within it.

This allows the API to authenticate with Firestore.

4. Set the `TODO_API_FIRESTOREDB_PROJECT` environment variable:
### Instructions for MacOS or Linux
```bash
export TODO_API_FIRESTOREDB_PROJECT="your_firestoredb_project_id"
```

### Instructions for Windows (*run as Administrator*)
```pwsh
$ENV:TODO_API_FIRESTOREDB_PROJECT = "your_firestoredb_project_id"
```

> Refresh your shell or, if you are opening this in an IDE (Integrated Development Environment) such as Visual Studio or JetBrains Rider [or a text editor such as Visual Studio Code], close it and reopen the project within it.

## 🔐 Additional Environment Variables
In launchSetting.json, configure the following environment variables needed to authorize the JWT in each profile you have configured.
```json
{
 ...,
 "environmentVariables": {
    "ASPNETCORE_ENVIRONMENT": "Development",
    "AuthorizedUser": "SUPER_SECRET_USER_NAME",
    "AuthorizedUserPassword": "SUPER_SECRET_PASSWORD"
  }
}
```
> When deploying live, the *AuthorizedUser* and *AuthorizedUserPassword* should be stored as System Environment variables and the *AuthorizedUserPassword* should be masked.

## ▶️ Run the API
```bash
dotnet run
```