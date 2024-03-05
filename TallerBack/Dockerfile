FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TallerApi/TallerApi.csproj", "TallerApi"]
COPY ["TestTaller/TestTaller.csproj", "TestTaller"]
RUN dotnet restore "TallerApi/TallerApi.csproj"
COPY . .
WORKDIR "/src/TallerApi"
RUN dotnet build "TallerApi/TallerApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet build "TallerApi/TallerApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TallerApi"]
