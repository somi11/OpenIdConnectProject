using CmosGalleryApi.Authorization;
using ImageGallery.Client.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(configure => 
        configure.JsonSerializerOptions.PropertyNamingPolicy = null);


JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAccessTokenManagement();

// create an HttpClient used for accessing the API
builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ImageGalleryAPIRoot"]);
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddUserAccessTokenHandler();


builder.Services.AddHttpClient("IDPClient", client =>
{

    client.BaseAddress = new Uri("https://localhost:5001");

});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme , options =>
    {
        options.AccessDeniedPath = "/Authentication/AccessDenied";
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Authority   = "https://localhost:5001";
        options.ClientId  = "cmos";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        //options.Scope.Add("openid");
        //options.Scope.Add("profile");
        //options.CallbackPath = new PathString("signin-oidc");
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
        //add claim to not filter
        options.ClaimActions.Remove("aud");
        //removing claims from claims list
        options.ClaimActions.DeleteClaim("sid");
        options.ClaimActions.DeleteClaim("idp");

        //adding roles to get roles from claims
        options.Scope.Add("roles");
        options.Scope.Add("country");
        // options.Scope.Add("cmosApi.fullAccess");
        options.Scope.Add("offline_access");
        options.Scope.Add("cmosApi.read");
        options.Scope.Add("cmosApi.write");
        options.ClaimActions.MapJsonKey("role", "role");
        options.ClaimActions.MapUniqueJsonKey("country", "country");
        options.TokenValidationParameters = new ()
        {
            NameClaimType = "name",
            RoleClaimType = "role"
        };
        
     });

builder.Services.AddAuthorization(authorizationOptions =>
{
    authorizationOptions.AddPolicy("CanAddImage", AuthorizationPolicies.CanAddImage());
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()  // Allow any origin
              .AllowAnyMethod()  // Allow any HTTP method
              .AllowAnyHeader(); // Allow any header
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
   
} else
{
   // app.UseCookieDeletion();
    
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//app.UseCookieDeletion();
app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ContentSecurityPolicyMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Gallery}/{action=Index}/{id?}");

app.Run();
