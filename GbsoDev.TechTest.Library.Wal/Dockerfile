#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GbsoDev.TechTest.Library.Wal/GbsoDev.TechTest.Library.Wal.csproj", "GbsoDev.TechTest.Library.Wal/"]
RUN dotnet restore "GbsoDev.TechTest.Library.Wal/GbsoDev.TechTest.Library.Wal.csproj"
COPY . .
WORKDIR "/src/GbsoDev.TechTest.Library.Wal"
RUN dotnet build "GbsoDev.TechTest.Library.Wal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GbsoDev.TechTest.Library.Wal.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GbsoDev.TechTest.Library.Wal.dll"]