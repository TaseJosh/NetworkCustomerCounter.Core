using System.Collections.Generic;
using NetworkCustomerCounter.Core.Domain.Models;

namespace NetworkCustomerCounter.Core.Domain.Interfaces
{
    public interface INetwork
    {
        List<Branch> Branches { get; set; }
        List<Customer> Customers { get; set; }
    }
}