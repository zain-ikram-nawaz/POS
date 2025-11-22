#!/bin/bash
# Build the project
dotnet publish BBS_POS/BBS_POS.csproj -c Release -o ./publish


dotnet ./publish/BBS_POS.dll