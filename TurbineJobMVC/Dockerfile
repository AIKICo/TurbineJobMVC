FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Development
COPY *.sln .
COPY TurbineJobMVC/*.csproj ./TurbineJobMVC/
RUN dotnet restore -s https://api.nuget.org/v3/index.json -s https://www.myget.org/F/my/api/v3/index.json

COPY TurbineJobMVC/. ./TurbineJobMVC/
WORKDIR /app/TurbineJobMVC
RUN dotnet publish -c Release -o out --self-contained false

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
RUN apt-get update && apt-get install -y vim apt-utils libgdiplus libc6-dev tzdata
RUN export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENV TZ=Asia/Tehran
ENV DEBIAN_FRONTEND=noninteractive
ENV ASPNETCORE_ENVIRONMENT=Development
RUN mkdir -p /etc/keys
WORKDIR /app
COPY --from=build /app/TurbineJobMVC/out ./
CMD ASPNETCORE_URLS=http://*:$PORT dotnet TurbineJobMVC.dll

# ENTRYPOINT ["dotnet", "TurbineJobMVC.dll"]

