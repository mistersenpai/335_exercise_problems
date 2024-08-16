using Microsoft.AspNetCore.Mvc;
using A1.Data;
using A1.Model;
using A1.Dtos;


namespace A1.Controllers
{
    [Route("webapi")]

    [ApiController]
    public class A1Controller : Controller
    {
        private readonly IA1Repo _repository;
        public A1Controller(IA1Repo repository)
        {
            _repository = repository;
        }

        // api 1 - get version of api
        [HttpGet("GetVersion")]
        public ActionResult GetVersion()
        {
            string text = _repository.GetVersion();

            return Ok(text);
        }

        //api 2 - get logo of group
        [HttpGet("Logo")]
        public ActionResult Logo() {

            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "Logos");

            // Bold assumption this will always be a png lol
            string fileName1 = Path.Combine(imgDir, "Logo.png");
            string respHeader = "";
            string fileName = "";

            // Check if it exists
            if (System.IO.File.Exists(fileName1))
            {
                respHeader = "image/png";
                fileName = fileName1;
            }
            else
            {
                return NotFound("File not found");
            }
            return PhysicalFile(fileName, respHeader);
        }

        //api 3 - get signs
        [HttpGet("AllSigns")]
        public ActionResult AllSigns()
        {
            IEnumerable<Sign> signs = _repository.AllSigns();
            return Ok(signs);
        }

        //api 4 - list signs
        [HttpGet("Signs/{text}")]
        public ActionResult Signs(string text)
        {
            IEnumerable<Sign> signs = _repository.Signs(text);

            return Ok(signs);
        }


        //api 5 get image of sign
        [HttpGet(("SignImage/{name}"))]
        public ActionResult SignImage(string name)
        {
            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "SignsImages");
            string filename1 = Path.Combine(imgDir, name + ".png");
            string filename2 = Path.Combine(imgDir, name + ".jpg");
            string filename3 = Path.Combine(imgDir, name + ".gif");
            string respHeader = "";
            string fileName = "";

            // Check if it exists
            if (System.IO.File.Exists(filename1))
            {
                respHeader = "image/png";
                fileName = filename1;
            } 
            else if (System.IO.File.Exists(filename2))
            {
                respHeader = "image/jpg";
                fileName = filename2;
            }
            else if (System.IO.File.Exists(filename3))
            {
                respHeader = "image/gif";
                fileName = filename3;
            }
            else
            {
                respHeader = "image/png";
                fileName = Path.Combine(imgDir, "default.png");

                return PhysicalFile(fileName, respHeader);
            }

            return PhysicalFile(fileName, respHeader);



        }

        //api 6 get comment given id
        [HttpGet("GetComment/{id}")]
        public ActionResult GetComment(int id)
        {
            Comment comment = _repository.GetComment(id);
            if (comment != null)
            {
                Comment input = new Comment {  Id = comment.Id, Time = comment.Time, UserComment = comment.UserComment, Name = comment.Name, IP = comment.IP};
                return Ok(input);
            }
            else 
            {
                return BadRequest($"Comment {id} does not exist.");
            }
        }

        //api 7 post comment
        [HttpPost("WriteComment")]
        public ActionResult<CommentInput> WriteComment(CommentInput comment)
        {
            var myIP = (Request.HttpContext.Connection.RemoteIpAddress).ToString();
            var time = DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ");
            Comment comm = new Comment { UserComment = comment.UserComment, Name = comment.Name, Time = time, IP= myIP };
            Comment addedComment = _repository.WriteComment(comm);


            return CreatedAtAction(nameof(GetComment), new {id = addedComment.Id}, addedComment);
        }

        //api 8 display all comments
        [HttpGet("Comments/{numOfComments}")]
        public ActionResult Comments(int numOfComments) 
        { 
            IEnumerable<Comment> comments = _repository.Comments(numOfComments);
            return Ok(comments);
        }
    }
    // 027 775 2501






}


