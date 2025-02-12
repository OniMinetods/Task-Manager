using AutoMapper;
using firstPetProject.Core.Domain.Entities;

namespace firstPetProject.Core.Auth
{
    public class DataBaseMapping : Profile
    {
        public DataBaseMapping()
        {
            CreateMap<User, User>().ReverseMap();
        }
    }
}
