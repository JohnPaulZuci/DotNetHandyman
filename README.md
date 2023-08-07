# DotNetHandyman
Handyman implementation in cSharp

## To get the repository clone

 > git clone https://github.com/JohnPaulZuci/DotNetHandyman.git

## To see the list of branch in the repo (default master)
 > git branch -a
 
## To change to the particular branch
 > git checkout {{branch}}


# Handyman build steps

# Project using Dotnet Core sdk version 7.0

# Url used here:  
 - https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-7.0.203-linux-x64-binaries

## Steps to configure 
sudo apt-get install antlr4

## [Execute the below command's in terminal one-by-one to install packages]
dotnet add package Antlr4.Runtime.Standard --version 4.7.2
dotnet add package Dapper --version 2.0.123
dotnet add package Newtonsoft.Json --version 13.0.3
dotnet add package Npgsql --version 7.0.4
dotnet add package Serilog --version 2.12.0
dotnet add package Serilog.Sinks.File --version 5.0.0
dotnet add package System.Data.SqlClient --version 4.8.5
dotnet add package Xunit --version 2.4.2
dotnet add package xunit.assert --version 2.4.2
dotnet add package Microsoft.NET.Test.Sdk --version 17.5.0
dotnet add package xunit.runner.visualstudio --version 2.4.5
dotnet add package MySql.Data


## Execute below commands to create antlr files
antlr4 -Dlanguage=CSharp -visitor -atn -o ./antlr/compiler/ ./antlr/gmr/Raven.g4

## Execute below commands to generate model/Action files
dotnet build /  run / restore

## Execute below commands to generate lambda files
dotnet build   
dotnet run 


# Server code build steps

# Build command
dotnet publish Server/Server.csproj --configuration Release --output ./Destination/

# Run command
dotnet run --project Server/Server.csproj 

# REFERENCES

### Refer 'CsharpDotNet1.csproj' file for detailed information

# Server folder
- Has code to trigger the resources
- Use the swagger url to call api.

# Handyman Folder
 - Orchestration flow using low code
 - Use lmd file with .lmd extension to write the logic
   
# Use solution(.sln) file to build and run the Dotnet application uisng visual studio editor

# Multiple Flavours
  - Tested the code in Visual studio editor - branch ( Windows-testing )
  - Use the Visual studio Code editor in ubuntu to run the solution - branch (master)
