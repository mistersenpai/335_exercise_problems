using A1.Model;


namespace A1.Data
{
    public interface IA1Repo
    {
        //api 1 - get version of api
        string GetVersion();

        //api 2 - get logo of group

        //api 3 - get signs
        IEnumerable<Sign> AllSigns();

        //api 4 - list signs
        IEnumerable<Sign> Signs(string text);

        //api 5 get image of sign

        //api 6 get comment given id
        Comment GetComment(int id);

        //api 7 post comment
        Comment WriteComment(Comment comment);

        //api 8 display all comments
    }
}