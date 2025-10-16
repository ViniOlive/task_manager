# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de solução e projetos
COPY TaskManager.sln ./
COPY TaskManager.Api/ TaskManager.Api/
COPY TaskManager.Application/ TaskManager.Application/
COPY TaskManager.Domain/ TaskManager.Domain/
COPY TaskManager.Infrastructure/ TaskManager.Infrastructure/

# Restaura dependências e publica o app
RUN dotnet restore
RUN dotnet publish TaskManager.Api/TaskManager.Api.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Define porta e variáveis
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "TaskManager.Api.dll"]
