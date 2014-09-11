using System;
using NUnit.Framework;

namespace CallPlanKata
{
    public class AgentAssigningStep :IStep
	{
        public string Execute(Interaction interaction)
        {
            var agentGroup = "";

            var response = 0;

            if(!Int32.TryParse(interaction.Data, out response))
            {
                agentGroup = "C";
            }
            else if(response % 2 == 0)
            {
                agentGroup = "A";
            }
            else
            {
                agentGroup = "B";
            }
            
            return string.Format("Deliver to group \"{0}\" and Agent \"{1}\"\n", agentGroup, 1);
        }
	}
}

