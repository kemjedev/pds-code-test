# UK Parliament Code Test

## Prerequisites
- Visual Studio 2022
- .NET 8 SDK

## Project Setup

1. Clone the repository:
   
2. Open the solution in Visual Studio.

3. Set up multiple startup projects:
   - Right-click on the solution in the Solution Explorer and select **Properties**.
   - In the Solution Properties window, select **Common Properties > Startup Project**.
   - Choose the **Multiple startup projects** option.
   - Set the Action for both the web app and the API app to **Start**.

4. Ensure the `launchSettings.json` files for both the web app and the API app are configured correctly. These files should already be included in the repository.

5. Click the green Play button to start both the web app and the API app.

## Configuring the Web App to Use the API App's URI

The web app is configured to use the API app's URI as its HTTP client base URL. This configuration is done in the `appsettings.json` file:


In the `Program.cs` file, the `HttpClient` is configured to use this base URL:
{ "ApiSettings": { "BaseUrl": "https://localhost:7167/" } }



