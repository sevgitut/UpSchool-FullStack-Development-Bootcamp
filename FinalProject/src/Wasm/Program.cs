using Application.Common.Interfaces;
using Application.Services;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Wasm;
using Wasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//var apiUrl = builder.Configuration.GetSection("ApiUrl").Value!;
//var signalRUrl = builder.Configuration.GetSection("SignalRUrl").Value!;
//builder.Services.AddBlazoredToast();

//builder.Services.AddScoped<IToasterService, BlazoredToastService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddSingleton<IUrlHelperService>(new UrlHelperService(apiUrl, signalRUrl));

await builder.Build().RunAsync();
