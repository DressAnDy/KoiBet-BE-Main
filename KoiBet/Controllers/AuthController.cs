using KoiBet.Data;
using KoiBet.DTO.User;
using KoiBet.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KoiBet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            // Kiểm tra username có hợp lệ không
            if (string.IsNullOrEmpty(loginDTO.Username))
            {
                return BadRequest(new { message = "Username needs to be filled" });
            }

            // Kiểm tra password có hợp lệ không
            if (string.IsNullOrEmpty(loginDTO.Password))
            {
                return BadRequest(new { message = "Password needs to be entered" });
            }

            // Tìm người dùng trong cơ sở dữ liệu
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDTO.Username);

            // Kiểm tra người dùng có tồn tại không
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            // Kiểm tra password có hợp lệ không
            if (string.IsNullOrEmpty(user.Password) || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid password" });
            }

            // Nếu tất cả đều hợp lệ, tạo phản hồi cho người dùng
            var userResponse = new
            {
                user.user_id,
                user.Username,
                user.role_id
            };

            return Ok(userResponse);
        }

    // POST: auth/register
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        var lastUser = await _context.Users.OrderByDescending(u => u.user_id).FirstOrDefaultAsync();
        var newUserId = lastUser == null ? "U1" : "U" + (int.Parse(lastUser.user_id.Substring(1)) + 1).ToString();

        // Check if the role exists
        var roleExists = await _context.Roles.AnyAsync(r => r.role_id == "R1");
        if (!roleExists)
        {
            // Insert the default role if it does not exist
            var defaultRole = new Roles
            {
                role_id = "R1",
                role_name = "customer" // Set the role name is customer
            };
            _context.Roles.Add(defaultRole);
            await _context.SaveChangesAsync();
        }

        var newUser = new Users
        {
            user_id = newUserId,
            Username = registerDTO.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
            role_id = "R1" // Set default role_id to R1
        };

        // Add user to the database
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok(newUser);
        }
    }

}
