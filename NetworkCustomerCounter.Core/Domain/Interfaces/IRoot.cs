using NetworkCustomerCounter.Core.Domain.Models;

namespace NetworkCustomerCounter.Core.Domain.Interfaces
{
    public interface IRoot
    {
        Network Network { get; set; }
        int SelectedNode { get; set; }
    }
}