using AutoMapper;
using WebCloudSystem.Dal.Models;
using WebCloudSystem.Bll.Dto.Users;
using WebCloudSystem.Bll.Dto.Files;

namespace WebCloudSystem.Bll.Mappings {

    public class WebCloudMappingProfile : Profile {

        public WebCloudMappingProfile() {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<User,UserDtoWithoutPassword>();
            CreateMap<File,FileDto>().ReverseMap();
        }
    }
}