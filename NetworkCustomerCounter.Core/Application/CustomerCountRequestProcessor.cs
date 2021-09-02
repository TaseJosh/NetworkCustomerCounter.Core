using NetworkCustomerCounter.Core.Domain.Interfaces;
using NetworkCustomerCounter.Core.Infrastructure;
using System;
using System.Linq;

namespace NetworkCustomerCounter.Core.Application
{
    public class CustomerCountRequestProcessor : ICustomerCountRequestProcessor
    {
        private INetwork _network;
        public CustomerCountRequestProcessor(INetwork network)
        {
            _network = network ??
                throw new ArgumentNullException(nameof(network));
        }
        public int GetCustomersForSelectedNode(IRoot request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

                _network = request.Network;
            return ProcessSelectedNode(request.SelectedNode);
        }


        private int ProcessSelectedNode(int node)
        {
            var nc = _network.Customers.FirstOrDefault(c => c.Node == node);
            var customersForNode = nc != null ? nc.NumberOfCustomers : 0;

            foreach (var subNode in _network.Branches.Where(b => b.StartNode == node))
            {
                customersForNode += ProcessSelectedNode(subNode.EndNode);
            }
            return customersForNode;
        }


    }
}