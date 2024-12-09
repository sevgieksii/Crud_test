using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Data
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions<ContactsAPIDbContext> options)
            : base(options)
        {
            //options ile bağlantı dizesi alınmış olacak
        }
        public DbSet<Contact> Contacts { get; set; } //Tablo

        
    }
}
