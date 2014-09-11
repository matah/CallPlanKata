using System;
using NUnit.Framework;

namespace CallPlanKata
{
	public class Interaction
	{
        public string Id { get; set; }

        public string Data {get; set; }

        public InteractionType Type {get; set;}
	}

    public enum InteractionType
    {
        email,
        call
    };

}

