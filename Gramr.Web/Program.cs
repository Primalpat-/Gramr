using Blazm.Components;
using Gramr.Core.Interfaces.Api;
using Gramr.Core.Interfaces.Data.Factories;
using Gramr.Core.Interfaces.Data.Services;
using Gramr.Core.Models.Data;
using Gramr.Data.Factories;
using Gramr.Data.Services;
using Gramr.Data.Settings;
using Gramr.Logic.Services;
using Gramr.Logic.Services.Api;
using Gramr.Logic.Services.Data;
using Gramr.Web.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("Api"));

builder.Services.AddTransient<IDataAccessService<Company>, DataAccessService<Company>>();
builder.Services.AddTransient<IDataService<Company>, GenericDataService<Company>>();
builder.Services.AddTransient<IDataAccessService<MarketAggregate>, DataAccessService<MarketAggregate>>();
builder.Services.AddTransient<IDataService<MarketAggregate>, GenericDataService<MarketAggregate>>();
builder.Services.AddTransient<IMarketDataRetrievalService, MarketDataRetrievalService>();
builder.Services.AddTransient<IMarketDataManagementService, MarketDataManagementService>();

builder.Services.AddSingleton<IGramrDbFactory, GramrDbFactory>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddBlazm();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
