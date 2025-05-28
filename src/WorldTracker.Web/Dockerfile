# Fase base com ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de build do React
FROM node:20-alpine AS node-build

ARG VITE_BASE_URL

WORKDIR /app
COPY WorldTracker.Web/wwwroot/package*.json ./
RUN npm install
COPY WorldTracker.Web/wwwroot ./

RUN echo "window.env = { VITE_BASE_URL: \"$VITE_BASE_URL\" };" > ./public/env-config.js

RUN npm run build

# Fase para build do backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WorldTracker.Web/WorldTracker.Web.csproj", "WorldTracker.Web/"]
COPY ["WorldTracker.Common/WorldTracker.Common.csproj", "WorldTracker.Common/"]
COPY ["WorldTracker.Domain/WorldTracker.Domain.csproj", "WorldTracker.Domain/"]
COPY ["WorldTracker.Infra.Database/WorldTracker.Infra.Database.csproj", "WorldTracker.Infra.Database/"]
COPY ["WorldTracker.Infra/WorldTracker.Infra.csproj", "WorldTracker.Infra/"]
RUN dotnet restore "WorldTracker.Web/WorldTracker.Web.csproj"
COPY . .
WORKDIR "/src/WorldTracker.Web"

# Substitui o conteúdo original da wwwroot pelo build do React
RUN rm -rf ./wwwroot
COPY --from=node-build /app/dist ./wwwroot

RUN dotnet build "WorldTracker.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WorldTracker.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorldTracker.Web.dll"]
