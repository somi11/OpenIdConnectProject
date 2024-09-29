using CmosGalleryApi.Authorization;
using ImageGallery.API.Authorzation;
using ImageGallery.API.DbContexts;
using ImageGallery.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(configure => configure.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddDbContext<GalleryContext>(options =>
{
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:ImageGalleryDBConnectionString"]);
});

// register the repository
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();

builder.Services.AddHttpContextAccessor();

// register the handler

builder.Services.AddScoped<IAuthorizationHandler, MustOwnImageHandler>();

// register AutoMapper-related services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
// .AddJwtBearer();
.AddJwtBearer(options =>
{
options.Authority = "https://localhost:5001";
options.Audience = "cmosApi";
    options.TokenValidationParameters = new()
    {
        NameClaimType = "given_name",
        RoleClaimType = "role",
        ValidTypes = new[] { "at+jwt" }
    };
});
//.AddOAuth2Introspection(options =>
//{
//    options.Authority = "https://localhost:5001";
//    options.ClientId = "cmosApi";
//    options.ClientSecret = "apisecret";
//    options.NameClaimType = "given_name";
//    options.RoleClaimType = "role";

//});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanAddImage", AuthorizationPolicies.CanAddImage());
    options.AddPolicy("clientApplicationCanWrite", policy =>
    {
        policy.RequireClaim("scope", "cmosApi.write");
    });
   options.AddPolicy("mustOwnImage", policyBuilder =>
   {
       policyBuilder.RequireAuthenticatedUser();
       policyBuilder.AddRequirements(new MustOwnImageRequirement());
   });
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
