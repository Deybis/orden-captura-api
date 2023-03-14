#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Seje.OrdenCaptura.Api/Seje.OrdenCaptura.Api.csproj", "Seje.OrdenCaptura.Api/"]
COPY ["Entities.Shared/Entities.Shared.csproj", "Entities.Shared/"]
COPY ["MementoFX.Messaging.Rebus/MementoFX.Messaging.Rebus.csproj", "MementoFX.Messaging.Rebus/"]
COPY ["MementoFX.Persistence.SqlServer/MementoFX.Persistence.SqlServer.csproj", "MementoFX.Persistence.SqlServer/"]
COPY ["Seje.Authorization.Service/Seje.Authorization.Service.csproj", "Seje.Authorization.Service/"]
COPY ["Seje.Expediente.Client/Seje.Expediente.Client.csproj", "Seje.Expediente.Client/"]
COPY ["Seje.FileManager.Client/Seje.FileManager.Client.csproj", "Seje.FileManager.Client/"]
COPY ["Seje.Firma.Client/Seje.Firma.Client.csproj", "Seje.Firma.Client/"]
COPY ["Seje.Services.Identity/Seje.Services.Identity.csproj", "Seje.Services.Identity/"]
COPY ["Seje.OrdenCaptura.SharedKernel/Seje.OrdenCaptura.SharedKernel.csproj", "Seje.OrdenCaptura.SharedKernel/"]
COPY ["Seje.OrdenCaptura.QueryStack/Seje.OrdenCaptura.QueryStack.csproj", "Seje.OrdenCaptura.QueryStack/"]
COPY ["Seje.OrdenCaptura.CommandStack/Seje.OrdenCaptura.CommandStack.csproj", "Seje.OrdenCaptura.CommandStack/"]

RUN dotnet restore "Seje.OrdenCaptura.Api/Seje.OrdenCaptura.Api.csproj"
COPY . .
WORKDIR "/src/Seje.OrdenCaptura.Api"
RUN dotnet build "Seje.OrdenCaptura.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Seje.OrdenCaptura.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Seje.OrdenCaptura.Api.dll"]