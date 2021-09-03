using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetworkCustomerCounter.Core.Domain.Interfaces;

namespace NetworkCustomerCounter.Core.Domain.Models
{
    public class Root : IValidatableObject, IRoot
    {
        [Required]
        public Network Network { get; set; }
        [Required]
        public int SelectedNode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SelectedNode == 0)
                yield return new ValidationResult("Please provide a valid selected Node,cannot be 0");
        }
    }
}
