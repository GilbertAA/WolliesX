using WebAPI.ViewModels;

namespace WebAPI.Services
{
    public interface IUserService
    {
        UserViewModel GetUser();
    }
}