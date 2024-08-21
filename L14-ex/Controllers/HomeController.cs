using Microsoft.AspNetCore.Mvc;
using L14_ex.Models;
using L14_ex.Data;
using L14_ex.Dtos;

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

        [HttpGet("GetUsers")]
        public ActionResult GetUsers()
        {
          IEnumerable<User> users = _repository.GetUsers();

            return Ok(users);
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
