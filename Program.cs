using finalTaskItra;
using finalTaskItra.Controllers;
using finalTaskItra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options =>
    {
        options.WithOrigins("https://final-task-itra.vercel.app").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddDbContext<EFCoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetValue<string>("AppCon"));
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = false,
             IssuerSigningKey = new AuthOptions(builder.Configuration).GetSymmetricSecurityKey(),
             ValidateIssuerSigningKey = true
         };
     });

var app = builder.Build();

app.UseCors("AllowOrigin");

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "photos")),
    RequestPath = "/photos"
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseWebSockets();
app.MapHub<ChatHub>("chat-hub");

//var people = new List<Person>
// {
//    new Person("tom", "12345", "admin"),
//    new Person("bob", "55555", "user")
//};
//app.MapPost("/login", (Person loginData) =>
//{
//    // находим пользователя 
//    Person? person = people.FirstOrDefault(p => p.Email == loginData.Email && p.Password == loginData.Password);
//    Console.WriteLine(person);
//    // если пользователь не найден, отправляем статусный код 401
//    if (person is null) return Results.Unauthorized();

//    var claims = new List<Claim>
//    { 
//        new Claim(ClaimTypes.Name, person.Email),
//        new Claim(ClaimTypes.Role, person.Role)
//    };
//    // создаем JWT-токен
//    var jwt = new JwtSecurityToken(
//            claims: claims,
//            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
//            );

//    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

//    // формируем ответ
//    var response = new
//    {
//        access_token = encodedJwt,
//        username = person.Email
//    };

//    return Results.Json(response);
//});

//app.Map("/admin", [Authorize(Roles = "admin")] (HttpContext context) => 
//{
//    Console.WriteLine(context);
//    return new { message = "You are authorized admin!" }; 
//});
//app.Map("/user", [Authorize(Roles ="user, admin")] (HttpContext context) => new { message = "You are authorized user!" });
//app.Map("/", () => "Home Page");

app.Run();
//record class Person(string Email, string Password, string Role);