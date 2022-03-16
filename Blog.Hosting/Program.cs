using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blog.Core.Config;
using Blog.Core.Extensions;
using Blog.Framework.DataValidation.Extensions;
using Blog.Framework.DependencyInjection;
using Blog.Framework.DependencyInjection.Extensions;
using Blog.Framework.Mapper.Extensions;
using Blog.Hosting.Extensions;
using EasyCaching.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

string cacheProviderName = "default";

var builder = WebApplication.CreateBuilder(args);

//更改视图实时生效
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false;//关闭GDPR规范
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

//添加Session 服务
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;

});

builder.Services.AddHttpContextAccessor();

//自动注入配置
builder.Services.AddConfig(builder.Configuration);

builder.Services.AddMvc().AddNewtonsoftJson(opt =>
{
    //忽略循环引用
    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

    //不改变字段大小
    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

//加入模型验证
builder.Services.AddControllersWithViews().AddValidation();
//自动模型映射
builder.Services.AddMapper();
//builder.Services.AddAutoDependencyInjection();//自动注入（和下方使用Autofac注入的 builder.RegisterModule<AutofacModule>(); 二选一即可）

#region 缓存配置
var sysConfig = builder.Services.BuildServiceProvider().GetService<IOptionsMonitor<SysConfig>>().CurrentValue;
builder.Services.AddEasyCaching(options =>
{
    ////使用文档 https://easycaching.readthedocs.io/en/latest
    if (sysConfig.UseRedis)
    {
        options.UseCSRedis(builder.Configuration);
        cacheProviderName = EasyCachingConstValue.DefaultCSRedisName;
    }
    else
    {
        cacheProviderName = EasyCachingConstValue.DefaultInMemoryName;
        options.UseInMemory(builder.Configuration);
    }
    options.WithJson(cacheProviderName);

});
#endregion

#region 使用Autofac注入
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container => {
    //自动注入
    container.RegisterModule<AutofacModule>();

    //自动注入service（业务层）
    container.AutoRegisterService("service");

    //使用AspectCore加入动态代理（拦截器）注：切记Autofac.Extensions.DependencyInjection的nuget包必须是6.0版本，否则会出异常
    container.AddAspectCoreInterceptor(x => x.CacheProviderName = cacheProviderName);
});
#endregion

#region 配置数据库连接
//支持多数据连接
builder.Services.AddSqlSugarConnection(builder.Configuration);
#endregion

#region cookie全局设置
//设置认证cookie名称、过期时间、是否允许客户端读取
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "appsoft";//cookie名称
    options.Cookie.HttpOnly = true;//不允许客户端获取
    options.SlidingExpiration = true;// 是否在过期时间过半的时候，自动延期
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
});
#endregion



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseSession();
app.UseCookiePolicy();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//使用静态Autofac容器
app.UseStaticContainer();

//异常处理中间件
app.UseExceptionHandle();

#region 解决Ubuntu Nginx 代理不能获取IP问题
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
#endregion

app.UseAuthentication();
app.UseAuthorization();

#region 路由配置

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

#endregion

app.Run();


