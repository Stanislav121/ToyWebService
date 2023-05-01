FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ToyWebService/ToyWebService.csproj", "ToyWebService/"]
RUN dotnet restore "ToyWebService/ToyWebService.csproj"
COPY . .
WORKDIR "/src/ToyWebService"
RUN dotnet build "ToyWebService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToyWebService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToyWebService.dll"]
