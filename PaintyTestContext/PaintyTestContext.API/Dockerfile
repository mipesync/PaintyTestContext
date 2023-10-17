FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PaintyTestContext/PaintyTestContext.csproj", "PaintyTestContext/"]
RUN dotnet restore "PaintyTestContext/PaintyTestContext.csproj"
COPY . .
WORKDIR "/src/PaintyTestContext"
RUN dotnet build "PaintyTestContext.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaintyTestContext.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaintyTestContext.dll"]
