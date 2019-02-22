using api.Controllers;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using twilioService.Interfaces;
using Xunit;

namespace api.tests.Controllers
{
    public class Post_Should
    {
        [Fact]
        public void Post_ReturnsActionResult_With200Status_For_Valid_Message_Valid_Phone()
        {
            // assign
            Mock<ITwilioService> twilioServiceMock = new Mock<ITwilioService>();
            twilioServiceMock
                .Setup(x => x.SendMessage("hello world", "12345"))
                .Returns(new SendMessageResponse());

            var smsController = new SmsController(twilioServiceMock.Object);

            SendMessageRequest smsMessageRequest = new SendMessageRequest
            {
                Message = "hello world",
                MobileNo = "12345"
            };

            // act
            var result = smsController.Post(smsMessageRequest);

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, viewResult.StatusCode);
        }

        [Fact]
        public void Post_ReturnsActionResult_With400Status_For_Validation_Failure()
        {
            // assign
            Mock<ITwilioService> twilioServiceMock = new Mock<ITwilioService>();

            var smsController = new SmsController(twilioServiceMock.Object);

            SendMessageRequest smsMessageRequest = new SendMessageRequest
            {
                Message = "hello world",
                MobileNo = "INVALID"
            };
            smsController.ModelState.AddModelError("MobileNo", "fakeError");

            // act
            var result = smsController.Post(smsMessageRequest);

            // assert
            var viewResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, viewResult.StatusCode);
        }
    }
}
