﻿using Blog.Core.Config;
using Blog.Core.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Blog.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加数据库连接
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlSugarConnection(this IServiceCollection services, IConfiguration configuration, string section = "DbConfig")
        {
            var configs = configuration.GetSection(section).Get<List<ConnectionConfig>>();
            foreach (var item in configs)
            {
                // 改用环境变量配置方便容器部署
                var db = configuration["DBNAME"] ?? "appsoft";
                var host = configuration["DBHOST"] ?? "localhost";
                var port = configuration["DBPORT"] ?? "3306";
                var pwd = configuration["DBPWD"] ?? "123456";
                item.ConnectionString = $"Server={host};User Id=root;Password={pwd};Port={port};Database={db};Allow User Variables=True";
                item.ConfigureExternalServices = new ConfigureExternalServices
                {
                    //配置ORM缓存
                    DataInfoCacheService = new SqlSugarCache()
                };
            }

            //注入SqlSugarClient
            services.AddScoped<ISqlSugarClient>(x =>
            {
                return new SqlSugarClient(configs);
            });
            //注入泛型仓储
            services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));
            return services;
        }
    }
}