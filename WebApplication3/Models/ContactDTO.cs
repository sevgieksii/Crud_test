namespace WebApplication3.Models
{
    public class ContactDTO
    {
        public class Contact
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public long Phone { get; set; }
            public string Address { get; set; }
        }
    }
}
