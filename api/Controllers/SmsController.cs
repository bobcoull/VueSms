using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using twilioService.Interfaces;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private ITwilioService _twilioService;

        public SmsController(ITwilioService twilioService)
        {
            _twilioService = twilioService;
        }

        /// <summary>
        /// Get list of SMS messages
        /// </summary>
        /// <param name="fromDate">FromDate in format DD/MM/YYYY HH:MM:SS</param>
        /// <param name="toDate">ToDate in format DD/MM/YYYY HH:MM:SS</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string fromDate, string toDate)
        {
            DateTime fromDateParsed;
            DateTime toDateParsed;

            GetMessagesResponse response = new GetMessagesResponse();

            if (!DateTime.TryParse(fromDate, out fromDateParsed))
            {
                response.ErrorMessages.Add(new ErrorMessage{ Message = $"From date {fromDate} is invalid" });
            }

            if (!DateTime.TryParse(toDate, out toDateParsed))
            {
                response.ErrorMessages.Add(new ErrorMessage { Message = $"To date {toDate} is invalid" });
            }

            if (response.IsSuccess && toDateParsed < fromDateParsed)
            {
                response.ErrorMessages.Add(new ErrorMessage { Message = "To Date can't be before From Date" });
            }

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            // get messages
            response = _twilioService.GetMessages(fromDateParsed, toDateParsed);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SendMessageRequest sendMessageRequest)
        {
            SendMessageResponse response = new SendMessageResponse();
            
            if (!ModelState.IsValid)
            {
                // validation errors have occurred
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        response.ErrorMessages.Add(new ErrorMessage { Message = error.ErrorMessage });
                    }
                }

                return BadRequest(response);
                // send message
            }

            response = _twilioService.SendMessage(sendMessageRequest.Message, sendMessageRequest.MobileNo);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            response.SuccessMessage = $"{sendMessageRequest.Message} sent to {sendMessageRequest.MobileNo} successfully";

            return Ok(response);
        }
    }
}
