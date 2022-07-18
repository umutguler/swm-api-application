# swm-api-application
# How to Run the App

## Requirements

Make sure you have:

-   [dotnet cli](https://dotnet.microsoft.com/download) - I've used the latest .NET 6.
-   You may need to run a dotnet restore for packages to install.
## Installation


## Usage

You can use VSCode, it has a launch.json ready for you to pick a profile.
Or Visual Studio IDE, also has profiles to launch in different modes.

## Option #1 (Simplest) - Run in Visual Studio IDE
1. Open VS 2019 or 2022 IDE
2. Select Clone a Repo https://github.com/umutguler/swm-api-application
3. Click on Build --> Build Solution
4. Select your mode to debug in and run it
5. You can run tests in the Test tab/test explorer

## Option #2 - To Run VSCode
1. To run in VSCode make sure you open the swm-api-application as the folder. (Default Shortcut: CTRL + SHIFT + O)
2. Click on "Run & Debug" on the side
3. Select a mode to debug in (standard runs once, watch stays running for and watches for file changes).

## Option #3 - CLI for Those Terminal Lovers
## Step 1. Download, Restore & Build Code
```bash
git checkout https://github.com/umutguler/swm-api-application
cd swm-api-application
dotnet restore; dotnet build;
```

## Step 2. Run in CLI
Make sure the terminal working directory is in swm-api-application
```bash
dotnet run .\Swm.Api.Business\Swm.Api.Business.csproj
```
## Step 3. Run Tests in CLI
```bash
dotnet test -l "console;verbosity=normal"
```
