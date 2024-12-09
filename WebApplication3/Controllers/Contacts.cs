using WebApplication3.Data;
using WebApplication3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Contacts : ControllerBase
    {
        // Dependency Injection


        private readonly ContactsAPIDbContext _dbContext;  // ContactsAPIDbContext'in implementasyonu yerleşik olarak yapıldı

        // Bir readonly'nin değerini iki yerde verebilirsiniz ilki constructor, diğeri tanımlandığı yerdir.
        // Ayrıca readonly ifadeler bir kez set edilebilir
        public Contacts(ContactsAPIDbContext dbContext) // Constructor
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts() //Listeleme yapar
        {
            return Ok(await _dbContext.Contacts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] int id) //id'si verilen kişiyi listeleme
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] AddContactContextRequest addContextRequest)  //ekleme
        {
            var contact = new Contact()
            {

                Address = addContextRequest.Address,
                Email = addContextRequest.Email,
                FullName = addContextRequest.FullName,
                Phone = addContextRequest.Phone
            };
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateContact(int id, UpdateContactContextRequest updateContextRequest) //id'ye göre güncelleme
        {
            var contact = await _dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {

                contact.Address = updateContextRequest.Address;
                contact.Email = updateContextRequest.Email;
                contact.FullName = updateContextRequest.FullName;
                contact.Phone = updateContextRequest.Phone;


                await _dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id) //id'ye göre silme
        {
            var contact = await _dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {

                _dbContext.Remove(contact);
                await _dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }
    }
}
