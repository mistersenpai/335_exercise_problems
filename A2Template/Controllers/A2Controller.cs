using Microsoft.AspNetCore.Mvc;
using A2.Data;

[Route("webapi")]
[ApiController]
public class A2Controller: Controller
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


}