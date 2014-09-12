using System;
using NUnit.Framework;

namespace CallPlanKata
{
	public class InteractionState
	{
        public string Data { get; set; }

        public GroupId AssignedGroupId { get; set; }

        public long AssignedAgentId {get; set;}
	}
}

