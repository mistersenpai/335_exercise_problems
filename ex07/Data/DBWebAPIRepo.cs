using ex07.Model;
using ex07.Data;

namespace ex07
{
    public class DBWebAPIRepo: IWebAPIRepo
    {
        private readonly WebAPIDBContext _dbContext;

        //Constructor
        public DBWebAPIRepo(WebAPIDBContext dbContext) { 
            _dbContext = dbContext;
        }

        public IEnumerable<Phone> GetPhoneByModelName(string modelName) {
            IEnumerable<Phone> phones = _dbContext.Phones.ToList();

            IEnumerable<Phone> eee = phones.Where(e => e.ModelName == modelName); //

            return eee;

        }

        public Phone AddPhone(Phone phone) {

            return phone;
        }


    }
}
