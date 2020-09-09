using WebAPI.Entity;

namespace WebAPI.Services
{
    public interface ITrolleyService
    {
        double CalculateTrolley(TrolleyInput input);
    }
}