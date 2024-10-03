using KoiBet.Data;
using KoiBet.DTO.User;
using KoiBet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace KoiBet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UpdateUserDTO _userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new Users
            {
                user_id = Guid.NewGuid().ToString(),
                full_name = _userDTO.full_name,
                Email = _userDTO.email,
                Phone = _userDTO.phone,
                role_id = _userDTO.role_id,
                Balance = 0
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User created successfully", user = newUser });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Cập nhật thông tin
            user.full_name = updateUserDTO.full_name ?? user.full_name;
            user.Email = updateUserDTO.email ?? user.Email;
            user.Phone = updateUserDTO.phone ?? user.Phone;
            user.role_id = updateUserDTO.role_id ?? user.role_id;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User updated successfully", user = user });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            // Tìm người dùng dựa trên user_id
            var user = await _context.Users.FirstOrDefaultAsync(u => u.user_id == id);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            // Xóa người dùng khỏi cơ sở dữ liệu
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User deleted successfully." });
        }
    }
}
