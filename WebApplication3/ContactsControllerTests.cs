using Moq;
using Xunit;
using WebApplication3.Models;
using WebApplication3.Data;
using WebApplication3.Controllers;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3
{
    public class ContactsControllerTests
    {
        [Fact]
        public async Task AddContact_ShouldReturnOkResult()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockDbContext = new Mock<ContactsAPIDbContext>(new DbContextOptions<ContactsAPIDbContext>());

            var addContextRequest = new AddContactContextRequest
            {
                FullName = "John Doe",
                Email = "john.doe@example.com",
                Phone = 1234567890,
                Address = "123 Elm Street"
            };

            var contact = new Contact
            {
                FullName = "John Doe",
                Email = "john.doe@example.com",
                Phone = 1234567890,
                Address = "123 Elm Street"
            };

            // Mock Mapper
            mockMapper
                .Setup(m => m.Map<Contact>(It.IsAny<AddContactContextRequest>()))
                .Returns(contact);

            // Mock DbSet
            var mockDbSet = new Mock<DbSet<Contact>>();
            mockDbContext.Setup(db => db.Contacts).Returns(mockDbSet.Object);

            var controller = new ContactsController(mockDbContext.Object, mockMapper.Object);

            // Act
            var result = await controller.AddContact(addContextRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedContact = Assert.IsType<Contact>(okResult.Value);
            Assert.Equal("John Doe", returnedContact.FullName);
        }

    }
}
