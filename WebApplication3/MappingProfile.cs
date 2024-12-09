using AutoMapper;
using WebApplication3.Models;

namespace WebApplication3
{
   
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddContactContextRequest, Contact>(); // Request -> Entity
            CreateMap<UpdateContactContextRequest, Contact>(); // Update -> Entity
            CreateMap<Contact,ContactDTO>();
        }
    }

}
