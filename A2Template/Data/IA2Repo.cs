using A2.Models;

namespace A2.Data
{
    public interface IA2Repo
    {
        string sayHello();

        Boolean ValidLogin(string username, string password);

        User Register(User user);

        // string PurchaseSign(string SignID);
    }
}