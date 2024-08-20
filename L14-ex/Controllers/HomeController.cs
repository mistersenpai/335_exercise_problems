using Microsoft.AspNetCore.Mvc;
using L14_ex.Models;
using L14_ex.Data;

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
       
    }
}
