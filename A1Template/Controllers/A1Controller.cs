using Microsoft.AspNetCore.Mvc;
using A1.Data;
using A1.Model;


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
            return PhysicalFile(fileName,respHeader);
        }

        //api 3 - get signs

        //api 4 - list signs

        //api 5 get image of sign

        //api 6 get comment given id

        //api 7 post comment

        //api 8 display all comments
    }







}


