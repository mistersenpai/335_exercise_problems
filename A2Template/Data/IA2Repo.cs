using A2.Models;

namespace A2.Data
{
    public interface IA2Repo
    {
        string sayHello();

        bool ValidLogin(string username, string password);

        bool ValidOrganizer(string username, string password);

        User Register(User user);

        Sign CheckSign(string id);

        Event AddEvent(Event toAddEvent);

        bool IsDateValid(string date);

        int EventCount();

        Event Event(int id);

        // string PurchaseSign(string SignID);
    }
}