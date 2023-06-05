using System.Diagnostics;
using System.Buffers.Text;
using System.Reflection.Metadata;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Login.Models;
using dotnet.Data;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace LoginUser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public class LoginCredentials
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login.Models.Login>>> GetLogins()
        {
            return await _context.Logins.ToListAsync();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Login.Models.Login>> CreateNewLogin(Login.Models.Login login)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(login.password);
            login.password = hashedPassword;
            _context.Logins.Add(login);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLogins), new { id = login.Id }, login);
        }

        [HttpPost("log_in")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials)
        {
            var user = await _context.Logins.FirstOrDefaultAsync(u => u.email == loginCredentials.email);
            if (user == null)
            {
                return NotFound();
            }

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(loginCredentials.password, user.password);
            if (!passwordMatch)
            {
                return BadRequest("Incorrect password entered");
            }

            // Generate a token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("fwgi0clglr3dkctuxdf3fc0ng5v8shzty7sqsohlkfgurneq3yhj3xjt6ugkd3ia5b5m6dxyyhyg45sig2shcgrdcjtszdaz2ph6o7fvhasojjxjxw2wq8f0t4xvjcqz"); // Replace with your secret key
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.email),
            new Claim("userName", user.userName),
            new Claim("Id", user.Id.ToString()),
            new Claim("year", user.year.ToString()),
            new Claim("phoneNo", user.phoneNo),
            new Claim("address", user.address),
            new Claim("universityName", user.universityName),
            new Claim("department", user.department),
            new Claim("course", user.course),
            new Claim("nameHOD", user.nameHOD),
            new Claim("hodEmail", user.hodEmail),
            new Claim("hodPhone", user.hodPhone),
            new Claim("userRole", user.userRole),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new { Token = tokenString });
        }


    }
}
