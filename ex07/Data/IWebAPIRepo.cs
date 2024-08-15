using ex07.Model;

namespace ex07.Data
{
    public interface IWebAPIRepo
    {
        IEnumerable<Phone> GetPhoneByModelName(string modelName);

        Phone AddPhone(Phone phone);

        
    }
}
