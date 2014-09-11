using System;
using NUnit.Framework;

namespace CallPlanKata
{
	public interface IStep
	{
        string Execute(Interaction interaction);
	}
}

