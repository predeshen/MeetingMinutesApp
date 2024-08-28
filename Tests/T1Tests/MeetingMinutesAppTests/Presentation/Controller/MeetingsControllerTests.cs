using FluentValidation;
using FluentValidation.Results;
using MeetingMinutesApp.Core.Entities;
using MeetingMinutesApp.Infrastructure.Data;
using MeetingMinutesApp.Presentation.Controllers;
using MeetingMinutesApp.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MeetingMinutesApp.Tests
{
    public class MeetingsControllerTests
    {
        private MeetingMinutesAppContext _context;
        private Mock<IValidator<CaptureNewMeetingRequest>> _validatorMock;
        private MeetingsController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MeetingMinutesAppContext>()
                .UseInMemoryDatabase(databaseName: "MeetingMinutesApp")
                .Options;

            _context = new MeetingMinutesAppContext(options);
            _validatorMock = new Mock<IValidator<CaptureNewMeetingRequest>>();
            _controller = new MeetingsController(_context);
        }
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task CaptureNewMeeting_ValidRequest_ReturnsOk()
        {
            // Arrange
            var request = new CaptureNewMeetingRequest
            {
                MeetingTypeId = 1,
                Date = "2023-10-01",
                Time = "10:00:00",
                PreviousOpenItems = new List<MeetingItemWithStatusDto>(),
                NewMeetingItems = new List<MeetingItemDto>()
            };

            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<CaptureNewMeetingRequest>(), default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            // Act
            var result = await _controller.CaptureNewMeeting(request);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task CaptureNewMeeting_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var request = new CaptureNewMeetingRequest
            {
                MeetingTypeId = 0,
                Date = "invalid-date",
                Time = "invalid-time",
                PreviousOpenItems = new List<MeetingItemWithStatusDto>(),
                NewMeetingItems = new List<MeetingItemDto>()
            };

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("MeetingTypeId", "Invalid meeting type ID."),
                new ValidationFailure("Date", "Invalid date."),
                new ValidationFailure("Time", "Invalid time.")
            };

            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<CaptureNewMeetingRequest>(), default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult(validationFailures));

            // Act
            var result = await _controller.CaptureNewMeeting(request);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
    }
}