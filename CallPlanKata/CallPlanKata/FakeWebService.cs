using System;
using NUnit.Framework;

namespace CallPlanKata
{
    public class FakeWebService : IWebService
	{
        public int GetOriginatorSpecificData(Interaction interaction)
        {
            throw new NotImplementedException();
        }
	}

}

