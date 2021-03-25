FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /src
COPY *.sln .
COPY BLOGIT.Test/*.csproj BLOGIT.Test/
RUN dotnet restore BLOGIT.Test/*.csproj
COPY BLOGIT.DataAccess/*.csproj BLOGIT.DataAccess/
RUN dotnet restore BLOGIT.DataAccess/*.csproj
COPY BLOGIT.Models/*.csproj BLOGIT.Models/
RUN dotnet restore BLOGIT.Models/*.csproj
COPY BLOGIT.Commons/*.csproj BLOGIT.Commons/
RUN dotnet restore BLOGIT.Models/*.csproj
COPY BLOGIT.Repository/*.csproj BLOGIT.Repository/
RUN dotnet restore BLOGIT.Repository/*.csproj
COPY BLOGIT.UserInterface/*.csproj BLOGIT.UserInterface/
RUN dotnet restore BLOGIT.UserInterface/*.csproj
COPY . .

#Testing
FROM base AS testing
WORKDIR /src/BLOGIT.DataAccess
WORKDIR /src/BLOGIT.Models
WORKDIR /src/BLOGIT.Repository
WORKDIR /src/BLOGIT.UserInterface
WORKDIR /src/BLOGIT.Commons
RUN dotnet build
WORKDIR /src/BLOGIT.Test
RUN dotnet test

#Publishing
FROM base AS publish
WORKDIR /src/BLOGIT.UserInterface
RUN dotnet publish -c Release -o /src/publish

#Get the runtime into a folder called app
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
COPY --from=publish /src/BLOGIT.UserInterface/BLOGIT.db .
COPY --from=publish /src/BLOGIT.DataAccess/DataSeeding/Category.json .
COPY --from=publish /src/BLOGIT.DataAccess/DataSeeding/PostSeed.json .
COPY --from=publish /src/BLOGIT.DataAccess/DataSeeding/UserSeed.json .
#ENTRYPOINT ["dotnet", "BLOGIT.UserInterface.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet BLOGIT.UserInterface.dll
