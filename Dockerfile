# Sử dụng hình ảnh cơ sở .NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Sử dụng hình ảnh cơ sở .NET Core SDK để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj và restore các phụ thuộc
COPY ["BlogHung/BlogHung.csproj", "BlogHung/"]
RUN dotnet restore "BlogHung/BlogHung.csproj"

# Copy toàn bộ mã nguồn và build ứng dụng
COPY . .
WORKDIR "/src/BlogHung"
RUN dotnet build "BlogHung.csproj" -c Release -o /app/build

# Build runtime image
FROM build AS publish
RUN dotnet publish "BlogHung.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Chọn hình ảnh cơ sở để chạy ứng dụng
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogHung.dll"]
