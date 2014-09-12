using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace CallPlanKata
{
	public class AgentGroup
	{
        public GroupId Id {get; set; }

        public IEnumerable<Agent> Agents { get; set; }
	}

    public enum GroupId
    {
        A,
        B,
        C
    }
}

