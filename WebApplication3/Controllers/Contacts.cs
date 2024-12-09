using WebApplication3.Data;
using WebApplication3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Contacts : ControllerBase
    {
        
        private readonly IMapper _mapper;


        private readonly ContactsAPIDbContext _dbContext;  

        
        public Contacts(IMapper mapper, ContactsAPIDbContext dbContext) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts() 
        {

            return Ok(await _dbContext.Contacts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] int id) 
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            var contactDto = _mapper.Map<Contact>(contact);
            return Ok(contact);
        }
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] AddContactContextRequest addContextRequest) 
        {
            var contact = _mapper.Map<Contact>(addContextRequest);

            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateContact(int id, UpdateContactContextRequest updateContextRequest) 
        {
            var contact = await _dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                _mapper.Map(updateContextRequest, contact);

                await _dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
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
