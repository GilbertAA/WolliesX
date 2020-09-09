using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        [HttpGet]
        public UserViewModel GetUser()
        {
            return _userService.GetUser();
        }
    }
}
