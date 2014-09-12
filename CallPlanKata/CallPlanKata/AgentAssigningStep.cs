using System;
using NUnit.Framework;

namespace CallPlanKata
{
    public class AgentAssigningStep :IStep
	{
        public void Execute(Interaction interaction)
        {
            interaction.Summary += "and Agent \"1\"\"";
        }
	}
}

