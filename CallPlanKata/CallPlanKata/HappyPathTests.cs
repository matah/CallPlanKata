using System;
using NUnit.Framework;

namespace CallPlanKata
{
    [TestFixture]
    public class HappyPathTests
    {
        private readonly IWebService _fakeWebService;
        private readonly AgentAssigner _agentAssigner;

        public HappyPathTests()
        {
            _fakeWebService = new FakeWebService();
            _agentAssigner = new AgentAssigner();
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

            var result = _agentAssigner.AssignAgentToInteractionFromResponse(interaction, response);
        }
    }
}

