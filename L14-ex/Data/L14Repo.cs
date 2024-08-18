namespace L14_ex.Data
{
    public class L14Repo: IL14Repo
    {
        private readonly L14DbContext _dbContext;
        public L14Repo(L14DbContext dbContext) 
        { 
            _dbContext = dbContext;
        }
    }
}
