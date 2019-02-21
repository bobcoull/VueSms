using BusinessObjects;
using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using twilioService.Interfaces;

namespace twilioService
{
    public class TwilioService : ITwilioService
    {
        // TODO: these would normally be stored in the db or a config file
        // live tokens
        const string accountSid = "ACb68fb6409c0594bbf9318e0391f9184e";
        const string authToken = "624a3175269bcb4565fa1cdf75ec8ac3";

        // test tokens
//        const string accountSid = "ACab3c8d77d508b6442207a65b50a55ceb";
//        const string authToken = "c68313ada988bfa112bd3cca88b305c6";

        public SendMessageResponse SendMessage(string message, string mobileNo)
        {
            TwilioClient.Init(accountSid, authToken);

            SendMessageResponse sendMessageResponse = new SendMessageResponse();

            try
            {
                MessageResource messageResource = MessageResource.Create(
                    body: message,
                    from: new Twilio.Types.PhoneNumber("+441527962368"), // live
                    //from: new Twilio.Types.PhoneNumber("+15005550006"), // ok test
                    //from: new Twilio.Types.PhoneNumber("+15005550000"), // unavailable test
                    //from: new Twilio.Types.PhoneNumber("+15005550001"), // invalid test
                    to: new Twilio.Types.PhoneNumber(mobileNo) 
                );

                if (messageResource.ErrorCode != null)
                {
                    sendMessageResponse.ErrorMessages.Add(new ErrorMessage
                    {
                        Code = messageResource.ErrorCode,
                        Message = messageResource.ErrorMessage
                    });
                }
            }
            catch (Twilio.Exceptions.ApiException ex)
            {
                sendMessageResponse.ErrorMessages.Add(new ErrorMessage
                {
                    Code = ex.Code,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                // TODO: possibly add logging of this exception so it doesn't get lost

                sendMessageResponse.ErrorMessages.Add(new ErrorMessage
                {
                    Message = ex.ToString()
                });
            }

            return sendMessageResponse;
        }

        public GetMessagesResponse GetMessages(DateTime fromDate, DateTime toDate)
        {
            TwilioClient.Init(accountSid, authToken);

            var messages = MessageResource.Read(
                    dateSentAfter: fromDate,
                    dateSentBefore: toDate
                    );
            //   from: new Twilio.Types.PhoneNumber("+15017122661"),
            //   to: new Twilio.Types.PhoneNumber("+15558675310")

            GetMessagesResponse response = new GetMessagesResponse { SmsMessages = new List<SmsMessage>() };

            foreach (var message in messages)
            {
                response.SmsMessages.Add(new SmsMessage
                {
                    DateSent = message.DateSent.Value,
                    MessageBody = message.Body
                });
            }

            return response;
        }
    }
}

