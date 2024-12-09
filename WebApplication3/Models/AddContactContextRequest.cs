namespace WebApplication3.Models
{
    public class AddContactContextRequest
    {
        //Id sırayla kendi kendine geldiği için ıd'yi ekleme yaparken gizlememiz gerekir
        public string FullName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
    }
}
