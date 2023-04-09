﻿using ASM_CSharp4_Linhtnph20247.Services;
using ASM_CSharp4_Linhtnph20247.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICartDetailService, CartDetailService>();
builder.Services.AddTransient<IOrderDetailService, OrderDetailService>();
builder.Services.AddSession(option =>
{
    option.IOTimeout = TimeSpan.FromSeconds(60);
    //Định hình Session này tồn tại trong 60 giây
});//Thêm để dùng Session

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession(); //Gọi để dùng Session

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
