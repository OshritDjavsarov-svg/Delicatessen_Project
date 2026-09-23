using BLL.BLLInterfaces;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Delicatessen.Controllers
{
    [Route("api/[controller]")] // הנתיב יהיה: /api/users
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IUserBLLService _userService;

        public UsersController(IUserBLLService userService) // הזרקת IUserService
        {
            _userService = userService;
        }

        [HttpPost("register")] // הנתיב יהיה: POST /api/users/register
        public ActionResult<UserResponseDto> Register([FromBody] UserRegisterDto userDto)
        {
            // קריאה לשירות ה-BLL
            UserResponseDto newUser = _userService.Register(userDto);

            if (newUser == null)
            {
                // אם המייל כבר קיים, נחזיר קוד 409 Conflict או 400 Bad Request
                return Conflict("אימייל זה כבר רשום במערכת.");
            }

            // הצלחה: קוד 201 Created
            return StatusCode(201, newUser);
        }

        [HttpPost("login")] // הנתיב יהיה: POST /api/users/login
        public ActionResult<UserResponseDto> Login([FromBody] UserLoginDto loginDto)
        {
            // קריאה לשירות ה-BLL
            UserResponseDto loggedInUser = _userService.Login(loginDto);

            if (loggedInUser == null)
            {
                // נחזיר 401 Unauthorized (או 400) כדי לסמן שפרטי ההתחברות שגויים
                return Unauthorized("אימייל או סיסמה שגויים.");
            }

            // הצלחה: קוד 200 OK
            return Ok(loggedInUser);
        }
    }
}
