using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebCloudSystem.Bll.Dto.Users;
using WebCloudSystem.Bll.Exceptions;
using WebCloudSystem.Bll.Helpers;
using WebCloudSystem.Bll.Services.Base;
using WebCloudSystem.Bll.Services.Emails;
using WebCloudSystem.Bll.Services.Emails.Models;
using WebCloudSystem.Bll.Services.Utils;
using WebCloudSystem.Dal.Models;
using WebCloudSystem.Dal.Repositories.Users;

namespace WebCloudSystem.Bll.Services.Users {

    public class UserService : BaseService, IUserService {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHashService _hashService;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService (
            IOptions<AppSettings> appSettings,
            IUserRepository userRepository, IMapper mapper,
            IHashService hashService,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccesor) {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
            _mapper = mapper;
            _hashService = hashService;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccesor;
        }

        public async Task<UserDtoWithoutPassword> Authenticate (string username, string password) {
            var hashedPassword = _hashService.GetHash (password);
            var user = await _userRepository.GetOneByAsync (x => x.Username == username && x.Password == hashedPassword);

            // return null if user not found
            if (user == null)
                return null;

            if (!user.IsActive) {
                throw new BadRequestException ("User not active, activate your account to login");
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler ();
            var key = Encoding.ASCII.GetBytes (_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (new Claim[] {
                new Claim (ClaimTypes.Name, user.Id.ToString ())
                }),
                Expires = DateTime.UtcNow.AddDays (7),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken (tokenDescriptor);

            // remove password before returning
            var userWithoutPassword = _mapper.Map<User, UserDtoWithoutPassword> (user);
            userWithoutPassword.Token = tokenHandler.WriteToken (token);

            return userWithoutPassword;
        }

        public async Task<UserDtoWithoutPassword> Register (UserDto userParam) {
            if (await IsUserWithThisUserNameIsInDatabase (userParam.Username)) {
                throw new BadRequestException ("There is a user with this username!");
            }
            if (await IsUserWithThisUserNameIsInDatabase (userParam.Email)) {
                throw new BadRequestException ("There is a user with this email!");
            }

            userParam.Password = _hashService.GetHash (userParam.Password);
            var user = _mapper.Map<UserDto, User> (userParam);
            user.Username = user.Username.ToLower();   
            user.Email = user.Email.ToLower ();
            user.IsActive = false;
            user.ActivationCode = _hashService.GetRandomActivationCode ();
           
            var userResult = await _userRepository.CreateAsync (user);

            await _userRepository.SaveAsync ();

            var emailMesssage = GetRegisterEmailMessage(userResult);
            _emailService.Send(emailMesssage);
          
            var userWithoutPassword = _mapper.Map<User, UserDtoWithoutPassword> (userResult);

            return userWithoutPassword;
        }

        private async Task<Boolean> IsUserWithThisUserNameIsInDatabase (string username) {
            var user = await _userRepository.GetOneByAsync (x => x.Username == username.ToLower ());
            if (user != null) {
                return true;
            }

            return false;
        }

        private async Task<Boolean> IsUserWithThisEmailIsInDatabase (string email) {
            var user =  await _userRepository.GetOneByAsync (x => x.Email == email.ToLower ());
            if (user != null) {
                return true;
            }

            return false;
        }

        private EmailMessage GetRegisterEmailMessage (User user) {
            EmailMessage emailMesssage = new EmailMessage ();
            emailMesssage.FromAddresses = new List<EmailAddress> ();
            emailMesssage.ToAddresses = new List<EmailAddress> ();

            EmailAddress emailAddres = new EmailAddress ();
            emailAddres.Address = "webcloudsystem@email.com";
            emailAddres.Name = "WebCloudSytem bot";
            emailMesssage.FromAddresses.Add (emailAddres);

            EmailAddress userEmailAddres = new EmailAddress ();
            userEmailAddres.Name = user.FirstName + " " + user.LastName;
            userEmailAddres.Address = user.Email;
            emailMesssage.ToAddresses.Add (userEmailAddres);

            emailMesssage.Subject = "Activation code Web Cloud System";

            var host = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            emailMesssage.Content = "Please go to " + host + "/activation/" + user.Id + " and a activate your account<br/> Here is your activation code: <b>" + user.ActivationCode + "</b>";

            return emailMesssage;
        }

        public async Task<UserDtoWithoutPassword> ActivateUser (UserDtoActivation activationUserDetails) {
            var user = await _userRepository.GetOneByAsync (u => u.Id == activationUserDetails.Id);
            if (user == null) {
                throw new UserNotFoundException ("Activation user not found!");
            }

            if (user.ActivationCode != activationUserDetails.Code) {
                throw new BadRequestException ("Wrong activation code!");
            }

            user.IsActive = true;

            var resultUser = _userRepository.Update (user);
            await _userRepository.SaveAsync ();

            var userWithoutPassword = _mapper.Map<User, UserDtoWithoutPassword> (resultUser);

            return userWithoutPassword;
        }

        public async Task ResendActivationCode (string userEmail) {
            var user = await _userRepository.GetOneByAsync (u => u.Email == userEmail.ToLower ());
            if (user == null) {
                throw new UserNotFoundException ("User with that email not found");
            }
            var emailMessage = GetRegisterEmailMessage (user);
            _emailService.Send (emailMessage);
        }
    }
}