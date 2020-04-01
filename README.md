# The Agile Monkeys Challenge - The CRM Service
The CRM service is an API for customers' management.

## Architecture
* The CRM service is a RESTFul API that implements a CQRS architecture pattern to separate concerns from business logic.
* Command and mediator patterns were implemented to make easy unit tests and separate concerns.
* Persistence data layer uses Entity Framework Core.

## Requirements
- [x] Endpoints to managment users
- [x] Endpoints to managment Customers
- [X] Attach an image to Customer
- [X] Set/Remove admin role to User
- [x] Validate endpoints with Authorization & Authentication
- [x] Prevent SQL Injection
- [X] Prevent XSS

### Extra Requirements
- [X] Unit test
- [X] OAuth
- [X] Continuous Deployment
- [ ] Docker/Vagrant or similar

### Notes
* **Pagination** was added to list customer information.
* **Automatic audit** was implemented to add reference to who updated it.
* **Soft delete** was implemented to avoid a real delete from database.
* **Swagger** was added.
* **AspNetCore Identity** was added.
* OAuth and Json Web Token was implemented using **Identity Server 4**.

## Newcomers: ¡How to start!
Before to start you need to make a litle configuration in the project.

### 1. Clone the repository into your pc
Choose your folder and run the next command to clone the repository.

```
git clone https://github.com/Anexsoft/The-Agile-Monkeys-Challenge
```

### 2. Update project port
Change CRM.Api project port to 50000 as default.

### 3. Update connection string
Change the connection string located in appsetting.json for your connection string.

### 4. Run pending migrations
* Set as StartUp the CRM.Api project.
* Open Nuget Console and choose CRM.Persistence.Database project.
* Run update-database command.

### 5. Insert default user and role into the database
```
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Surname]) VALUES (N'1b5d5d2c-5d81-4c03-88c8-96585ed41d04', N'erodriguezp105@gmail.com', N'ERODRIGUEZP105@GMAIL.COM', N'erodriguezp105@gmail.com', N'ERODRIGUEZP105@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEKhBafzFgUkxOwa7thkJwXW6A5rcyazwKC+qKoySW8QLG8S2L3Kc+eCQeUHkWA0kqg==', N'ULMOVOWCNRCOC3ZKPND7R4COYJNXQS6L', N'a16ab8a4-93b4-495e-a765-a9f65d1ae5dc', NULL, 0, 0, NULL, 1, 0, N'Eduardo', N'Rodríguez Patiño')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [Discriminator]) VALUES (N'1b5d5d2c-5d81-4c03-88c8-96585ed41d04', N'25fe6bec-401c-4157-9e3e-0819fd31e68b', N'IdentityUserRole<string>')
```

#### The default user credential are:
* **Username**: erodriguezp105@gmail.com
* **Password**: 123456

**Note**: *only for development environment*.

### 6. Endpoints
You can check our endpoints documentation on http://localhost:50000/swagger after run the solution.

### 7. Create an access token
To test any endpoint you need to generate a valid access token. By default, our user inserted has an admin role and you can generate the token with the next endpoint using POSTMAN or something similar.

#### a. Endpoint
`[POST] http://localhost:50000/v1/identity/signin`

#### b. Body Parameters
```
{
	"userName": "test@admin.com",
	"password": "123456"
}
```

#### c. Expected response
Don't use this token, is only an example.

`eyJhbGciOiJSUzI1NiIsImtpZCI6IkowcTdISDJ5VFNfNTdvaUlQbHh0c0EiLCJ0eXAiOiJhdCtqd3QifQ.eyJuYmYiOjE1ODU3NTg4MDksImV4cCI6MTU4NTc2MjQwOSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwMCIsImF1ZCI6IkNSTS5BcGkiLCJjbGllbnRfaWQiOiJyby5jbGllbnQiLCJzdWIiOiI3NjViMTA2Ny04MzBmLTRmMjEtYTgwNi1lYWJmZTkwMDIzN2EiLCJhdXRoX3RpbWUiOjE1ODU3NTg4MDksImlkcCI6ImxvY2FsIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IlRlc3QgdXNlciIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3N1cm5hbWUiOiJTdXJuYW1lIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoidGVzdEBhZG1pbi5jb20iLCJzY29wZSI6WyJDUk0uQXBpIl0sImFtciI6WyJwd2QiXX0.CXS1TYnRo2QGtRIxWPkh8Boet5TEeyccFl9KMQgPbqG571f57-0dlhobbYnzWXdOal3weYYe1g1Nay1TqmL__d33HjndVc3TTG3kHFk644LtGbqrvccC1QBqgFsvUw-Lo0EEAEkULyE2RceHHMnqk0Gvrt0wcsUZjAoRb2pZVj_gPoxPnAdv6kkMMOAm6sLFYRn-No4ECcJvSqGIK88m19GD7fWs3z2MvaYWzp-PuHf8UJVB-jKfpYDY6SrdGhYyWhZiZ6xCYfVrtnlrVHrWso1SxYYCNxq1vDGGM4HQ6x4tx40FhWu9CzXj0Sn8QRVeKh6upJgWIisUXgVhbgFzrA`

¡Happy Code 💪!