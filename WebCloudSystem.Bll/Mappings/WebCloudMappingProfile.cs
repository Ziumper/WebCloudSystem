using AutoMapper;
using WebCloudSystem.Dal.Models;
using WebCloudSystem.Bll.Dto.Users;

namespace WebCloudSystem.Bll.Mappings {

    public class WebCloudMappingProfile : Profile {

        public WebCloudMappingProfile() {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<User,UserDtoWithoutPassword>();
        }
    }
}