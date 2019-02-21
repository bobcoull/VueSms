using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class GetMessagesResponse : ResponseBase
    {
        public GetMessagesResponse()
        {
            SmsMessages = new List<SmsMessage>();
        }
        public List<SmsMessage> SmsMessages { get; set; }

    }
}
