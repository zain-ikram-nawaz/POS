# Stage 1: Build the application (SDK image use hoga)
# NOTE: '8.0' ko apne .NET version se badal dein (e.g., 6.0, 7.0)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files, aur dependencies restore karein
COPY *.sln .
COPY BBS_POS/*.csproj BBS_POS/
RUN dotnet restore

# Copy baaki sara code
COPY . .

# Application ko Release mode mein build aur publish karein
RUN dotnet publish "BBS_POS/BBS_POS.csproj" -c Release -o /app/publish

# ---

# Stage 2: Final Runtime Image (Chota aur sirf run karne ke liye)
# NOTE: '8.0' ko apne .NET version se badal dein
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Railway default port 8080 use karta hai
EXPOSE 8080

# Publish kiye hue files ko Stage 1 se copy karein
COPY --from=build /app/publish .

# Application run karne ke liye command set karein
# DLL ka naam Project ka naam + .dll hona chahiye.
ENTRYPOINT ["dotnet", "BBS_POS.dll"]