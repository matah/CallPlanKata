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

        public List<Agent> GetAgentsFromGroup(GroupId id)
        {
            return _agentGroups.Single(ag => ag.Id == id).Agents.ToList();
        }
    }
}

