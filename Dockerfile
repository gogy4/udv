FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY udv.sln .
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY udv/udv.csproj udv/

RUN dotnet restore

COPY . .

WORKDIR /source/udv
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "udv.dll"]