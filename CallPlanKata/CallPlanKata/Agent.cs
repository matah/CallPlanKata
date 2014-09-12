using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace CallPlanKata
{
	public class Agent
	{
        public long Id { get; set; }

        private bool _isHandlingCall = false;

        private int _numberOfAssignedEmails = 0;

        public bool TryHandleInteraction(InteractionType type)
        {
            if(CanHandleInteraction(type))
            {
                AssignInteraction(type);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanHandleInteraction(InteractionType type)
        {
            if(type == InteractionType.email)
            {
                return _numberOfAssignedEmails < 5;
            }

            if(type == InteractionType.call)
            {
                return !_isHandlingCall;
            }

            return false;
        }

        private void AssignInteraction(InteractionType type)
        {
            if(type == InteractionType.email)
            {
                _numberOfAssignedEmails++;
            }

            if(type == InteractionType.call)
            {
                _isHandlingCall = true;
            }
        }
	}


    
}

