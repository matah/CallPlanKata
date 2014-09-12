using System;
using NUnit.Framework;

namespace CallPlanKata
{
	public class Interaction
	{
        private InteractionState _state;

        public string Id { get; set; }

        public InteractionType Type {get; set;}

        public string Summary {get; set; }

        public InteractionState State 
        { 
            get 
            {
                return _state ?? (_state =  new InteractionState());
            }
        }
	}

    public enum InteractionType
    {
        email,
        call
    };

}

