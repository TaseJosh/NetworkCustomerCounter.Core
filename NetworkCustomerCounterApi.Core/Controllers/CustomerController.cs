using Microsoft.AspNetCore.Mvc;
using NetworkCustomerCounter.Core.Domain.Models;
using NetworkCustomerCounter.Core.Infrastructure;
using System;

namespace NetworkCustomerCounterApi.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerCountRequestProcessor _processor;

        public CustomerController(ICustomerCountRequestProcessor processor)
        {
            _processor = processor ??
                throw new ArgumentNullException(nameof(processor));
        }

        [HttpPost]
        public IActionResult GetCustomersForSelectedNode(Root request)
        {
            var result = _processor.GetCustomersForSelectedNode(request);

            return Ok(result);
        }
    }
}
