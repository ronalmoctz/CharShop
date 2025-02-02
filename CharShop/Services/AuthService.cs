using CharShop.Data;
using CharShop.DTO;
using CharShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CharShop.Services
{
    public class AuthService
    {
        private readonly CarShopDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(CarShopDbContext context, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
            _configuration = configuration;
        }

        public async Task<string?> RegisterAsync(RegisterDto request)
        {

            Log.Information("Registering user with email {Email}", request.Email);

            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                Log.Warning("Register faile: missing files");
                return "Email and Password are required.";
            }

            // Verificar si el usuario ya existe
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                Log.Warning("Register failed: email already in use");
                return "Email already in use.";
            }

            // Buscar el rol "Customer"
            var customerRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Customer");
            if (customerRole == null)
            {
                Log.Warning("Register failed: role 'Customer' not found");
                return "Role 'Customer' not found.";
            }

            try
            {
                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    Email = request.Email,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    RoleId = customerRole.RoleId
                };

                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Crear el UserDetail
                var userDetail = new UserDetail
                {
                    UserDetailId = Guid.NewGuid(),
                    UserId = user.UserId,
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    Age = request.Age,
                    Address = request.Address,
                    CreditCard = request.CreditCard
                };

                _context.UserDetails.Add(userDetail);
                await _context.SaveChangesAsync();

                Log.Information("User registered with email {Email}", request.Email);
                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error registering user with email {Email}", request.Email);
                return "Error registering user.";
            }

        }


        public async Task<string?> LoginAsync(string email, string password)
        {
            Log.Information("Try login for {Email}", email);

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                Log.Warning("Login failed: user not found");
                return null;
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash ?? string.Empty, 
                password
            );

            if (passwordResult != PasswordVerificationResult.Success)
            {
                Log.Warning("Login failed: invalid password {Email}", email);
                return null;
            }


            Log.Information("User {Email} logged in", email);
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key is not configured.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
                new Claim("UserId", user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
