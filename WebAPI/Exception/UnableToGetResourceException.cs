namespace WebAPI.Exception
{
    public class UnableToGetResourceException : System.Exception
    {
        public UnableToGetResourceException(string message) : base(message)
        {
        }
    }
}
