## Running the Dapper Application

### Step 1: Download the Project

- Visit the project's GitHub repository or download the project ZIP file.

### Step 2: Extract Project Files

- If you downloaded a ZIP file, extract its contents.
- Move the extracted project folder to your desired location.

### Step 3: Open Visual Studio

- Open Visual Studio (ensure you have a compatible version installed, e.g., Visual Studio 2021).

### Step 4: Open Solution File

- In Visual Studio, open the solution file (usually a file with a `.sln` extension) located in the project's root folder.

### Step 5: Install Dapper NuGet Package

- In the Solution Explorer, right-click on the project and select "Manage NuGet Packages."
- Go to the "Browse" tab, search for "Dapper," and install the latest version.

### Step 6: Create appsettings.json File

- If the project doesn't have one, create an `appsettings.json` file in the project directory.
- Add a connection string for your database, replacing placeholders with your actual database details.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=YourDatabase;User=YourUser;Password=YourPassword;"
  }
}
```

### Step 7: Configure Database Connection

- In your application code, configure the database connection using the connection string from `appsettings.json`.

### Step 8: Build the Project

- Build the project to ensure NuGet packages are downloaded and the project compiles successfully.
  - Click on "Build" in the top menu and select "Build Solution."

### Step 9: Run the Application

- Run the application to check if everything is set up correctly.
  - Press `F5` or click on the "Start Debugging" button (green play button) in the toolbar.

