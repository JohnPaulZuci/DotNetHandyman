

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class Startup
{

    public void ConfigureServices(IServiceCollection services)
    {
        // ...

        // JWT Authentication
        var jwtSecretKey = "zuci_systems"; // Replace with your own secret key
        var key = Encoding.ASCII.GetBytes(jwtSecretKey);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // ...

        app.UseRouting();

        // ...

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }



    public string generateJWTToken()
    {
        var jwtSecretKey = "zuci_systems"; // Replace with your own secret key
        var key = Encoding.ASCII.GetBytes(jwtSecretKey);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("sub", "johnpaul") }), // Replace with your own user identifier
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        return jwtToken;
    }


}