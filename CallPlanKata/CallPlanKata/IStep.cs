using System;
using NUnit.Framework;

namespace CallPlanKata
{
	public interface IStep
	{
        void Execute(Interaction state);
	}
}

