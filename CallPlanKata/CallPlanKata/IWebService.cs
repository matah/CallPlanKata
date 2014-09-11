using System;
using NUnit.Framework;

namespace CallPlanKata
{
	public interface IWebService
	{
        int GetOriginatorSpecificData(Interaction interaction);
	}

}

