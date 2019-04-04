using WebCloudSystem.Bll.Services.Base;
using System.Threading.Tasks;
using WebCloudSystem.Bll.Dto.Files;
using WebCloudSystem.Dal.Repositories.Files;
using WebCloudSystem.Dal.Repositories.Users;
using WebCloudSystem.Dal.Models.Base;
using WebCloudSystem.Dal.Models;
using AutoMapper;
using WebCloudSystem.Bll.Exceptions;
using System.Collections.Generic;

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
                throw new UserNotFoundException("User for files owner not found");
            }

            var pagedEntity = await _fileRepository.GetAllPagedAsync(fileQuery.Page,fileQuery.Size,fileQuery.Filter,fileQuery.Order,x => x.user.Id == myUserId);

            var pagedResult = getFilesPaged(pagedEntity,fileQuery);

            return pagedResult;
        }

        private FileDtoPaged getFilesPaged(PagedEntity<File> pagedEntity,FileDtoPagedQuery fileQuery){
            var result = new FileDtoPaged();
            var entitiesDtoList = new List<FileDto>();
            var files = pagedEntity.Entities;

            result.Size = fileQuery.Size;
            result.Count = pagedEntity.Count;
          
            result.Page = fileQuery.Page;

            foreach(var file in files) {
                var fileDto = _mapper.Map<File,FileDto>(file);
                entitiesDtoList.Add(fileDto);
            }
            result.Entities = entitiesDtoList;

            return result;
        }
    }
}