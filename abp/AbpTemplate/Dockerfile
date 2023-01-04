FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["abp/AbpTemplate/AbpTemplate.csproj", "abp/AbpTemplate/"]
RUN dotnet restore "abp/AbpTemplate/AbpTemplate.csproj"
COPY . .
WORKDIR "/src/abp/AbpTemplate"
RUN dotnet build "AbpTemplate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AbpTemplate.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AbpTemplate.dll"]
