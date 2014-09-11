using System;
using NUnit.Framework;

namespace CallPlanKata
{
    [TestFixture]
    public class HappyPathTests
    {
        private readonly IWebService _fakeWebService;

        public HappyPathTests()
        {
            _fakeWebService = new FakeWebService();
        }

        [Test]
        public void HappyPathTest()
        {
            var interaction = new Interaction()
            {
                Id = "bob@test.com",
                Type = InteractionType.Email
            };

            var response = _fakeWebService.GetOriginatorSpecificData(interaction);

            var callPlan = new CallPlan();

            var getOriginatorSpecificDataStep = new GetOriginatorSpecificDataStep(_fakeWebService);
            var agentAssigningStep = new AgentAssigningStep();

            callPlan.AppendStep(getOriginatorSpecificDataStep);
            callPlan.AppendStep(agentAssigningStep);

            callPlan.ReceiveInteraction(interaction);

            var summary = callPlan.PrintSummary();
        }
    }
}

