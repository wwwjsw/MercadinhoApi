#!/bin/bash

echo "Parando containers existentes..."
docker stop market-api sqlserver || true
docker rm market-api sqlserver || true

echo "Iniciando SQL Server..."
docker run -d --name sqlserver \
  -e "SA_PASSWORD=Password123!" \
  -e "ACCEPT_EULA=Y" \
  -p 1433:1433 \
  mcr.microsoft.com/mssql/server:2019-latest

echo "Aguardando SQL Server iniciar (30 segundos)..."
sleep 30

echo "Build da aplicação..."
docker build -t market-crud .

echo "Iniciando aplicação na porta 5000..."
docker run -d --name market-api \
  -p 5000:8080 \
  --link sqlserver \
  -e "ConnectionStrings__DefaultConnection=Server=sqlserver;Database=MarketDB;User Id=sa;Password=Password123!;TrustServerCertificate=true;" \
  -e "ASPNETCORE_URLS=http://*:8080" \
  market-crud

echo "Aguardando aplicação iniciar (10 segundos)..."
sleep 10

echo "Aplicação iniciada em http://localhost:5000"
echo "Para ver os logs: docker logs market-api"
echo "Para testar: curl http://localhost:5000/api/itensmercado"