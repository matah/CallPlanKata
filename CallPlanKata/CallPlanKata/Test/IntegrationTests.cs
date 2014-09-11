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

        [TestCase(2)]
        [TestCase(4)]
        public void GivenAnInteractionComesIntoTheSystemWhenTheWebServiceRespondsWithAnEvenNumberThenTheInteractionGetsAssignedToAnAgentInGroupA(int webServiceResponse)
        {
            var interaction = new Interaction()
            {
                Id = "bob@test.com",
                Type = InteractionType.email
            };

            var getOrginatorSpecificDataStep = new GetOriginatorSpecificDataStep(_fakeWebService);
            var agentAssigningStep = new AgentAssigningStep();

            var callPlan = new CallPlan();
            callPlan.AppendStep(getOrginatorSpecificDataStep);
            callPlan.AppendStep(agentAssigningStep);

            _fakeWebService.Stub(fws => fws.GetOriginatorSpecificData(Arg<Interaction>.Is.Equal(interaction))).Return(webServiceResponse);

            callPlan.ReceiveInteraction(interaction);

            var result = callPlan.PrintSummary();

            StringAssert.Contains("Receive email from \"bob@test.com\"", result);
            StringAssert.Contains("Invoke function with \"bob@test.com\", response is \""+webServiceResponse+"\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"1\"", result);
        }

        [TestCase(3)]
        [TestCase(1)]
        public void GivenAnInteractionComesIntoTheSystemWhenTheWebServiceRespondsWithAnOddNumberThenTheInteractionGetsAssignedToAnAgentInGroupB(int webServiceResponse)
        {
            var interaction = new Interaction()
            {
                Id = "bob@test.com",
                Type = InteractionType.email
            };

            var getOrginatorSpecificDataStep = new GetOriginatorSpecificDataStep(_fakeWebService);
            var agentAssigningStep = new AgentAssigningStep();

            var callPlan = new CallPlan();
            callPlan.AppendStep(getOrginatorSpecificDataStep);
            callPlan.AppendStep(agentAssigningStep);

            _fakeWebService.Stub(fws => fws.GetOriginatorSpecificData(Arg<Interaction>.Is.Equal(interaction))).Return(webServiceResponse);

            callPlan.ReceiveInteraction(interaction);

            var result = callPlan.PrintSummary();

            StringAssert.Contains("Receive email from \"bob@test.com\"", result);
            StringAssert.Contains("Invoke function with \"bob@test.com\", response is \""+webServiceResponse+"\"", result);
            StringAssert.Contains("Deliver to group \"B\" and Agent \"1\"", result);
        }

        [Test]
        public void GivenAnInteractionComesIntoTheSystemWhenTheWebServiceRespondsWithAnErrorThenTheInteractionGetsAssignedToAnAgentInGroupC()
        {
            var interaction = new Interaction()
            {
                Id = "bob@test.com",
                Type = InteractionType.email
            };

            var getOrginatorSpecificDataStep = new GetOriginatorSpecificDataStep(_fakeWebService);
            var agentAssigningStep = new AgentAssigningStep();

            var callPlan = new CallPlan();
            callPlan.AppendStep(getOrginatorSpecificDataStep);
            callPlan.AppendStep(agentAssigningStep);

            _fakeWebService.Stub(fws => fws.GetOriginatorSpecificData(Arg<Interaction>.Is.Equal(interaction))).Throw(new Exception());

            callPlan.ReceiveInteraction(interaction);

            var result = callPlan.PrintSummary();

            StringAssert.Contains("Receive email from \"bob@test.com\"", result);
            StringAssert.Contains("Invoke function with \"bob@test.com\", response is \"Error Xyz\"", result);
            StringAssert.Contains("Deliver to group \"C\" and Agent \"1\"", result);
        }
    }
}

