using FakeBook.Application.Common.Interfaces;
using FakeBook.Application.Handlers.Commads;
using FakeBook.Application.Handlers.Queries.Services;
using FakeBook.Application.Handlers.Queries;
using FakeBook.Contracts.Commands;
using FakeBook.Contracts.Queries.Services;
using FakeBook.Contracts.Queries;
using FakeBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FakeBook.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => {
  options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IAuthenticationCommand, AuthenticationCommand>();
builder.Services.AddScoped<IAuthenticationQuery, AuthenticationQuery>();
builder.Services.AddScoped<IJwtQueryService, JwtQueryService>();
builder.Services.AddScoped<IFriendCommand, FriendCommand>();
builder.Services.AddScoped<IMessageCommand, MessageCommand>();
builder.Services.AddScoped<IProfileCommand, ProfileCommand>();
builder.Services.AddDistributedMemoryCache();

builder.Services
    .AddAuthentication(x => {
      x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o => {
      var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
      o.SaveToken = true;
      o.TokenValidationParameters =
          new TokenValidationParameters { ValidateIssuer = false,
                                          ValidateAudience = false,
                                          ValidateLifetime = true,
                                          ValidateIssuerSigningKey = true,
                                          ValidIssuer = builder.Configuration["JWT:Issuer"],
                                          ValidAudience = builder.Configuration["JWT:Audience"],
                                          IssuerSigningKey = new SymmetricSecurityKey(Key) };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}
// app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(m => m.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
