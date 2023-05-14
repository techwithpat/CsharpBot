using CsharpBot;
using CsharpBot.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.Configure<ChatOptions>(o => builder.Configuration.GetSection("OpenAI").Bind(o));

builder.Services.AddScoped<OpenAIService>();

await builder.Build().RunAsync();
