using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace CallPlanKata
{
    public class GroupAssigningStep :IStep
	{
        public void Execute(Interaction interaction)
        {
            var agentGroup = "";

            var response = 0;

            if(!Int32.TryParse(interaction.State.Data, out response))
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

            interaction.Summary += string.Format("Deliver to group \"{0}\" ", agentGroup);
        }
	}
}

