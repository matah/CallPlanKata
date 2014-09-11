using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace CallPlanKata
{
    [TestFixture]
    public class IntegrationTests
    {
        private readonly IWebService _fakeWebService;

        public IntegrationTests()
        {
            _fakeWebService = MockRepository.GenerateMock<IWebService>();
        }

        [Test]
        public void GivenAnInteractionComesIntoTheSystemWhenTheWebServiceRespondsWithAnEvenNumberThenTheInteractionGetsAssignedToAnAgentInGroupA()
        {
            var interaction = new Interaction()
            {
                Id = "bob@test.com",
                Type = InteractionType.Email
            };

            _fakeWebService.Stub(fws => fws.GetOriginatorSpecificData(Arg<Interaction>.Is.Equal(interaction))).Return(2);
        }
    }
}

