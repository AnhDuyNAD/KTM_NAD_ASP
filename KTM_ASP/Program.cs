using KTM_ASP.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<KTM_ASPContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KTM_ASPContext") ?? throw new InvalidOperationException("Connection string 'KTM_ASPContext' not found.")));

// Đăng ký HttpContextAccessor (nếu cần sử dụng Session trong Views)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Cấu hình Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Thêm dịch vụ MVC với Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Xử lý lỗi cho môi trường Production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Bật HTTPS & Static Files
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Kích hoạt Session
app.UseSession();

app.UseAuthorization();

// Định tuyến Controller Mặc Định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
