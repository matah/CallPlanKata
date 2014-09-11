using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace CallPlanKata
{
	public class CallPlan
	{
        private readonly List<IStep> _steps;
        private string _summary = "";

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
            _summary += string.Format("Receive {0} from \"{1}\"\n", interaction.Type, interaction.Id);

            foreach(var step in _steps)
            {
                _summary += step.Execute(interaction);
            }
        }

        public string PrintSummary()
        {
            return _summary;
        }
	}

}

