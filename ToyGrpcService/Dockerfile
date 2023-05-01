FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ToyGrpcService/ToyGrpcService.csproj", "ToyGrpcService/"]
RUN dotnet restore "ToyGrpcService/ToyGrpcService.csproj"
COPY . .
WORKDIR "/src/ToyGrpcService"
RUN dotnet build "ToyGrpcService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToyGrpcService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToyGrpcService.dll"]
