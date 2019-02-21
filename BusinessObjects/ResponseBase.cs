using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public abstract class ResponseBase
    {
        public ResponseBase()
        {
            ErrorMessages = new List<ErrorMessage>();
        }

        public List<ErrorMessage> ErrorMessages { get; set; }

        public bool IsSuccess
        {
            get
            {
                return (ErrorMessages.Count == 0);
            }
        }
    }
}
