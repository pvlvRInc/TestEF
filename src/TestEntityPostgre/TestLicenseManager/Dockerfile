FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestLicenseManager/TestLicenseManager.csproj", "TestLicenseManager/"]
RUN dotnet restore "TestLicenseManager/TestLicenseManager.csproj"
COPY . .
WORKDIR "/src/TestLicenseManager"
RUN dotnet build "TestLicenseManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestLicenseManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestLicenseManager.dll"]
