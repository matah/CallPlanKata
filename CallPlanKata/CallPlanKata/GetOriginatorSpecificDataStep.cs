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

        public void Execute(Interaction interaction)
        {
            int response;

            try
            {
                response = _webService.GetOriginatorSpecificData(interaction);
                interaction.State.Data = response.ToString();
            }
            catch(TimeoutException)
            {
                interaction.State.Data = "Time-out";
            }
            catch(Exception)
            {
                interaction.State.Data = "Error Xyz";
            }

            interaction.Summary += string.Format("Invoke function with \"{0}\", response is \"{1}\"\n", interaction.Id, interaction.State.Data);
        }
	}

}

