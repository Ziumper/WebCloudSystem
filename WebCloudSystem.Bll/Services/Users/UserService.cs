using WebCloudSystem.Bll.Services.Base;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using WebCloudSystem.Bll.Dto.Users;
using WebCloudSystem.Bll.Helpers;
using Microsoft.Extensions.Options;
using WebCloudSystem.Dal.Repositories.Users;
using WebCloudSystem.Dal.Models;
using WebCloudSystem.Bll.Services.Utils;
using AutoMapper;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace WebCloudSystem.Bll.Services.Users
{

    public class UserService : BaseService, IUserService
    {
        private List<User> _users = new List<User>
        { 
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" } 
        };
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHashService _hashService;

        public UserService(IOptions<AppSettings> appSettings,IUserRepository userRepository,IMapper mapper,IHashService hashService) {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
            _mapper = mapper;
            _hashService = hashService;
        }

        public async Task<UserDtoWithoutPassword> Authenticate(string username, string password)
        {
            var hashedPassword = _hashService.GetHash(password);
             var user = await _userRepository.GetOneByAsync(x => x.Username == username && x.Password == hashedPassword);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            var userWithoutPassword = _mapper.Map<User,UserDtoWithoutPassword>(user);

            return userWithoutPassword;
        }

        public IEnumerable<UserDtoWithoutPassword> GetAll()
        {
            List<UserDtoWithoutPassword> usersWithoutPassword = new List<UserDtoWithoutPassword>();
            foreach(var user in _users) {
                var userDto = _mapper.Map<User,UserDtoWithoutPassword>(user);
                usersWithoutPassword.Add(userDto);
            }

            return usersWithoutPassword;
        }

        public async Task<UserDtoWithoutPassword> Register(UserDto userParam)
        {
            userParam.Password = _hashService.GetHash(userParam.Password);
            var user = _mapper.Map<UserDto,User>(userParam);
            var userResult = await _userRepository.CreateAsync(user);
            
            await _userRepository.SaveAsync();

            var userWithoutPassword = _mapper.Map<User,UserDtoWithoutPassword>(userResult);

            return userWithoutPassword;
        }
    }
}