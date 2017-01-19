namespace ShoppingCart.UI.Authentication
{
    public interface IUserAuthenticator
    {
        bool IsValid(string username, string password);
    }
}