using Microsoft.AspNetCore.Mvc;
using A2.Data;
using A2.Models;

[Route("webapi")]
[ApiController]
public class A2Controller : Controller
{
    private readonly IA2Repo _repository;
    public A2Controller(IA2Repo repository)
    {
        _repository = repository;
    }
    [HttpGet("SayHello")]
    public ActionResult SayHello()
    {
        string output = _repository.sayHello();

        return Ok(output);
    }

    [HttpPost("Register")]
    public ActionResult<User> Register(User user)
    {
        User u = new User { UserName = user.UserName, Password = user.Password, Address = user.Address };

        User addedUser = _repository.Register(u);

        if (addedUser == null)
        {
            return Ok($"Username {user.UserName} is not available.");
        }

        return CreatedAtAction(nameof(Register), u, "User succesfully registered.");
    }
}