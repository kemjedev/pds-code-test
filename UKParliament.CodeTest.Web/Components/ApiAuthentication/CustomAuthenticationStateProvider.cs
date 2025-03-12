//using Microsoft.AspNetCore.Components.Authorization;
//using System.Net.Http.Headers;
//using System.Security.Claims;

//public class CustomAuthenticationStateProvider : AuthenticationStateProvider
//{
//    private readonly HttpClient _httpClient;
//    private readonly ILocalStorageService _localStorageService;

//    public CustomAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
//    {
//        _httpClient = httpClient;
//        _localStorageService = localStorageService;
//    }

//    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
//    {
//        var token = await _localStorageService.GetItemAsync<string>("authToken");

//        var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwt");

//        _httpClient.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(token) ? null : new AuthenticationHeaderValue("Bearer", token);

//        return new AuthenticationState(new ClaimsPrincipal(identity));
//    }

//    public void NotifyUserAuthentication(string token)
//    {
//        var identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwt");
//        var user = new ClaimsPrincipal(identity);

//        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
//    }

//    public void NotifyUserLogout()
//    {
//        var identity = new ClaimsIdentity();
//        var user = new ClaimsPrincipal(identity);

//        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
//    }
//}
