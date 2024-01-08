# Bloghung
Bloghung là một dự án chia sẽ kinh nghiệm được phát triển một cách chuyên nghiệp bởi H.


**Thông tin VPS đã có sẵn có thể sử dụng.**   
**Lưu ý:** Nếu dùng của google thì 3 tháng hết hạng cần chuẩn bị phương án backup dữ liệu để chuyển qua server khác. 

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
	
## Phần 1: Deploy ứng dụng .NET lên VPS:  

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
# Lưu ý phải copy đầy đủ các project con các tầng Application, Infrastructure nếu có
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

 **Chạy Docker file:**  
  **Lưu ý:** Để trình chạy docker file không gặp lỗi thì chú ý các mục sau:  

1. Chú ý `Cấu trúc thư mục` .  
2. Chú ý `COPY và  restore` đầy đủ các project.  
3. Chú ý `base Image SDK, Runtime` đã đúng version hay chưa.  

Mở terminal và di chuyển đến thư mục chứa tệp Dockerfile, sau đó chạy lệnh:  
 ```bash
  docker build --platform linux/amd64 -t imageName --no-cache .
 ```
Lưu ý: nếu chạy ở local thì không cần `linux/amd64` cái này chỉ để deploy lên VPS Centos

 
  Run container:  
 
 ```bash
  docker run -p 44388:80 --name containerName  imageName
 ```

  Login vào docker hub:  
 
 ```bash
  dokcer login
 ```

  Đánh tag để đưa lên docker hub:  
 
 ```bash
  docker tag imageName:latest vanhungdev/imageName:v.1.1
 ```

  Đẩy image lên docker hub:  
 
 ```bash
  docker push vanhungdev/imageName:v.1.1
 ```
  Run container trên môi trường VPS Centos:  
 
 ```bash
  docker run -p 44380:80 --name containerName vanhungdev/imageName:v.1.1 
 ```

## Phần 2: Cấu hình reverse proxy, load Balance và SSL:  
  Cài đặt nginx:  

 ```bash
    docker run -d -p 80:80 -p 443:443 --name nginx-proxy --privileged=true \
	-e ENABLE_IPV6=true \
	-v ~/nginx/vhost.d:/etc/nginx/vhost.d \
	-v ~/nginx-certs:/etc/nginx/certs:ro \
	-v ~/nginx-conf:/etc/nginx/conf.d \
	-v ~/nginx-logs:/var/log/nginx \
	-v /usr/share/nginx/html \
	-v /var/run/docker.sock:/tmp/docker.sock:ro \
	jwilder/nginx-proxy
 ```

  Tải chứng chỉ SSL cho nó:  

 ```bash
   docker run -d --privileged=true \
	-v ~/nginx/vhost.d:/etc/nginx/vhost.d \
	-v ~/nginx-certs:/etc/nginx/certs:rw \
	-v /var/run/docker.sock:/var/run/docker.sock:ro \
	--volumes-from nginx-proxy \
	jrcs/letsencrypt-nginx-proxy-companion
 ```
Chú ý: volumes-from cho đúng với server nginx

  Chạy web lên:  

 ```bash
   docker run -it -d --name containerName \
	-e VIRTUAL_HOST="your-domain.vn" \
	-e VIRTUAL_PORT=80 \
	-e LETSENCRYPT_HOST="your-domain.vn" \
	-e LETSENCRYPT_EMAIL="vanhungdev@fpt.com.vn" \
	vanhungdev/imageName:v.1.1

 ```

Lưu ý: nếu muốn chạy 2 host độc lập thì chạy lại server api giống nhau đổi VIRTUAL_HOST và containerName.
