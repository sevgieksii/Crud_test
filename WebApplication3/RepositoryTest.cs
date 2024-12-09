using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Models;
using Xunit;

namespace WebApplication3
{
    public class RepositoryTest
    {
        private Repository<Contact> _repository;
        private ContactsAPIDbContext _context;

        public RepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ContactsAPIDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ContactsAPIDbContext(options);
            _repository = new Repository<Contact>(_context);
        }


        [Fact]
        public async Task AddAsync_ShouldAddEntityToDatabase()
        {
            // Arrange
            var contact = new Contact
            {
                FullName = "John Doe",
                Email = "john.doe@example.com",
                Phone = 1234567890,
                Address = "123 Elm Street"
            };

            // Act
            await _repository.AddAsync(contact);
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("John Doe", result.First().FullName);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            _context.Contacts.Add(new Contact
            {
                FullName = "Sevgi",
                Email = "sevgi@example.com",
                Address = "123 Test Street"
            });

            _context.Contacts.Add(new Contact
            {
                FullName = "fatma",
                Email = "fatma@example.com",
                Address = "456 Sample Avenue"
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntityById()
        {
            // Arrange
            var contact = new Contact
            {
                FullName = "John Doe",
                Email = "johndoe@example.com",
                Address = "123 Elm Street",
                Phone = 1234567890
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(contact.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.FullName);
            Assert.Equal("johndoe@example.com", result.Email); 
            Assert.Equal("123 Elm Street", result.Address);    
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntityInDatabase()
        {
            // Arrange
            var contact = new Contact
            {
                FullName = "ahmet",
                Email = "johndoe@example.com",
                Address = "123 Elm Street",
                Phone = 1234567890
            };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            // Güncellenen değer
            contact.FullName = "John Updated";
            contact.Email = "updated.johndoe@example.com"; 

            // Act
            await _repository.UpdateAsync(contact);
            var result = await _repository.GetByIdAsync(contact.Id);

            // Assert
            Assert.Equal("John Updated", result.FullName);
            Assert.Equal("updated.johndoe@example.com", result.Email); 
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveEntityFromDatabase()
        {
            // Arrange
            var contact = new Contact
            {
                FullName = "ali",
                Email = "johndoe@example.com",
                Address = "123 Elm Street",
                Phone = 1234567890
            };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteAsync(contact.Id);
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }




    }
}
