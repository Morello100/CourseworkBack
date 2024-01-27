using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mongodb_dotnet_example.Models;
using mongodb_dotnet_example.Services;
using System.Collections.Generic;
using BCrypt.Net;

namespace mongodb_dotnet_example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _userService;
        private readonly SelectedProductsService _selectedProductsService;

        public UsersController(UsersService userService,SelectedProductsService selectedProductsService)
        {
            _userService = userService;
            _selectedProductsService = selectedProductsService;
        }

        

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _userService.Get();

            foreach (var user in users)
            {
                user.Password = null;
            }

            return users.ToList();
        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Password = null;
            return user;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Не вдалося знайти дані користувача.");
            }

            var existingUser = _userService.FindUserByEmail(user.Email);

            if (existingUser != null)
            {
                return BadRequest("Користувач з такою електронною адресою вже існує.");
            }

            user.Password = HashPassword(user.Password);

            var createdUser = _userService.Create(user);
            _selectedProductsService.Create(createdUser.Id);
            return Ok("Користувач успішно зареєстрований.");
        }

[HttpPost("login")]
public IActionResult Login([FromBody] UserLogin userLogin)
{
    if (userLogin == null)
    {
        return BadRequest("Не вдалося знайти дані користувача.");
    }

    var existingUser = _userService.FindUserByEmail(userLogin.Email);

    if (existingUser == null || !VerifyPassword(userLogin.Password, existingUser.Password))
    {
        return Unauthorized();
    }

    existingUser.Password = null;

    return Ok(existingUser);
}




        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] User userIn)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            userIn.Password = null;

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Delete(user.Id);

            return NoContent();
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }
    }
}
