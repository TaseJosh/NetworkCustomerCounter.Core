using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetworkCustomerCounter.Core.Domain.Interfaces;

namespace NetworkCustomerCounter.Core.Domain.Models
{

    public class Network : INetwork
    {
        [Required]
        public List<Branch> Branches { get; set; }
        [Required]
        public List<Customer> Customers { get; set; }

    }



}
