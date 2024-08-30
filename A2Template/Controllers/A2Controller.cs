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
    //[Authorize(AuthenticationSchemes = "MyAuthentication")]
    //[Authorize(Policy = "normalUser")]
    //[HttpGet("SayHello")]
    //public ActionResult SayHello()
    //{
    //    string output = _repository.sayHello();

    //    return Ok(output);
    //}

    [HttpPost("Register")]
    public ActionResult<User> Register(User user)
    {
        User u = new User { UserName = user.UserName, Password = user.Password, Address = user.Address };

        User addedUser = _repository.Register(u);

        if (addedUser == null)
        {
            return Ok($"UserName {user.UserName} is not available.");
        }

        CreatedAtAction(nameof(Register), u, "User succesfully registered.");
        return Ok("User succesfully registered.");
    }

    [Authorize(AuthenticationSchemes = "MyAuthentication")]
    [Authorize(Policy = "normalUser")]
    [HttpGet("PurchaseSign/{id}")]
    public ActionResult<PurchaseOutput> PurchaseSign(string id)
    {
        //check sign first
        Sign doesExist = _repository.CheckSign(id);
        if (doesExist == null)
        {
            return BadRequest($"Sign {id} not found");
        }

        ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
        Claim claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        string userName1 = claim.Value.ToString();


        PurchaseOutput purchaseOutput = new PurchaseOutput{signID = id, userName = userName1 };


        return Ok(purchaseOutput);
    }

    [Authorize(AuthenticationSchemes = "MyAuthentication")]
    [Authorize(Policy = "organizer")]
    [HttpPost("AddEvent")]
    public ActionResult<Event> AddEvent(EventInput userEvent)
    {

        Claim claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        if (claim.GetType().ToString() == "normalUser")
        {
            return Forbid();
        }
        // check start and end date
        if (!(_repository.IsDateValid(userEvent.Start)) && !(_repository.IsDateValid(userEvent.End)))
        {
            return BadRequest("Start and End Date are not valid datetime objects");
        }
        
        // check start 
        else if (!(_repository.IsDateValid(userEvent.Start)))
        {
            return BadRequest("Start Date are not valid datetime objects");
        }
        // check end 
        if (!(_repository.IsDateValid(userEvent.End)))
        {
            return BadRequest("End Date are not valid datetime objects");
        }

        Event e = new Event { Description = userEvent.Description, End = userEvent.End, Summary = userEvent.Summary, Location = userEvent.Location, Start = userEvent.Start };

        Event toadd = _repository.AddEvent(e);

        CreatedAtAction(nameof(AddEvent),new {id = toadd.Id}, toadd);
        return Ok("Success");

    }

    [Authorize(AuthenticationSchemes = "MyAuthentication")]
    [Authorize(Policy = "bothUsers")]
    [HttpGet("EventCount")]
    public ActionResult<int> EventCount()
    {
        int countEvents = _repository.EventCount();

        return Ok(countEvents);
    }

    [Authorize(AuthenticationSchemes = "MyAuthentication")]
    [Authorize(Policy = "bothUsers")]
    [HttpGet("Event/{id}")]
    public ActionResult<Event> Event(int id)
    {

        Event _event = _repository.Event(id);

        if (_event == null)
        {
            return BadRequest($"Event {id} does not exist.");
        }
        Response.Headers.Add("Content-Type", "text/vcard");


        return Ok(_event);

    }
}