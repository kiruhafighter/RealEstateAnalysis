using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace RealEstateAnalysis.Client.Authentication;

public class JwtAuthorizationMessageHandler : DelegatingHandler
{
    private readonly IJSRuntime _jsRuntime;

    public JwtAuthorizationMessageHandler(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await GetTokenAsync();

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        
        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string> GetTokenAsync()
    {
        return await _jsRuntime.InvokeAsync<string>("getJwtToken");
    }
}