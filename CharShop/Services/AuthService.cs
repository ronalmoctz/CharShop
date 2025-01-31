using CharShop.Data;
using CharShop.DTO;
using CharShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CharShop.Services
{
    public class AuthService
    {
        private readonly CarShopDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(CarShopDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<string?> RegisterAsync(RegisterDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return "Email and Password are required.";
            }

            // Verificar si el usuario ya existe
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return "Email already in use.";
            }

            // Buscar el rol "Customer"
            var customerRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Customer");
            if (customerRole == null)
            {
                return "Role 'Customer' not found.";
            }

            // Crear el usuario
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(null, request.Password),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                RoleId = customerRole.RoleId
            };

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

            return null; 
        }
    }
}
