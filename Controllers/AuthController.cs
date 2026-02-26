using BooksApi.Data;
using BooksApi.Dtos.Authentication;
using BooksApi.Models;
using BooksApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IConfiguration configuration)
        { 
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration; 
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExists = await _userManager.FindByNameAsync(dto.Username);

            if (userExists != null)
                return BadRequest($"User {dto.Username} already exists!");

            ApplicationUser user = new ApplicationUser()
            {
                UserName = dto.Username,
                Email = dto.Email,
                Custom = "Default Value",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest($"User {dto.Username} creation failed! Please check user details and try again.");

            return Created(nameof(Register), $"User {dto.Username} Created Successfully.");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized("Invalid username or password.");

            var tokenValue = await GenerateToken(user);

            return Ok(tokenValue);
        }



        private async Task<AuthResult> GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var JwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                IdRevoked = false,
                DateAdded = DateTime.UtcNow,
                dateExpire = DateTime.UtcNow.AddMonths(6),
                Token =   Guid.NewGuid().ToString() +"_"+ Guid.NewGuid().ToString()

            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResult
            {
                Token = JwtToken,
                RefreshToken = refreshToken.Token,
                ExpireAt = token.ValidTo
            };
        }
    }
}
