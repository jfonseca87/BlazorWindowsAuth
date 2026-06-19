using System.Net.Http.Headers;

namespace BlazorWindowsAuth.Client.Services;

public class WindowsOAuthService
{
    private readonly HttpClient _httpClient;
    public WindowsOAuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> Test()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer", 
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
        );

        var response = await _httpClient.GetAsync("test");

        return true;
    }
}
