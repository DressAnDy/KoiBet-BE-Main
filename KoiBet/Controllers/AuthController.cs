using KoiBet.Data;
using KoiBet.DTO;
using KoiBet.Entites;
using KoiBet.Entities;
using KoiBet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace KoiBet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Khai báo ApplicationDbContext
        private readonly IAuthService _authService; // Thêm dịch vụ xác thực

        // Constructor
        public AuthController(ApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        // POST: auth/login
        [AllowAnonymous]
        [HttpPost("login")] // Thêm đường dẫn cho rõ ràng
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            // Kiểm tra username và password
            if (string.IsNullOrEmpty(loginDTO.Username))
            {
                return BadRequest(new { message = "Username needs to be entered" });
            }
            else if (string.IsNullOrEmpty(loginDTO.Password))
            {
                return BadRequest(new { message = "Password needs to be entered" });
            }

            // Thực hiện đăng nhập
            Users loggedInUser = await _authService.Login(loginDTO.Username, loginDTO.Password);

            if (loggedInUser != null)
            {
                return Ok(loggedInUser); // Trả về thông tin người dùng đã đăng nhập
            }

            return BadRequest(new { message = "User login unsuccessful" });
        }

        // POST: auth/register
        [AllowAnonymous]
        [HttpPost("register")] // Thêm đường dẫn cho rõ ràng
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO) // Sửa tên biến cho đúng
        {
            if (string.IsNullOrEmpty(registerDTO.Username))
            {
                return BadRequest(new { message = "Username needs to be entered" });
            }
            else if (string.IsNullOrEmpty(registerDTO.Password))
            {
                return BadRequest(new { message = "Password needs to be entered" });
            }
            else if (string.IsNullOrEmpty(registerDTO.Email))
            {
                return BadRequest(new { message = "Email needs to be entered" });
            }

            var userToRegister = new Users(registerDTO.Username, registerDTO.Password, registerDTO.Email);
            var registeredUser = await _authService.Register(userToRegister);

            if (registeredUser != null)
            {
                // Đăng nhập ngay sau khi đăng ký
                return Ok(registeredUser);
            }

            return BadRequest(new { message = "User registration unsuccessful" });
        }

        // GET: auth/test
        [Authorize(Roles = "Everyone")]
        [HttpGet("test")] // Thêm đường dẫn cho rõ ràng
        public IActionResult Test()
        {
            string token = Request.Headers["Authorization"];

            if (token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = handler.ReadJwtToken(token);

            var claims = new Dictionary<string, string>();

            foreach (var claim in jwt.Claims)
            {
                claims.Add(claim.Type, claim.Value);
            }

            return Ok(claims); // Trả về các claims trong token
        }

        //// Phương thức đăng nhập bằng Google (chưa hoàn thiện)
        //[AllowAnonymous]
        //[HttpPost("login-google")] // Thêm đường dẫn cho rõ ràng
        //public async Task<IActionResult> LoginByGoogle([FromBody] GoogleLoginModel model)
        //{
        //    // Logic để xác thực người dùng qua Google
        //    // Chưa hoàn thiện, cần định nghĩa GoogleLoginModel và logic xác thực
        //}
    }
}
