using System;
using NUnit.Framework;
using System.Linq;

namespace CallPlanKata
{
    public class AgentAssigningStep :IStep
	{
        private AgentsService _agentsService;

        public AgentAssigningStep(AgentsService agentsService)
        {
            _agentsService = agentsService;
        }
        public void Execute(Interaction interaction)
        {
            var agents = _agentsService.GetAgentsFromGroup(interaction.State.AssignedGroupId);

            var availableAgent = agents.FirstOrDefault(a => a.TryHandleInteraction(interaction.Type));

            if(availableAgent != null)
            {
                interaction.Summary += string.Format("and Agent \"{0}\"", availableAgent.Id);
            }
            else
            {
                interaction.Summary += "all agents are busy";
            }
        }
	}
}

