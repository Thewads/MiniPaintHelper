using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MiniPaintHelperUi;
using Services;
using MudBlazor.Services;
using Services.Converters;
using Services.Providers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IColorService, Services.ColorService>();
builder.Services.AddScoped<IPaintService, PaintService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IPaintProvider, JsonProvider>();
builder.Services.AddScoped<IRgbToLabConverter, RgbToLabConverter>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();