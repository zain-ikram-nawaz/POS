#!/bin/bash
# Build the project
dotnet publish BBS_POS/BBS_POS.csproj -c Release -o ./publish

# Run the published application
# Replace 'BBS_POS.dll' with your actual DLL name
dotnet ./publish/BBS_POS.dll