using Exo.WebApi.Contexts;
using Exo.WebApi.Repositories;
using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ExoContext, ExoContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Forma de autenticacão.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
// Parâmetros de validacão do token.
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuer = true,

        ValidateAudience = true,

        ValidateLifetime = true,

        IssuerSigningKey = new
    SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapichave-autenticacao")),

        ClockSkew = TimeSpan.FromMinutes(30),

        ValidIssuer = "exoapi.webapi",

        ValidAudience = "exoapi.webapi"
    };
});
builder.Services.AddTransient<ProjetoRepository,
ProjetoRepository>();
builder.Services.AddTransient<UsuarioRepository,
UsuarioRepository>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
// Habilita a autenticação.
app.UseAuthentication();
// Habilita a autorização.
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
