using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public class SendMessageResponse : ResponseBase
    {
        public SendMessageResponse() : base()
        {
        }

        public string SuccessMessage { get; set; }
    }
}
