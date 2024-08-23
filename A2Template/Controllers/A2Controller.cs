using Microsoft.AspNetCore.Mvc;
using A2.Data;
using A2.Models;
using A2.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Xml.Linq;
using System.Security.Claims;

[Route("webapi")]
[ApiController]
public class A2Controller : Controller
{
    private readonly IA2Repo _repository;
    public A2Controller(IA2Repo repository)
    {
        _repository = repository;
    }
    [Authorize(AuthenticationSchemes = "MyAuthentication")]
    [Authorize(Policy = "normalUser")]
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

    [Authorize(AuthenticationSchemes = "MyAuthentication")]
    [Authorize(Policy = "normalUser")]
    [HttpGet("PurchaseSign/{id}")]
    public ActionResult<PurchaseOutput> PurchaseSign(string id)
    {
        string path = Directory.GetCurrentDirectory();
        string imgDir = Path.Combine(path, "SignsImages");
        string fileName1 = Path.Combine(imgDir, id + ".png");
        string fileName2 = Path.Combine(imgDir, id + ".gif");
        string fileName3 = Path.Combine(imgDir, id + ".jpg");
        string respHeader = "";
        string fileName = "";

        if (System.IO.File.Exists(fileName1))
        {
            respHeader = "image/png";
            fileName = fileName1;
        }
        else if (System.IO.File.Exists(fileName2))
        {
            respHeader = "image/gif";
            fileName = fileName2;
        }
        else if (System.IO.File.Exists(fileName3))
        {
            respHeader = "image/jpeg";
            fileName = fileName3;
        }
        else { return BadRequest($"Sign {id} not found"); }

        ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
        Claim c = ci.FindFirst("userName");
        string userName1 = c.Value;


        PurchaseOutput purchaseOutput = new PurchaseOutput{signID = fileName, userName = userName1 };


        return Ok(purchaseOutput);
    }
}