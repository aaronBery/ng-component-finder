# Ng Component in template finder
Finds components in an angular app that are not in use in order to facilitate app maintenance

## Install
Dotnet core 3.1 needs to be installed on your system. Dotnet core is available on Windows, Mac OS and Linux from Microsoft
https://dotnet.microsoft.com/download

## Run
The application is run using the dotnet from the command line/terminal. You need to pass the directories of the Angular app you want to scan as an argument e.g.
`dotnet run -- /Users/joebloggs/Projects/angular-app/src`

The unused components are returned in the console as their angular selector value.
