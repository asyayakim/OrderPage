
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src


COPY *.sln .
COPY ECommerceApp.Api/*.csproj ECommerceApp.Api/
COPY ECommerceApp.ApplicationLayer/*.csproj ECommerceApp.ApplicationLayer/
COPY ECommerceApp.Domain/*.csproj ECommerceApp.Domain/
COPY ECommerceApp.Infrastructure/*.csproj ECommerceApp.Infrastructure/
COPY ECommerce.Tests/*.csproj ECommerce.Tests/


RUN dotnet restore
COPY . .
RUN dotnet publish ECommerceApp.Api/ECommerceApp.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
COPY --from=build /app/publish .

EXPOSE 5000
ENTRYPOINT ["dotnet", "ECommerceApp.Api.dll"]
