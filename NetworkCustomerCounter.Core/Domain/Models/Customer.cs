using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetworkCustomerCounter.Core.Domain.Models
{

    public class Customer : IValidatableObject
    {
        [Required]
        public int Node { get; set; }
        [Required]
        public int NumberOfCustomers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Node == 0)
                yield return new ValidationResult("Please provide valid Node values, cannot be 0");

            if (NumberOfCustomers == 0)
                yield return new ValidationResult("Please provide a valid customer count, cannot be 0");
        }
    }

}
