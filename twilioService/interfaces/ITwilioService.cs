using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace twilioService.Interfaces
{
    public interface ITwilioService
    {
        SendMessageResponse SendMessage(string message, string mobileNo);
        GetMessagesResponse GetMessages(DateTime fromDate, DateTime toDate);
    }
}
