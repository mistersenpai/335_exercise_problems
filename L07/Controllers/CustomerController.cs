using Microsoft.AspNetCore.Mvc;
using L07.Data;
using L07.Dtos;
using L07.Model;

namespace L07.Controllers
{
    [Route("webapi")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IWebAPIRepo _repository;
        public CustomerController(IWebAPIRepo repository)
        {
            _repository = repository;
        }

        [HttpGet("GetCustomers")]
        public ActionResult <IEnumerable<CustomerOutDto>> GetCustomers()
        {
            IEnumerable<Customer> customers = _repository.GetAllCustomers();
            IEnumerable<CustomerOutDto> c = customers.Select(e => new CustomerOutDto
            { Id = e.Id, FirstName = e.FirstName, LastName = e.LastName });

            return Ok(c);

        }

        // GET /webapi/GetCustomer/{id}
        [HttpGet("GetCustomer/{id}")]
        public ActionResult<CustomerOutDto> GetCustomer(int id)
        {
            Customer customer = _repository.GetCustomerByID(id);
            if (customer == null)
                return NotFound();
            else
            {
                CustomerOutDto c = new CustomerOutDto { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName };
                return Ok(c);
            } 

        }

        //Post reuest
        [HttpPost("AddCustomer")]
        public ActionResult<CustomerOutDto> AddCustomer(CustomerInputDto customer)
        {
            Customer c = new Customer { FirstName = customer.FirstName, LastName = customer.LastName, Email = customer.Email };
            Customer addedCustomer = _repository.AddCustomer(c);
            CustomerOutDto co = new CustomerOutDto { Id = addedCustomer.Id, FirstName = addedCustomer.FirstName, LastName = addedCustomer.LastName };
            return CreatedAtAction(nameof(GetCustomer), new { id = co.Id }, co);
        }
    }
}
