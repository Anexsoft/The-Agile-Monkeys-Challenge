# The Agile Monkeys Challenge - The CRM Service
The CRM service is an API for customers' management.

## Architecture
* The CRM service is a RESTFul API that implements a CQRS architecture pattern to separate concerns from business logic.
* Command and mediator patterns were implemented to make easy unit tests.
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
- [ ] Continuous Deployment
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

### 1. Clonse the project
Clonse this repo before to start.

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

### 5. Endpoints
You can check our endpoints documentation on http://localhost:50000/swagger after run the solution.

¡Happy Code 💪!