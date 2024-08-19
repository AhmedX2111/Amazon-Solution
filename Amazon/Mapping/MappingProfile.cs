using Amazon.Models;
using Amazon.ViewModels;
using AutoMapper;

namespace Amazon.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Electronics
            CreateMap<Laptop, LaptopFormViewModel>().ReverseMap();
            CreateMap<Laptop, LaptopViewModel>().ReverseMap();
            CreateMap<LaptopFormViewModel, LaptopViewModel>().ReverseMap();

        }
    }
}
