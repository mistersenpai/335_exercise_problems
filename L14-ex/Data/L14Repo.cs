using L14_ex.Models;

namespace L14_ex.Data

{
    public class L14Repo: IL14Repo
    {
        private readonly L14DbContext _dbContext;
        public L14Repo(L14DbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public string SayHello()
        {
            string word = ("Hello, World!");

            return word;
        }


        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _dbContext.Users.ToList();
            return users;
        }

        //public User AddUser(User user) 
        //{ 
        //    _dbContext.Users.Add(user);
        //    _dbContext.SaveChanges();

        //    return user;
        //}

        public bool ValidLogin(string username, string password) 
        {
            User c = _dbContext.Users.FirstOrDefault(e => e.UserName == username && e.Password == password);

            if (c == null)
            {
                return false;
            }
            else {return true;}
        }
    }
}
