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

        public List<Agent> GetAvailableAgentsFromGroup(GroupId id)
        {
            var group = _agentGroups.Single(ag => ag.Id == id);

            return group.Agents.Where(a => !a.IsHandlingCall).ToList();
        }
    }
}

