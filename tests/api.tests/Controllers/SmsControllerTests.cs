using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Moq;
using twilioService.Interfaces;
using BusinessObjects;
using twilioService;

namespace api.tests.Controllers
{
    public class SmsControllerTests
    {
        [Fact]
        public void Get_ReturnsActionResult_With200ttaus_With_Valid_Parameters()
        {
            // assign
            Mock<ITwilioService> twilioServiceMock = new Mock<ITwilioService>();

            var smsController = new SmsController(twilioServiceMock.Object);
            var result = smsController.Get("20/02/2019 10:00:00", "22/02/2019 10:00:00");

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetMessagesResponse>(viewResult.Value);
            Assert.True(model.IsSuccess);
        }

        [Fact]
        public void Post_ReturnsActionResult_With200Status_For_Valid_Message_Valid_Phone()
        {
            // assign
            Mock<ITwilioService> twilioServiceMock = new Mock<ITwilioService>();

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
