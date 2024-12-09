using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3
{
    [ApiController]
    [Route("api/[controller]")]
    internal class ContactsController : ControllerBase
    {
        private ContactsAPIDbContext _dbContext;
        private IMapper _mapper;

        public ContactsController(ContactsAPIDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] AddContactContextRequest addContextRequest)
        {
            var contact = _mapper.Map<Contact>(addContextRequest);
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            return Ok(contact);
        }

    }
}