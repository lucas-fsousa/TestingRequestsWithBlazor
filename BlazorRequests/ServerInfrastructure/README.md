# Generic Project
## What is the project...?

The class library contains standard and generic definitions of a project with database integration using SQL Server and can be easily integrated into large projects to streamline common and repetitive tasks.

### Integration of the library into your project
In the project's Startup class, find the **"ConfigureServices"** method and add the reference:
- ExtensionConfigureServices(Configuration);

It is important to point out that when adding the reference to the method extension, the **configuration** variable that was defined as read-only must be informed, otherwise an exception will be generated for the injection of dependencies of the IGenericRepository interface.

### Connection string definition

After integrating the library with the application, the connection string inclusion method will be available in **ServiceConfiguration**. To connect the application to the database, go to your application's appsettings.json, then create a json object as shown in the example.

- "ConnectionStrings": {
    "DefaultConnection": "**enter your connection string here**"
  }


After finishing the connection string insertion process, just add a project reference and select the class library and the integration is complete.