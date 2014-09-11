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

            var getOrginatorSpecificDataStep = new GetOriginatorSpecificDataStep(_fakeWebService);
            var agentAssigningStep = new AgentAssigningStep();

            var callPlan = new CallPlan();
            callPlan.AppendStep(getOrginatorSpecificDataStep);
            callPlan.AppendStep(agentAssigningStep);

            callPlan.ReceiveInteraction(interaction);

            var result = callPlan.PrintSummary();

            StringAssert.Contains("Receive email from \"bob@test.com\"", result);
            StringAssert.Contains("Invoke function with \"bob@test.com\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"1\"", result);
        }
    }
}

