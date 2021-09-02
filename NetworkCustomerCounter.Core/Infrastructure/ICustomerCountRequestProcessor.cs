using NetworkCustomerCounter.Core.Domain.Interfaces;

namespace NetworkCustomerCounter.Core.Infrastructure
{
    public interface ICustomerCountRequestProcessor
    {
        int GetCustomersForSelectedNode(IRoot request);
    }
}