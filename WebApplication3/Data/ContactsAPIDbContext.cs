using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Data
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions<ContactsAPIDbContext> options)
            : base(options)
        {
           
        }
        public virtual DbSet<Contact> Contacts { get; set; } 

        
    }
}
