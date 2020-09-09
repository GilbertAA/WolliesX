using WebAPI.ViewModels;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        public UserViewModel GetUser()
        {
            return new UserViewModel { Name = Constants.Name, Token = Constants.Token };
        }
    }
}
