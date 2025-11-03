FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto
COPY ["MercadinhoApi.csproj", "./"]
RUN dotnet restore "MercadinhoApi.csproj"

# Copia o restante do código
COPY . .
WORKDIR "/src"
RUN dotnet build "MercadinhoApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MercadinhoApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MercadinhoApi.dll"]