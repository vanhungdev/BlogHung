# Bloghung
Bloghung là một dự án chia sẽ kinh nghiệm được phát triển một cách chuyên nghiệp bởi H.


**Thông tin Broker Kafka đã có sẵn trên VPS có thể sử dụng.**   
**Lưu ý:** ----------------- 

1. MapHost by pass proxy (mạng công ty):  
    ```bash
    34.16.204.104
	```	
2. Portainer:  
    ```bash
    http://34.16.204.104:9000/
	
	Username: admin
	Password: Provanhung77
	```	
	
## Phần 2: Cài đặt Kafka bằng docker compose:  

**Để deploy được úng dụng .NET 7 lên chúng ta cần có các bước sau:**   

1. **Tạo docker file** - Để chạy các SDK cần thiết.
2. **Build image** - Chúng ta sẽ build image từ docker file.
3. **Run và test ở local** - test website trước ở localhost kiểm tra các file tài nguyên đã được chưa.
4. **Đẩy Repositories kên docker hub** - Công cụ theo dõi và quản lý cần thiết cho việc load test.
5. **Run và test trên VPS Centos** - chạy thử ở môi trường thật.
6. **Cấu hình Reverse Proxy, Load Balance, trỏ domain và Let's Encrypt SSL** - chạy thử ở môi trường thật.

Tạo file docker file có tên `dockerfile` đặt ở đường dẫn có file .sln như sau:  


```bash
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
COPY ["BlogHung.Application/BlogHung.Application.csproj", "BlogHung.Application/"]
COPY ["BlogHung.Infrastructure/BlogHung.Infrastructure.csproj", "BlogHung.Infrastructure/"]
RUN dotnet restore "BlogHung/BlogHung.csproj"
RUN dotnet restore "BlogHung.Application/BlogHung.Application.csproj" 
RUN dotnet restore "BlogHung.Infrastructure/BlogHung.Infrastructure.csproj"

# Copy toàn bộ mã nguồn và build ứng dụng
COPY . .
WORKDIR "/src/BlogHung"
RUN dotnet build "BlogHung.csproj" -c Release -o /app/build

# Build runtime image
FROM build AS publish   
RUN dotnet publish "BlogHung.csproj" -c Release -o /app/publish

# Chọn hình ảnh cơ sở để chạy ứng dụng
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogHung.dll"]

```

 **Chạy Docker Compose:**  
  **Lưu ý:** Để trình chạy docker file không gặp lỗi thì chú ý các mục sau:  

1. Chú ý `port` đã bị chiếm trong hệ thống chưa: `2181`, `9092`, `9091`.  
2. Chú ý `container_name` đã có chưa: `zookeeper`, `kafka`, `kafdrop`.  
3. Chú ý `networks` đã có chưa: `kafka-net`.  
4. Chú ý cấu hình các `environment` (biến môi trường) phù hợp.  
5. Chú ý nếu gặp lỗi `The requested image's platform` thì điều chỉnh `image` lại cho phù hợp với platform của bạn.  

Mở terminal và di chuyển đến thư mục chứa tệp docker-compose.yml, sau đó chạy lệnh:  
 ```bash
  docker-compose up -d
 ```
 
  Truy cập Kafdrop:  
 
 ```bash
  http://localhost:9091
 ```
