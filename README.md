# Ng Component Finder
Finds components in an angular app that are not in use in order to facilitate app maintenance. Currently it is able to find components that are included in html template files or defined in routes.

## Install
Dotnet core 3.1 needs to be installed on your system. Dotnet core is available on Windows, Mac OS and Linux from Microsoft
https://dotnet.microsoft.com/download

## Run
The application is run using the dotnet from the command line/terminal. You first need to navigate to the directory containing the Program.cs file. You then need to pass the directories of the Angular app you want to scan as an argument e.g.
`dotnet run -- /Users/joebloggs/Projects/angular-app/src/`

The unused components are returned in the console as their angular selector value.
