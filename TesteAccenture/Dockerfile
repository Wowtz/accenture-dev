# Use a imagem oficial do ASP.NET Core como imagem base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use a imagem oficial do SDK do ASP.NET Core para construir o aplicativo
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TesteAccenture/TesteAccenture.csproj", "TesteAccenture/"]
RUN dotnet restore "TesteAccenture/TesteAccenture.csproj"
COPY . .
WORKDIR "/src/TesteAccenture"
RUN dotnet build "TesteAccenture.csproj" -c Release -o /app/build

# Publicar o aplicativo
FROM build AS publish
RUN dotnet publish "TesteAccenture.csproj" -c Release -o /app/publish

# Configurar a imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TesteAccenture.dll"]
