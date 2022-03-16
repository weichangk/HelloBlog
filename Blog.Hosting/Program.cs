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

//������ͼʵʱ��Ч
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false;//�ر�GDPR�淶
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

//���Session ����
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;

});

builder.Services.AddHttpContextAccessor();

//�Զ�ע������
builder.Services.AddConfig(builder.Configuration);

builder.Services.AddMvc().AddNewtonsoftJson(opt =>
{
    //����ѭ������
    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

    //���ı��ֶδ�С
    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

//����ģ����֤
builder.Services.AddControllersWithViews().AddValidation();
//�Զ�ģ��ӳ��
builder.Services.AddMapper();
//builder.Services.AddAutoDependencyInjection();//�Զ�ע�루���·�ʹ��Autofacע��� builder.RegisterModule<AutofacModule>(); ��ѡһ���ɣ�

#region ��������
var sysConfig = builder.Services.BuildServiceProvider().GetService<IOptionsMonitor<SysConfig>>().CurrentValue;
builder.Services.AddEasyCaching(options =>
{
    ////ʹ���ĵ� https://easycaching.readthedocs.io/en/latest
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

#region ʹ��Autofacע��
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container => {
    //�Զ�ע��
    container.RegisterModule<AutofacModule>();

    //�Զ�ע��service��ҵ��㣩
    container.AutoRegisterService("service");

    //ʹ��AspectCore���붯̬������������ע���м�Autofac.Extensions.DependencyInjection��nuget��������6.0�汾���������쳣
    container.AddAspectCoreInterceptor(x => x.CacheProviderName = cacheProviderName);
});
#endregion

#region �������ݿ�����
//֧�ֶ���������
builder.Services.AddSqlSugarConnection(builder.Configuration);
#endregion

#region cookieȫ������
//������֤cookie���ơ�����ʱ�䡢�Ƿ�����ͻ��˶�ȡ
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "appsoft";//cookie����
    options.Cookie.HttpOnly = true;//������ͻ��˻�ȡ
    options.SlidingExpiration = true;// �Ƿ��ڹ���ʱ������ʱ���Զ�����
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

//ʹ�þ�̬Autofac����
app.UseStaticContainer();

//�쳣�����м��
app.UseExceptionHandle();

#region ���Ubuntu Nginx �����ܻ�ȡIP����
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
#endregion

app.UseAuthentication();
app.UseAuthorization();

#region ·������

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


