#!/bin/bash

# 1. Dependencies Restore (Zaroori hai)
echo "Restoring NuGet packages..."
dotnet restore BBS_POS/BBS_POS.csproj

# 2. Project Publish (Build)
echo "Starting .NET Application Publish..."
dotnet publish BBS_POS/BBS_POS.csproj -c Release -o ./publish

# 3. Application Run
echo "Starting the application..."
# Railway Environment Variable 'PORT' ko use karein
export ASPNETCORE_URLS="http://*:$PORT"

# DLL file ko run karein
dotnet ./publish/BBS_POS.dll