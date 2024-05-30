FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
COPY . /source
WORKDIR /source
RUN dotnet restore
WORKDIR /source/
RUN dotnet publish -c Release -o /app --no-restore
RUN ls -lah /app/

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build /app ./

RUN ls -lah /app/

ENV ASPNETCORE_ENVIRONMENT="Development"


ENTRYPOINT ["dotnet", "ERP.Server.dll"]

EXPOSE 80
