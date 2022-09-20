FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazorTodoClient/BlazorTodoClient/BlazorTodoClient.csproj", "BlazorTodoClient/"]
RUN dotnet restore "BlazorTodoClient/BlazorTodoClient/BlazorTodoClient.csproj"
COPY . .
WORKDIR "/src/BlazorTodoClient"
RUN dotnet build "BlazorTodoClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorTodoClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorTodoClient.dll"]
