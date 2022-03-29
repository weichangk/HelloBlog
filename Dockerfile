#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM registry.cn-hangzhou.aliyuncs.com/newbe36524/aspnet:6.0 AS base
#解决图形验证码无法显示问题
RUN apt-get update && apt-get install libgdiplus -y
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM registry.cn-hangzhou.aliyuncs.com/newbe36524/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Blog.Hosting/Blog.Hosting.csproj", "Blog.Hosting/"]
COPY ["src/Blog.Application/Blog.Application.csproj", "Blog.Application/"]
COPY ["src/Blog.Core/Blog.Core.csproj", "Blog.Core/"]
COPY ["src/Blog.Framework/Blog.Framework.csproj", "Blog.Framework/"]
RUN dotnet restore "Blog.Hosting/Blog.Hosting.csproj"
COPY . .
WORKDIR "/src/src/Blog.Hosting"
RUN dotnet build "Blog.Hosting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.Hosting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.Hosting.dll"]