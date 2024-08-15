using Microsoft.EntityFrameworkCore;

namespace A1.Data
{
    public class A1Repo: IA1Repo
    {
        private readonly A1DbContext _dbcontext;

        public A1Repo(A1DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //api 1 - get version of api
        public string GetVersion()
        {
            string upi = "jmin479";
            string output = ($"1.0.0 (Ngāruawāhia) by {upi}");

            return output;
        }

        //api 2 - get logo of group

        //api 3 - get signs

        //api 4 - list signs

        //api 5 get image of sign

        //api 6 get comment given id

        //api 7 post comment

        //api 8 display all comments

        public void SaveChanges()
        {
            _dbcontext.SaveChanges();
        }
    }
}
