using Moq;
using NetworkCustomerCounter.Core.Application;
using NetworkCustomerCounter.Core.Domain.Interfaces;
using NetworkCustomerCounter.Core.Domain.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace NetworkCustomerCounter.Core.Tests.Processor
{
    [TestFixture]
    public class CustomerCountRequestProcessorTests
    {
        private Mock<INetwork> _mockNetwork;
        private Mock<IRoot> _mockRequest;
        private CustomerCountRequestProcessor _processor;

        private const string SampleRequest = @"
                    { 
                       'network':    {
                          'branches': [
                              {'startNode': 10,'endNode': 20},
                             {'startNode': 20,'endNode': 30},      
                             {'startNode': 30,'endNode': 50},            
                             {'startNode': 50,'endNode': 60},                  
                             {'startNode': 50,'endNode': 90},                  
                             {'startNode': 60,'endNode': 40},
                             {'startNode': 70,'endNode': 80}	
                          ],
                          'customers': [
                              {'node': 30,'numberOfCustomers': 8},
                             {'node': 40,'numberOfCustomers': 3},
                             {'node': 60,'numberOfCustomers': 2},         
                             {'node': 70,'numberOfCustomers': 1},                  
                             {'node': 80,'numberOfCustomers': 3},                     
                             {'node': 90,'numberOfCustomers': 5}  
                          ]
                       },
                       'selectedNode': 50
                    }
                     ";

        [SetUp]
        public void Setup()
        {
            _mockNetwork = new Mock<INetwork>();
            _processor = new CustomerCountRequestProcessor(_mockNetwork.Object);

            _mockRequest = new Mock<IRoot>();
            _mockRequest.Setup(x => x.Network)
                .Returns(JsonConvert.DeserializeObject<Root>(SampleRequest)?.Network);
        }

        [Test]
        [TestCase(10, 18)]
        [TestCase(20, 18)]
        [TestCase(30, 18)]
        [TestCase(50, 10)]
        [TestCase(90, 5)]
        [TestCase(60, 5)]
        [TestCase(70, 4)]
        [TestCase(80, 3)]
        public void ShouldReturnTotalNumberOfCustomers_UsingRequestObject(int selectedNode, int expected)
        {
            _mockRequest.Setup(x => x.SelectedNode)
                .Returns(selectedNode);

            var actual = _processor.GetCustomersForSelectedNode(_mockRequest.Object);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            var expected = "request";

            var actual = Assert.Throws<ArgumentNullException>
                (() => _processor.GetCustomersForSelectedNode(null));

            Assert.AreEqual(expected, actual.ParamName);
        }

    }
}


