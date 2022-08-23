#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Certificates/-CA-2.crt", "/usr/local/share/ca-certificates/"]
RUN update-ca-certificates

COPY ["WebApiSample/WebApiSample.csproj", "WebApiSample/"]

RUN dotnet restore "WebApiSample/WebApiSample.csproj"
COPY . .
WORKDIR "/src/WebApiSample"
RUN dotnet build "WebApiSample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiSample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiSample.dll"]