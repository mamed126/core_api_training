
using Auth.Common.Constants;
using Auth.Common.Contracts;
using Auth.TokenService.Manager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAuthentication().AddJwtBearer();

//var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mySecretKey@123mySecretKey@123mySecretKey@123mySecretKey@123mySecretKey@123mySecretKey@123mySecretKey@123mySecretKey@123mySecretKey@123mySecretKey@123"));
//var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

//var tokenOptions = new JwtSecurityToken(
//    issuer: "http://localhost:3000",
//    audience: "http://localhost:3000",
//    claims: claims,
//    expires: DateTime.Now.AddMinutes(5),
//    signingCredentials: signinCredentials
//    );

//builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(CommonConstants.AccessTokenSecretKey);
    
    o.SaveToken = true;

    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Key),
        ClockSkew = TimeSpan.Zero
    };
});


//add service for token
builder.Services.AddTransient<ITokenService,TokenManager>() ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.UseAuthentication();//- auth

app.UseAuthorization();

app.MapControllers();

app.Run();
