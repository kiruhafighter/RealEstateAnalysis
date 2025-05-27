using ApiClient;
using RealEstateAnalysis.Client.Services;
using RealEstateAnalysis.Client.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<JwtAuthorizationMessageHandler>();

builder.Services.AddHttpClient<IClient, Client>(httpClient =>
{
    var baseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");
    httpClient.BaseAddress = new Uri(baseUrl!);
}).AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

builder.Services.AddHttpClient<IChatClient, ChatClient>(httpClient =>
{
    var chatApiBaseUrl = builder.Configuration.GetValue<string>("ChatApiBaseUrl");
    httpClient.BaseAddress = new Uri(chatApiBaseUrl!);
    httpClient.Timeout = TimeSpan.FromSeconds(1000);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();