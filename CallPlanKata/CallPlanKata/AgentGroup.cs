using System;
using NUnit.Framework;

namespace CallPlanKata
{
	public class Group
	{
        public GroupId Id {get; set; }

        public List<Agent> Agent { get; set; }
	}

    public enum GroupId
    {
        A,
        B,
        C
    }
}

