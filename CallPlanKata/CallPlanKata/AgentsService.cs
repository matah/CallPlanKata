using System;
using System.Collections.Generic;
using System.Linq;

namespace CallPlanKata
{
    public class AgentsService
    {
        private List<AgentGroup> _agentGroups;

        public AgentsService(List<AgentGroup> agentGroups)
        {
            _agentGroups = agentGroups;
        }

        public List<Agent> GetAvailableAgentsFromGroupForInteraction(Interaction interaction)
        {
            var group = _agentGroups.Single(ag => ag.Id == interaction.State.AssignedGroupId);

            if(interaction.Type == InteractionType.email)
            {
                return group.Agents.Where(a => a.NumberOfAssignedEmails < 5).ToList();
            }
            else
            {
                return group.Agents.Where(a => !a.IsHandlingCall && a.NumberOfAssignedEmails <= 5).ToList();
            }
        }
    }
}

