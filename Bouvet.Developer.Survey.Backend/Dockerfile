FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["./Bouvet.Developer.Survey.Api/Bouvet.Developer.Survey.Api.csproj", "Bouvet.Developer.Survey.Api/"]
COPY ["./Bouvet.Developer.Survey.Domain/Bouvet.Developer.Survey.Domain.csproj", "Bouvet.Developer.Survey.Domain/"]
COPY ["./Bouvet.Developer.Survey.Infrastructure/Bouvet.Developer.Survey.Infrastructure.csproj", "Bouvet.Developer.Survey.Infrastructure/"]
COPY ["./Bouvet.Developer.Survey.Service/Bouvet.Developer.Survey.Service.csproj", "Bouvet.Developer.Survey.Service/"]

RUN dotnet restore "Bouvet.Developer.Survey.Api/Bouvet.Developer.Survey.Api.csproj"

COPY . ../
WORKDIR /Bouvet.Developer.Survey.Api
RUN dotnet build "Bouvet.Developer.Survey.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_URLS=http://+:5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bouvet.Developer.Survey.Api.dll"]