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
    public class Get_Should
    {
        [Fact]
        public void Get_ReturnsActionResult_With_200_Status_With_Valid_Parameters()
        {
            // assign
            Mock<ITwilioService> twilioServiceMock = new Mock<ITwilioService>();
            twilioServiceMock
                .Setup(x => x.GetMessages(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new GetMessagesResponse());

            // act
            var smsController = new SmsController(twilioServiceMock.Object);
            var result = smsController.Get("20/02/2019 10:00:00", "22/02/2019 10:00:00");

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetMessagesResponse>(viewResult.Value);
            Assert.True(model.IsSuccess);
        }

        [Fact]
        public void Get_ReturnsActionResult_With_400_Status_With_Invalid_Valid_From_Date()
        {
            // assign
            Mock<ITwilioService> twilioServiceMock = new Mock<ITwilioService>();
            var smsController = new SmsController(twilioServiceMock.Object);

            // act
            var result = smsController.Get("30/15/2019 10:00:00", "22/02/2019 10:00:00");

            // assert
            var viewResult = Assert.IsType<BadRequestObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetMessagesResponse>(viewResult.Value);
            Assert.False(model.IsSuccess);
            Assert.Equal(1, model.ErrorMessages.Count);
        }

        [Fact]
        public void Get_ReturnsActionResult_With_400_Status_With_Invalid_Valid_To_Date()
        {
            // assign
            Mock<ITwilioService> twilioServiceMock = new Mock<ITwilioService>();
            var smsController = new SmsController(twilioServiceMock.Object);

            // act
            var result = smsController.Get("22/02/2019 10:00:00", "30/15/2019 10:00:00");

            // assert
            var viewResult = Assert.IsType<BadRequestObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetMessagesResponse>(viewResult.Value);
            Assert.False(model.IsSuccess);
            Assert.Equal(1, model.ErrorMessages.Count);
        }

        [Fact]
        public void Get_ReturnsActionResult_With_200_Status_With_Messges_For_Valid_Date_Range()
        {
            // assign
            Mock<ITwilioService> twilioServiceMock = new Mock<ITwilioService>();
            DateTime fromDate = new DateTime(2019, 02, 10, 10, 0, 0);
            DateTime toDate = new DateTime(2019, 02, 11, 10, 0, 0);
            twilioServiceMock
                .Setup(x => x.GetMessages(fromDate, toDate))
                .Returns(new GetMessagesResponse
                    {
                        SmsMessages = new List<SmsMessage>
                        {
                            new SmsMessage {
                                DateSent = new DateTime(2019, 02, 10, 10, 0, 0),
                                MessageBody = "Tet Message"
                            }
                        }
                    }
                );

            var smsController = new SmsController(twilioServiceMock.Object);

            // act
            var result = smsController.Get("10/02/2019 10:00:00", "11/02/2019 10:00:00");

            // assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetMessagesResponse>(viewResult.Value);
            Assert.True(model.IsSuccess);
            Assert.Equal(1, model.SmsMessages.Count);
        }
    }
}
