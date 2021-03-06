﻿using Moq;
using System;
using Xunit;


namespace ScheduleAppointment.Tests
{


    public class AppointmentServicePartialMockTests
    {


        [Fact]
        public void WhenAppointmentDateBeforeCurrentDate_ThenExceptionShouldBeThrown()
        {
            // Values
            DateTime now = new DateTime(2018, 5, 15, 11, 42, 32);
            DateTime today = now.Date;
            int maxAppointmentDays = 15;
            AppointmentRequest request = new AppointmentRequest()
            {
                AppointmentDate = new DateTime(2018, 5, 14),
                Hours = 4
            };

            // Arrange
            var mock = new Mock<AppointmentServicePartialMock>();
            mock.Setup(m => m.Today).Returns(today);
            mock.Setup(m => m.Now).Returns(now);
            mock.Setup(m => m.MaxAppointmentDays).Returns(maxAppointmentDays);
            mock.Setup(m => m.GetConfirmationCode()).Returns("ABCDEFG");

            // Act and Assert
            Assert.Throws<Exception>(() => mock.Object.CreateAppointment(request));
        }


        [Fact]
        public void WhenAppointmentDateAfterMaxDate_ThenExceptionShouldBeThrown()
        {
            // Values
            DateTime now = new DateTime(2018, 5, 15, 11, 42, 32);
            DateTime today = now.Date;
            int maxAppointmentDays = 15;
            AppointmentRequest request = new AppointmentRequest()
            {
                AppointmentDate = new DateTime(2018, 5, 31),
                Hours = 4
            };

            // Arrange
            var mock = new Mock<AppointmentServicePartialMock>();
            mock.Setup(m => m.Today).Returns(today);
            mock.Setup(m => m.Now).Returns(now);
            mock.Setup(m => m.MaxAppointmentDays).Returns(maxAppointmentDays);
            mock.Setup(m => m.GetConfirmationCode()).Returns("ABCDEFG");

            // Act and Assert
            Assert.Throws<Exception>(() => mock.Object.CreateAppointment(request));
        }


        [Fact]
        public void WhenAppointmentDateOnMaxDate_AppointmentShouldBeCreatedForThatDate()
        {
            // Values
            DateTime now = new DateTime(2018, 5, 15, 11, 42, 32);
            DateTime today = now.Date;
            int maxAppointmentDays = 15;
            AppointmentRequest request = new AppointmentRequest()
            {
                AppointmentDate = new DateTime(2018, 5, 30),
                Hours = 4
            };

            // Arrange
            var mock = new Mock<AppointmentServicePartialMock>();
            mock.Setup(m => m.Today).Returns(today);
            mock.Setup(m => m.Now).Returns(now);
            mock.Setup(m => m.MaxAppointmentDays).Returns(maxAppointmentDays);
            mock.Setup(m => m.GetConfirmationCode()).Returns("ABCDEFG");

            // Act
            var appt = mock.Object.CreateAppointment(request);

            // Assert
            Assert.NotNull(appt);
            Assert.Equal(new DateTime(2018, 5, 30), appt.AppointmentDate);
            Assert.Equal("ABCDEFG", appt.ConfirmationCode);
            Assert.Equal(now, appt.CreateTime);
        }



    }
}
