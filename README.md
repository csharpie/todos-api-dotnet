# 📝 Todos API — C# Web API for Tutorials

This is a simple C# Web API built with ASP.NET Core 9, designed as the foundation for a series of tutorials I’ll be writing and/or recording. It demonstrates how to build a secure, cloud-hosted API with JWT authentication and Firestore as a lightweight JSON datastore.

---

## 🚀 Hosting & Storage

- **Cloud Hosting**: The API will be deployed to [Vercel](https://vercel.com), leveraging its simplicity for hosting serverless endpoints.
- **Datastore**: JSON data is stored in [Google Firestore](https://firebase.google.com/docs/firestore), chosen for its ease of use and flexible document model.

---

## 🔨 Getting Started Locally
Follow the tutorial <a href="docs/INSTALL.md" target="_blank">here</a>.

---

## 🧩 Data Model

This API is based on a simple table structure in a ServiceNow application (TODO: create this app and table) called `Todo`. The table will include:

| Field        | Type     | Required | Description                                      |
|--------------|----------|----------|--------------------------------------------------|
| `SysId`      | `string` | ✅        | Unique identifier from ServiceNow               |
| `Description`| `string` | ✅        |Task at hand - *Max length: 255 characters*                      |
| `Completed`  | `bool`   | ✅        | Indicates whether the task is completed         |

---

## 🔐 Authentication

JWT (JSON Web Token) is used to secure the API. Clients must include a valid token in the `Authorization` header for protected endpoints.

### 🔑 LoginController

- `POST /login`  
  Accepts a payload with a valid username and password.  
  Returns a JWT token if credentials are valid.

---

## 📋 TodoController

### `GET /todos`  
Returns a JSON array of all Todo items stored in the Firestore `todos` collection.  
Requires a valid JWT token.

### `POST /todos`  
Creates a new Todo item.  
Payload must include:

```json
{
  "sysId": "string",
  "description": "string (max 255)",
  "completed": true | false
}
```

## References that helped me along the way
<a href="https://dev.to/eduardstefanescu/jwt-authentication-with-symmetric-encryption-in-asp-net-core-2i53" target="_blank">JWT Authentication with Symmetric Encryption in ASP.NET Core by Eduard Stefanescu</a>
