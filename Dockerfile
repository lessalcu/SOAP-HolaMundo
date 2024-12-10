# Etapa 1: Construcción de la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar los archivos del proyecto y restaurar dependencias
COPY . ./ 
RUN dotnet restore

# Compilar la aplicación
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Imagen de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto 8000 para SOAP y 5000 para Swagger
EXPOSE 8000
EXPOSE 5000

# Iniciar la aplicación
ENTRYPOINT ["dotnet", "SOAPHolaMundo.dll"]