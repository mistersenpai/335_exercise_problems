using A2.Models;

namespace A2.Data
{
    public class A2Repo : IA2Repo
    {
        private readonly A2DbContext _dbContext;
        public A2Repo(A2DbContext dbContext)
        { 
            _dbContext = dbContext;
        }

        public string sayHello() 
        {
            return "hello";
        }

        public bool ValidLogin(string username, string password) 
        { 
            User check = _dbContext.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (check == null) 
            {
                return false;
            }

            return true;

        }

        public bool ValidOrganizer(string username, string password)
        {
            Organizer check = _dbContext.Organizers.FirstOrDefault(u => u.Name == username && u.Password == password);

            if (check == null)
            {
                return false;
            }

            return true;

        }

        public User Register(User user)
        {
            //must be null
            User CheckExists = _dbContext.Users.FirstOrDefault(u => u.UserName == user.UserName);
            if (CheckExists != null) 
            {
                return null;
            }

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }

    }
}