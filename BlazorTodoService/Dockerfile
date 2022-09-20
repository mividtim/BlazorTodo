FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazorTodoService/BlazorTodoService.csproj", "BlazorTodoService/"]
RUN dotnet restore "BlazorTodoService/BlazorTodoService.csproj"
COPY . .
WORKDIR "/src/BlazorTodoService"
RUN dotnet build "BlazorTodoService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorTodoService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorTodoService.dll"]
