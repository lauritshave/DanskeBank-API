FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY DanskeBank.csproj .
RUN dotnet restore "DanskeBank.csproj"

COPY . .
RUN dotnet publish "DanskeBank.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /publish .

ENTRYPOINT [ "dotnet", "DanskeBank.dll" ]