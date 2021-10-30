

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 1234
EXPOSE 1235
#asp change ports from 5000 and 5001 to 80
#docker run -p 1234:80 mvc

#ENV ASPNETCORE_URLS=http://+:5000
#ENV ASPNETCORE_URLS=https://+:5001
# if i want to change port, default 80 and 443 for http nad https

 
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["warehouse/warehouse.csproj", "warehouse/"]
RUN dotnet restore "warehouse/warehouse.csproj"
COPY . .
WORKDIR "/src/warehouse"
RUN dotnet build "warehouse.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "warehouse.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "warehouse.dll"]