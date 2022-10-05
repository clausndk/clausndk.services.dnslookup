#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["clausndk.services.dnslookup.web/clausndk.services.dnslookup.web.csproj", "clausndk.services.dnslookup.web/"]
RUN dotnet restore "clausndk.services.dnslookup.web/clausndk.services.dnslookup.web.csproj"
COPY . .
WORKDIR "/src/clausndk.services.dnslookup.web"
RUN dotnet build "clausndk.services.dnslookup.web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "clausndk.services.dnslookup.web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "clausndk.services.dnslookup.web.dll"]