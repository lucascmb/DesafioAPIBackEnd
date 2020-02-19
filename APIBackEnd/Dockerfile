FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["APIBackEnd/APIBackEnd.csproj", "APIBackEnd/"]
RUN dotnet restore "APIBackEnd/APIBackEnd.csproj"
COPY . .
WORKDIR "/src/APIBackEnd"
RUN dotnet build "APIBackEnd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIBackEnd.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIBackEnd.dll"]