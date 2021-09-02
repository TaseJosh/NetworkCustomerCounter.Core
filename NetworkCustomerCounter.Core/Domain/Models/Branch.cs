using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetworkCustomerCounter.Core.Domain.Models
{

    public class Branch : IValidatableObject
    {
        [Required]
        public int StartNode { get; set; }
        [Required]
        public int EndNode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartNode == 0)
            { yield return new ValidationResult("Please valid Start Node values, cannot be 0"); }

            if (EndNode == 0)
            { yield return new ValidationResult("Please valid End Node values, cannot be 0"); }
        }
    }



}
