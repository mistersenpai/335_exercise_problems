using Microsoft.AspNetCore.Mvc;
using L14_ex.Models;
using L14_ex.Data;
using L14_ex.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace L14_ex.Controllers
{
    [Route("webapi")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IL14Repo _repository;
        public HomeController(IL14Repo repository)
        {
            _repository = repository;
        }

        [HttpGet("SayHello")] 
        public ActionResult SayHello()
        {
            string word = _repository.SayHello();

            return Ok(word);
        }

        [Authorize(AuthenticationSchemes = "AdminAuthentication")]
        [Authorize(Policy ="AdminOnly")]
        [HttpGet("GetUsers")]
        public ActionResult<IEnumerable<UserOutput>> GetUsers()
        {
            IEnumerable<User> users = _repository.GetUsers();
            IEnumerable<UserOutput> uOut = users.Select(e =>
                new UserOutput { UserName = e.UserName, Position = e.Position}

            );

            return Ok(uOut);
        }

        [HttpGet("makewebsite")]
        public ContentResult get335()
        {
            string cs335 = "<html><body>This is cs335<h1> sugma nutts</h1></body></html>";



            ContentResult content = new ContentResult { Content = cs335, ContentType = "text/html", StatusCode = (int)HttpStatusCode.OK };
            return content;
        }

        //[HttpPost("Add User")]
        //public ActionResult<UserInput> WriteUser(UserInput user)
        //{

        //    User newUser = new User { UserName = user.UserName, Password = user.Password, Position = user.Position };
        //    User addUser = _repository.AddUser(newUser);

        //    return CreatedAtAction(newUser.UserName, newUser, addUser);
        //}


    }
}
