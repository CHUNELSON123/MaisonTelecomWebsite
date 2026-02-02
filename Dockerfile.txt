# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MaisonTelecom.csproj", "./"]
RUN dotnet restore "MaisonTelecom.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MaisonTelecom.csproj" -c Release -o /app/build

# 2. Publish Stage
FROM build AS publish
RUN dotnet publish "MaisonTelecom.csproj" -c Release -o /app/publish

# 3. Final Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MaisonTelecom.dll"]