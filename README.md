# The Agile Monkeys Challenge

## The CRM Service
The CRM service is an API for customers' management.

### Architecture
* The CRM service is a RESTFul API that implements a CQRS architecture pattern to separate concerns from business logic.
* Command pattern and mediator were implemented to make easy unit tests.
* Persistence data layer uses Entity Framework Core.

### Requirements
- [x] List of customers
- [x] Get a full customer information
- [x] Create a new customer using validation criteria and reference who created it
- [x] Update a customer using validation criteria and reference who updated it
- [x] Delete a customer
- [] Upload an image
- [] Authorization & Authentication
- [x] SQL Injection
- [X] XSS Prevention

#### Extra requeriments
- [] Unit test
- [] OAuth
- [] Continuous Deployment
- [] Docker/Vagrant or similar

#### Notes
* Generic paging was added to list whatever.
* Automatic audit was implemented to avoid a manually codification from our developers.
* Soft delete was implemented to avoid a real delete from the database.

## Newcomers: ¡How to start!
ToDo ..