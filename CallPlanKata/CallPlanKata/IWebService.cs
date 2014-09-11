using System;
using NUnit.Framework;

namespace CallPlanKata
{
	interface IWebService
	{
        int GetOriginatorSpecificData(Interaction interaction);
	}

}

