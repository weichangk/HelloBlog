# Blog

## 预览地址
 **[博客入口](175.178.28.188:9000)** 

 **[后台入口](175.178.28.188:9000/Main/Login/Index)** 
后台预览账号/密码：root/123456（请勿修改密码）

#### 介绍
个人博客网站 

#### 软件架构
网站 .NET Core 6.0，已集成 Redis、Autofac、Mapster 映射、FluentValidation 验证组件（支持自带 model 验证）、集成极验行为验证、layui 开发，博客基本功能已经全部完成（异常日志只记录在了文件中）


#### 使用说明
1. 网站使用的 sqlsugar ORM 开源框架，相关文档请查看官网[sqlsugar框架](https://www.donet5.com/)，数据库使用的是mysql，ORM支持7种数据库（MySql、SqlServer、Sqlite、Oracle、Postgresql、达梦、人大金仓），所以可以随意切换，具体请看 sqlsugar 官网文档
2. 数据库备份以及脚本放在 db 目录下，执行任意一项即可，数据库表中仅将所有主键统一成“Id”,项目中后台管理员登录用户名/密码:admin/123456
3. 写代码都有详细注释，这里就不一一介绍
4. 创建数据库后记得修改 appsettings.json 文件中的数据库连接字符串
5. 在 Linux 部署注意事项，默认是使用的图形验证码，在 Linux 上部署需要安装相关依赖不然无法正常显示图形验证码，可自行百度解决，图形绘制已经替换为官方的 System.Drawing.Common 包

6.项目可以选择性使用 redis， 默认是没有启用 redis（默认使用内置缓存）的和极验验证的，需要启用请先安装 redis 和注册极验的账号，在 appsettings.json 文件中更改即可使用（注：极验行为验证免费版只支持滑块验证，还有一些其他限制，个人使用已经足够）
7. 日志模块还没有时间完成，以后有时间会补上，有需要可先参考此博客的另外一个版本
8. 接入 QQ 授权登录留言评论


前台预览
![前台预览](https:// "亿亿幺幺-个人博客.png")

后台预览![后台预览](https:// "后台管理系统.png")

如果有什么 BUG 还希望大家提交到 Issues，我看到会及时修复。



#### 部署
使用 jenkins，使用 docker-compose 结合 .env 环境变量配置进行部署
生产环境部署只要指定生产环境变量配置文件 prod.env 路径即可。防止了生产环境隐私配置上传到代码仓库
首次部署成功后需要连接数据库客户端创建数据库执行数据库脚本生成appsoft数据库（后续找下在部署脚本中执行数据库脚本方案）

docker-compose 部署脚本
```shell
docker-compose -f docker-compose.yml -p hellobolg --env-file .env down
docker-compose -f docker-compose.yml -p hellobolg --env-file .env up --detach
#docker-compose -f docker-compose.yml -p hellobolg --env-file prod.env down
#docker-compose -f docker-compose.yml -p hellobolg --env-file prod.env up --detach
```


问题
在服务器上 jenkins 部署执行指令的时候指定目录的 prod.env 找不到，jenkins 是在工作空间目录下找的，
发现在 jenkins 挂载目录下有对应构建任务的工作区目录 /var/lib/docker/volumes/jenkins_home/_data/workspace/helloblog 将 prod.env 放置工作区目录即可


