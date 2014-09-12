using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace CallPlanKata
{
	public class CallPlan
	{
        private readonly List<IStep> _steps;

        public CallPlan()
        {
            _steps = new List<IStep>();
        }

        public void AppendStep(IStep step)
        {
            _steps.Add(step);
        }

        public void ReceiveInteraction(Interaction interaction)
        {
            interaction.Summary += string.Format("Receive {0} from \"{1}\"\n", interaction.Type, interaction.Id);

            foreach(var step in _steps)
            {
                step.Execute(interaction);
            }
        }

        public string PrintSummary(Interaction interaction)
        {
            return interaction.Summary;
        }
	}

}

