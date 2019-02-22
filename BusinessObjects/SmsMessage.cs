using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class SmsMessage
    {
        public DateTime DateSent { get; set; }

        public string DateSentFormatted
        {
            get { return DateSent.ToString("dd/MM/yyyy HH:mm"); }
        }

        public string MessageBody { get; set; }
    }
}
