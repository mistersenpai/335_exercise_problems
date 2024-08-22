using A2.Models;

namespace A2.Data
{
    public interface IA2Repo
    {
        string sayHello();

        User Register(User user);
    }
}