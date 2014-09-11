using System;
using NUnit.Framework;

namespace CallPlanKata
{
    public class GetOriginatorSpecificDataStep : IStep
	{
        private readonly IWebService _webService;

        public GetOriginatorSpecificDataStep(IWebService webService)
        {
            _webService = webService;
        }

        public string Execute(string input)
        {
            throw new NotImplementedException();
        }
	}

}

