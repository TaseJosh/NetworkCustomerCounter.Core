using Microsoft.AspNetCore.Mvc;
using NetworkCustomerCounter.Core.Domain.Models;
using NetworkCustomerCounter.Core.Infrastructure;
using System;

namespace NetworkCustomerCounterApi.Core.Controllers
{
    [ApiController]
    [Route("api/total-customers")]
    public class TotalCustomersController : ControllerBase
    {
        private readonly ICustomerCountRequestProcessor _processor;

        public TotalCustomersController(ICustomerCountRequestProcessor processor)
        {
            _processor = processor ??
                throw new ArgumentNullException(nameof(processor));
        }

        [HttpPost]
        public IActionResult ProcessRequest(Root request)
        {
            var result = _processor.GetCustomersForSelectedNode(request);

            return Ok(result);
        }
    }
}
