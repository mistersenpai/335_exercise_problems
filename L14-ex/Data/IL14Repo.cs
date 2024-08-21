using L14_ex.Models;

namespace L14_ex.Data
{
    public interface IL14Repo
    {
        string SayHello();

        IEnumerable<User> GetUsers();

        //User AddUser(User user);


    }
}
