using Microsoft.EntityFrameworkCore;
using A1.Model;
using System.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

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
        public IEnumerable<Sign> AllSigns()
        {
            IEnumerable<Sign> signs = _dbcontext.Signs.ToList();         
            return signs;
        }

        //api 4 - list signs
        public IEnumerable<Sign> Signs(string text) 
        {
            //Enter into cheat sheet
            IEnumerable<Sign> desiredOutput = _dbcontext.Signs.Where(e => e.Description.ToLower().Contains(text)).ToList();


            return desiredOutput;
        }

        //api 5 get image of sign

        //api 6 get comment given id
        public Comment GetComment(int id)
        {
            Comment comment = _dbcontext.Comments.FirstOrDefault(e => e.Id == id);
            return comment;
        }

        //api 7 post comment
        public Comment WriteComment(Comment comment) 
        {
            _dbcontext.Comments.Add(comment);
            _dbcontext.SaveChanges();
            return comment;
        }

        public IEnumerable<Comment> Comments(int numOfComments) 
        {
            IEnumerable<Comment> comments = _dbcontext.Comments.OrderByDescending(c => c.Id).ToList<Comment>();
            if (numOfComments >= 0)
            {
                if (numOfComments > comments.Count())
                {
                    return comments;
                }
                else
                {
                    return comments.Take(numOfComments);
                }
            }
            else
            {
                return comments.Take(0);
            }
        }

        //api 8 display all comments

        public void SaveChanges()
        {
            _dbcontext.SaveChanges();
        }
    }
}
