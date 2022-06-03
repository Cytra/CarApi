#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarApi/CarApi.csproj", "CarApi/"]
COPY ["CarApi.Model/CarApi.Model.csproj", "CarApi.Model/"]
COPY ["CarApi.Core/CarApi.Core.csproj", "CarApi.Core/"]
COPY ["CarApi.Data/CarApi.Data.csproj", "CarApi.Data/"]
RUN dotnet restore "CarApi/CarApi.csproj"
COPY . .
WORKDIR "/src/CarApi"
RUN dotnet build "CarApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarApi.dll"]