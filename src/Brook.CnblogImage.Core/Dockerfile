FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY Brook.CnblogImage.Core/Brook.CnblogImage.Core.csproj Brook.CnblogImage.Core/
RUN dotnet restore "Brook.CnblogImage.Core/Brook.CnblogImage.Core.csproj"
COPY . .
WORKDIR "/src/Brook.CnblogImage.Core"
RUN dotnet build "Brook.CnblogImage.Core.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Brook.CnblogImage.Core.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Brook.CnblogImage.Core.dll"]
