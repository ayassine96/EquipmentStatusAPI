using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EquipmentStatusAPI.Controllers;
using EquipmentStatusAPI.Models;
using EquipmentStatusAPI.Data;
using System.Threading.Tasks;

namespace EquipmentStatusAPI.Tests
{
    public class EquipmentStatusControllerTests
    {
        private readonly EquipmentStatusContext _context;
        private readonly EquipmentStatusController _controller;

        public EquipmentStatusControllerTests()
        {
            var options = new DbContextOptionsBuilder<EquipmentStatusContext>()
                .UseInMemoryDatabase(databaseName: "EquipmentStatusDbTest")
                .Options;

            _context = new EquipmentStatusContext(options);
            _controller = new EquipmentStatusController(_context);
        }

        [Fact]
        public async Task PostStatus_ValidData_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var status = new EquipmentStatus { EquipmentId = "1", Status = "Operational" };

            // Act
            var result = await _controller.PostStatus(status);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task PostStatus_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            EquipmentStatus status = null; // Invalid data

            // Act
            var result = await _controller.PostStatus(status);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetStatus_ValidId_ReturnsOkObjectResult()
        {
            // Arrange
            var status = new EquipmentStatus { EquipmentId = "E1", Status = "Operational" };
            _context.EquipmentStatuses.Add(status);
            await _context.SaveChangesAsync();
            // Act
            var result = await _controller.GetStatus("E1");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetStatus_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetStatus("E999"); // Non-existent ID

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
