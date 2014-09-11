using System;
using NUnit.Framework;
using System.Web.Script.Serialization;

namespace CallPlanKata
{
    public class GetOriginatorSpecificDataStep : IStep
	{
        private readonly IWebService _webService;

        public GetOriginatorSpecificDataStep(IWebService webService)
        {
            _webService = webService;
        }

        public string Execute(Interaction interaction)
        {
            var response = _webService.GetOriginatorSpecificData(interaction);
            interaction.Data = response.ToString();
            return string.Format("Invoke function with \"{0}\", response is \"{1}\"", interaction.Id, response);
        }
	}

}

