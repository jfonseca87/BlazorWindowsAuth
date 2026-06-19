using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWindowsAuth.Client;
using BlazorWindowsAuth.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp =>
{
    var client = new HttpClient
    { 
        BaseAddress = new Uri("http://localhost:5187/api/") 
    };

    return client;
});

builder.Services.AddTransient<WindowsOAuthService>();

await builder.Build().RunAsync();
