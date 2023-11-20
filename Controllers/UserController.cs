using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using finalTaskItra.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using finalTaskItra.Models;
using System.Text;
using System.Security.Cryptography;

namespace finalTaskItra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly EFCoreContext _context;

        public UserController(IConfiguration configuration, IWebHostEnvironment env, EFCoreContext context)
        {
            _configuration = configuration;
            _env = env;
            _context = context;
        }
        public static string HashWithSHA256(string value)
        {
            using var hash = SHA256.Create();
            var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToHexString(byteArray);
        }

        [HttpPost("login/")]
        public JsonResult Login(UserLoginInfo info)
        {
            string saltedPassword = info.saltedPassword + _configuration["Salt"];
            for (int i = 0; i < Convert.ToInt32(_configuration["Iterations"]); i++)
            {
                saltedPassword = HashWithSHA256(saltedPassword);
            }
            User? user = _context.users.FirstOrDefault(user =>
                user.email == info.email && user.saltedPassword == saltedPassword);
            if (user is null)
                return new JsonResult("Invalid data.");
            user.loginDate = DateTime.Now;
            _context.SaveChanges();
            return new JsonResult(user);
        }

        [HttpPost("register/")]
        public JsonResult Register(UserRegisterInfo user)
        {
            User? userFind = _context.users.FirstOrDefault(userFind =>
                userFind.email == user.email);
            string saltedPassword = user.saltedPassword + _configuration["Salt"];
            for (int i = 0; i < Convert.ToInt32(_configuration["Iterations"]); i++)
            {
                saltedPassword = HashWithSHA256(saltedPassword);
            }
            if (userFind != null)
                return new JsonResult("User exists.");
            User newUser = new User();
            newUser.email = user.email;
            newUser.saltedPassword = saltedPassword;
            newUser.fullName = user.fullName;
            newUser.role = 0;
            newUser.access = true;
            newUser.loginDate = DateTime.Now;
            newUser.joinDate = DateTime.Now;
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, newUser.fullName),
                        new Claim(ClaimTypes.Role, Convert.ToString(newUser.role)),
                        new Claim(ClaimTypes.Email, newUser.email),
                    };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: new SigningCredentials(new AuthOptions(_configuration).GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            newUser.accessToken = encodedJwt;
            _context.users.Add(newUser);
            _context.SaveChanges();
            return new JsonResult("User created");
        }

        [HttpPut("changeInfo/")]
        [Authorize(Roles = "0")]
        public JsonResult ChangeInfo(UserChangeInfo user)
        {
            User? userFind = _context.users.FirstOrDefault(userFind =>
                userFind.email == user.email && userFind.accessToken != user.accessToken);
            if (userFind != null)
                return new JsonResult("User with that email exists.");
            string saltedPasswordOld = user.saltedOldPassword + _configuration["Salt"];
            for (int i = 0; i < Convert.ToInt32(_configuration["Iterations"]); i++)
            {
                saltedPasswordOld = HashWithSHA256(saltedPasswordOld);
            }
            var userToChange = _context.users.FirstOrDefault(userToFind => userToFind.accessToken == user.accessToken && userToFind.saltedPassword == saltedPasswordOld);
            if (userToChange == null)
                return new JsonResult("Wrong Password.");
            userToChange.email = user.email;
            if (user.saltedNewPassword != "")
            {
                string saltedPasswordNew = user.saltedNewPassword + _configuration["Salt"];
                for (int i = 0; i < Convert.ToInt32(_configuration["Iterations"]); i++)
                {
                    saltedPasswordNew = HashWithSHA256(saltedPasswordNew);
                }
                userToChange.saltedPassword = saltedPasswordNew;
            }
            userToChange.fullName = user.fullName;
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userToChange.fullName),
                        new Claim(ClaimTypes.Role, Convert.ToString(userToChange.role)),
                        new Claim(ClaimTypes.Email, userToChange.email),
                    };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: new SigningCredentials(new AuthOptions(_configuration).GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            userToChange.accessToken = encodedJwt;
            _context.SaveChanges();
            return new JsonResult(userToChange);
        }

        [HttpDelete("delete/")]
        [Authorize(Roles = "0")]
        public JsonResult Delete(UserDeleteInfo info)
        {
            User? user = _context.users.FirstOrDefault(user =>
                user.email == info.email && user.saltedPassword == info.saltedPassword && user.accessToken == info.accessToken);
            if (user is null)
                return new JsonResult("Invalid data.");
            _context.Remove(user);
            _context.SaveChanges();
            return new JsonResult(user);
        }
    }
}
