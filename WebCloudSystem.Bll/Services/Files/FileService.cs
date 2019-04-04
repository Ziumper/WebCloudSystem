using WebCloudSystem.Bll.Services.Base;
using System.Threading.Tasks;
using WebCloudSystem.Bll.Dto.Files;
using WebCloudSystem.Dal.Repositories.Files;
using WebCloudSystem.Dal.Repositories.Users;
using AutoMapper;
using WebCloudSystem.Bll.Exceptions;

namespace WebCloudSystem.Bll.Services.Files {

    public class FileService : BaseService, IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly IParserService _parserService;
        private readonly IUserRepository _userRepository;

        public FileService(IFileRepository fileRepository,IMapper mapper, IParserService parserService,IUserRepository userRepository) {
            _fileRepository = fileRepository;
            _mapper = mapper;
            _parserService = parserService;
            _userRepository = userRepository;
        }
        public async Task<FileDtoPaged> GetFilesByUser(string userId,FileDtoPagedQuery fileQuery)
        {
            var myUserId = _parserService.ParseUserId(userId);

            var myUser = await _userRepository.GetOneByIdAsync(myUserId);

            if(myUser == null) {
                throw new UserNotFoundException();
            }
            throw new System.NotImplementedException();
        }
    }
}