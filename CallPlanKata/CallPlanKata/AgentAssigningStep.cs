using System;
using NUnit.Framework;

namespace CallPlanKata
{
    public class AgentAssigningStep :IStep
	{
        public string Execute(Interaction interaction)
        {
            var agentGroup = "";

            if(Int32.Parse(interaction.Data) % 2 == 0)
            {
                agentGroup = "A";
            }
            
            return string.Format("Deliver to group \"{0}\" and Agent \"{1}\"\n", agentGroup, 1);
        }
	}
}

