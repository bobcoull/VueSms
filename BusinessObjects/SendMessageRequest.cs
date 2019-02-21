using System.ComponentModel.DataAnnotations;

namespace BusinessObjects
{
    public class SendMessageRequest
    {
        [Required(ErrorMessage = "Message can not be empty")]
        [MaxLength(160, ErrorMessage = "Max length of message is 160 characters")]
        public string Message { get; set; }

        [RegularExpression(@"^\+[0-9]{1,}$", ErrorMessage = "The Mobile No must be numeric")]
        [Required(ErrorMessage = "Mobile No can not be empty")]
        public string MobileNo { get; set; }
    }
}
