using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace CallPlanKata
{
    public class GroupAssigningStep :IStep
	{
        public void Execute(Interaction interaction)
        {
            var response = 0;

            if(!Int32.TryParse(interaction.State.Data, out response))
            {
                interaction.State.AssignedGroupId = GroupId.C;
            }
            else if(response % 2 == 0)
            {
                interaction.State.AssignedGroupId = GroupId.A;
            }
            else
            {
                interaction.State.AssignedGroupId = GroupId.B;
            }

            interaction.Summary += string.Format("Deliver to group \"{0}\" ", interaction.State.AssignedGroupId);
        }
	}
}

