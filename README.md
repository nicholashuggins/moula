# moula


Points on Architecture
Separation of APIController and business logic via a Payment Service which talks to a separate datalayer
Use of interface and DI to enable replacement of Payment service implementation for extensibility.
Asynchronous calls provide for load along with the use of stored procedures for the 'heavier' queries.
Repository pattern provides separation of the ORM allowing easy upgrade of EF or complete replacement.


Testing
Unit test coverage of Use Case Scenarios, I opted to use a database connection rather than mocking so as to provide more realisting testing.
End to End testing done in Postman - export of tests provided -> Moula\PostmanTests

Configuration
Separate Configurations for Development Staging and Production via launchSsettings.json
Configured 3 publish scripts for Development Staging and Production -> Moula\Moula\Properties\PublishProfiles.

