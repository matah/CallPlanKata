using System;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;

namespace CallPlanKata
{
    [TestFixture]
    public class IntegrationTests
    {
        private IWebService _fakeWebService;
        private AgentsService _agentsService;

        [SetUp]
        public void TestSetUp()
        {
            _fakeWebService = MockRepository.GenerateMock<IWebService>();
            SetUpAgentGroupsAndAgents();
        }

        [TestCase(2)]
        [TestCase(4)]
        public void GivenAnInteractionComesIntoTheSystemWhenTheWebServiceRespondsWithAnEvenNumberThenTheInteractionGetsAssignedToAnAgentInGroupA(int webServiceResponse)
        {
            var interaction = CreateTestEmailInteraction();


            _fakeWebService.Stub(fws => fws.GetOriginatorSpecificData(Arg<Interaction>.Is.Equal(interaction))).Return(webServiceResponse);

            var callPlan = ConfigureTestCallPlan();


            callPlan.ReceiveInteraction(interaction);

            var result = callPlan.PrintSummary(interaction);

            StringAssert.Contains("Receive email from \"bob@test.com\"", result);
            StringAssert.Contains("Invoke function with \"bob@test.com\", response is \"" + webServiceResponse + "\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"1\"", result);
        }

        [TestCase(3)]
        [TestCase(1)]
        public void GivenAnInteractionComesIntoTheSystemWhenTheWebServiceRespondsWithAnOddNumberThenTheInteractionGetsAssignedToAnAgentInGroupB(int webServiceResponse)
        {
            var interaction = CreateTestEmailInteraction();

            _fakeWebService.Stub(fws => fws.GetOriginatorSpecificData(Arg<Interaction>.Is.Equal(interaction))).Return(webServiceResponse);

            var callPlan = ConfigureTestCallPlan();


            callPlan.ReceiveInteraction(interaction);

            var result = callPlan.PrintSummary(interaction);

            StringAssert.Contains("Receive email from \"bob@test.com\"", result);
            StringAssert.Contains("Invoke function with \"bob@test.com\", response is \""+webServiceResponse+"\"", result);
            StringAssert.Contains("Deliver to group \"B\" and Agent \"1\"", result);
        }

        [Test]
        public void GivenAnInteractionComesIntoTheSystemWhenTheWebServiceRespondsWithAnErrorThenTheInteractionGetsAssignedToAnAgentInGroupC()
        {
            var interaction = CreateTestEmailInteraction();
            _fakeWebService.Stub(fws => fws.GetOriginatorSpecificData(Arg<Interaction>.Is.Equal(interaction))).Throw(new Exception());

            var callPlan = ConfigureTestCallPlan();



            callPlan.ReceiveInteraction(interaction);

            var result = callPlan.PrintSummary(interaction);

            StringAssert.Contains("Receive email from \"bob@test.com\"", result);
            StringAssert.Contains("Invoke function with \"bob@test.com\", response is \"Error Xyz\"", result);
            StringAssert.Contains("Deliver to group \"C\" and Agent \"1\"", result);
        }

        [Test]
        public void GivenAnInteractionComesIntoTheSystemWhenTheWebServiceRespondsWithATimeoutThenTheInteractionGetsAssignedToAnAgentInGroupC()
        {
            var interaction = CreateTestEmailInteraction();

            _fakeWebService.Stub(fws => fws.GetOriginatorSpecificData(Arg<Interaction>.Is.Equal(interaction))).Throw(new TimeoutException());

            var callPlan = ConfigureTestCallPlan();

            callPlan.ReceiveInteraction(interaction);

            var result = callPlan.PrintSummary(interaction);

            StringAssert.Contains("Receive email from \"bob@test.com\"", result);
            StringAssert.Contains("Invoke function with \"bob@test.com\", response is \"Time-out\"", result);
            StringAssert.Contains("Deliver to group \"C\" and Agent \"1\"", result);
        }

        [Test]
        public void GivenFourCallInteractionsWhenWebServiceRespondsWithEvenNumberEverytimeThenEachInteractionGetsAssignedToDifferentAgentsUptoThreeAgents()
        {
            var interaction1 = CreateTestCallInteraction();
            var interaction2 = CreateTestCallInteraction();
            var interaction3 = CreateTestCallInteraction();
            var interaction4 = CreateTestCallInteraction();

            _fakeWebService.Stub(fws => fws.GetOriginatorSpecificData(Arg<Interaction>.Is.Anything)).Return(2);

            var callPlan = ConfigureTestCallPlan();



            callPlan.ReceiveInteraction(interaction1);
            callPlan.ReceiveInteraction(interaction2);
            callPlan.ReceiveInteraction(interaction3);
            callPlan.ReceiveInteraction(interaction4);

            var result = callPlan.PrintSummary(interaction1);

            StringAssert.Contains("Receive call from \"" + interaction1.Id + "\"", result);
            StringAssert.Contains("Invoke function with \"" + interaction1.Id + "\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"1\"", result);

            result = callPlan.PrintSummary(interaction2);

            StringAssert.Contains("Receive call from \"" + interaction2.Id + "\"", result);
            StringAssert.Contains("Invoke function with \"" + interaction2.Id + "\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"2\"", result);

            result = callPlan.PrintSummary(interaction3);

            StringAssert.Contains("Receive call from \"" + interaction3.Id + "\"", result);
            StringAssert.Contains("Invoke function with \"" + interaction3.Id + "\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"3\"", result);

            result = callPlan.PrintSummary(interaction4);

            StringAssert.Contains("Receive call from \"" + interaction4.Id + "\"", result);
            StringAssert.Contains("Invoke function with \"" + interaction4.Id + "\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" all agents are busy", result);
        }

        [Test]
        public void GivenACallInteractionAndAnEmailInteractionWhenWebServiceRespondsWithEvenNumberThenEachInteractionGetsAssignedToSameAgent()
        {
            var emailInteraction = CreateTestEmailInteraction();
            var callInteraction = CreateTestCallInteraction();

            _fakeWebService.Stub(fws => fws.GetOriginatorSpecificData(Arg<Interaction>.Is.Anything)).Return(2);

            var callPlan = ConfigureTestCallPlan();

            callPlan.ReceiveInteraction(emailInteraction);
            callPlan.ReceiveInteraction(callInteraction);

            var result = callPlan.PrintSummary(emailInteraction);

            StringAssert.Contains("Receive email from \"" + emailInteraction.Id + "\"", result);
            StringAssert.Contains("Invoke function with \"" + emailInteraction.Id + "\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"1\"", result);

            result = callPlan.PrintSummary(callInteraction);

            StringAssert.Contains("Receive call from \"" + callInteraction.Id + "\"", result);
            StringAssert.Contains("Invoke function with \"" + callInteraction.Id + "\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"1\"", result);
        }

        [Test]
        public void GivenASixEmailInteractionsWhenWebServiceRespondsWithEvenNumberThenEachFiveInteractionsGetsAssignedToFirstAgentAndLastGetsAssignedToAnotherAgent()
        {
            var emailInteraction1 = CreateTestEmailInteraction();
            var emailInteraction2 = CreateTestEmailInteraction();
            var emailInteraction3 = CreateTestEmailInteraction();
            var emailInteraction4 = CreateTestEmailInteraction();
            var emailInteraction5 = CreateTestEmailInteraction();
            var emailInteraction6 = CreateTestEmailInteraction();

            _fakeWebService.Stub(fws => fws.GetOriginatorSpecificData(Arg<Interaction>.Is.Anything)).Return(2);

            var callPlan = ConfigureTestCallPlan();

            callPlan.ReceiveInteraction(emailInteraction1);
            callPlan.ReceiveInteraction(emailInteraction2);
            callPlan.ReceiveInteraction(emailInteraction3);
            callPlan.ReceiveInteraction(emailInteraction4);
            callPlan.ReceiveInteraction(emailInteraction5);
            callPlan.ReceiveInteraction(emailInteraction6);

            var result = callPlan.PrintSummary(emailInteraction1);

            StringAssert.Contains("Receive email from \"" + emailInteraction1.Id + "\"", result);
            StringAssert.Contains("Invoke function with \"" + emailInteraction1.Id + "\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"1\"", result);

            result = callPlan.PrintSummary(emailInteraction2);

            StringAssert.Contains("Receive email from \"" + emailInteraction2.Id + "\"", result);
            StringAssert.Contains("Invoke function with \"" + emailInteraction2.Id + "\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"1\"", result);

            result = callPlan.PrintSummary(emailInteraction3);

            StringAssert.Contains("Receive email from \"" + emailInteraction3.Id + "\"", result);
            StringAssert.Contains("Invoke function with \"" + emailInteraction3.Id + "\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"1\"", result);

            result = callPlan.PrintSummary(emailInteraction4);

            StringAssert.Contains("Receive email from \"" + emailInteraction4.Id + "\"", result);
            StringAssert.Contains("Invoke function with \"" + emailInteraction4.Id + "\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"1\"", result);

            result = callPlan.PrintSummary(emailInteraction5);

            StringAssert.Contains("Receive email from \"" + emailInteraction5.Id + "\"", result);
            StringAssert.Contains("Invoke function with \"" + emailInteraction5.Id + "\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"1\"", result);

            result = callPlan.PrintSummary(emailInteraction6);

            StringAssert.Contains("Receive email from \"" + emailInteraction6.Id + "\"", result);
            StringAssert.Contains("Invoke function with \"" + emailInteraction6.Id + "\", response is \"2\"", result);
            StringAssert.Contains("Deliver to group \"A\" and Agent \"2\"", result);
        }

        private void SetUpAgentGroupsAndAgents()
        {
            var agentGroups = new List<AgentGroup>
            {
                new AgentGroup
                {
                    Id = GroupId.A,
                    Agents = new List<Agent>
                    {
                        new Agent
                        {
                            Id = 1
                        },
                        new Agent
                        {
                            Id = 2
                        },
                        new Agent
                        {
                            Id = 3
                        },
                    }
                },
                new AgentGroup
                {
                    Id = GroupId.B,
                    Agents = new List<Agent>
                    {
                        new Agent
                        {
                            Id = 1
                        },
                        new Agent
                        {
                            Id = 2
                        },
                        new Agent
                        {
                            Id = 3
                        },
                    }
                },
                new AgentGroup
                {
                    Id = GroupId.C,
                    Agents = new List<Agent>
                    {
                        new Agent
                        {
                            Id = 1
                        },
                        new Agent
                        {
                            Id = 2
                        },
                        new Agent
                        {
                            Id = 3
                        },
                    }
                },
            };

            _agentsService = new AgentsService(agentGroups);
        }

        private Interaction CreateTestCallInteraction()
        {
            var interaction = new Interaction() {
                Id = "12345",
                Type = InteractionType.call
            };
            return interaction;
        }

        private Interaction CreateTestEmailInteraction()
        {
            var interaction = new Interaction() {
                Id = "bob@test.com",
                Type = InteractionType.email
            };
            return interaction;
        }

        private CallPlan ConfigureTestCallPlan()
        {
            var getOrginatorSpecificDataStep = new GetOriginatorSpecificDataStep(_fakeWebService);
            var groupAssigningStep = new GroupAssigningStep();
            var agentAssgningStep = new AgentAssigningStep(_agentsService);
            var callPlan = new CallPlan();
            callPlan.AppendStep(getOrginatorSpecificDataStep);
            callPlan.AppendStep(groupAssigningStep);
            callPlan.AppendStep(agentAssgningStep);
            return callPlan;
        }
    }
}

