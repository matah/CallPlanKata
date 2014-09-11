using System;
using NUnit.Framework;

namespace CallPlanKata
{
	public class Interaction
	{
        public string Id { get; set; }

        public Type Type {get; set;}
	}

    public enum InteractionType
    {
        Email,
        Call
    };

}

