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
using WebCloudSystem.Bll.Services.Utils;
using Microsoft.AspNetCore.Http;

namespace WebCloudSystem.Bll.Services.Files {

    public class FileService : BaseService, IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IFileWriter _fileWriter;

        public FileService(IFileRepository fileRepository,IMapper mapper, IUserRepository userRepository,IFileWriter fileWriter) {
            _fileRepository = fileRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _fileWriter = fileWriter;
        }

        public async Task<FileDto> GetFileByUser(int userId,int fileId)
        {
            var user = _userRepository.GetOneByIdAsync(userId);
            if(user == null) {
                throw new UserNotFoundException("User not found");
            } 

            var fileResult = await _fileRepository.GetOneByAsync(f => f.user.Id == user.Id && f.Id == fileId);
            if(fileResult == null) {
                throw new ResourceNotFoundException("File not found!");
            }

            var fileDto = _mapper.Map<File,FileDto>(fileResult);

            return fileDto;
        }

        public async Task<FileDtoPaged> GetFilesByUser(int userId,FileDtoPagedQuery fileQuery)
        {
            var myUser = await _userRepository.GetOneByIdAsync(userId);

            if(myUser == null) {
                throw new UserNotFoundException("Files owner not found");
            }

            var pagedEntity = await _fileRepository.GetAllPagedAsync(fileQuery.Page,fileQuery.Size,fileQuery.Filter,fileQuery.Order,x => x.user.Id == myUser.Id);

            var pagedResult = GetFilesPaged(pagedEntity,fileQuery);

            return pagedResult;
        }

        public async Task<FileDto> UpdateFile(int userId,FileDtoUpdate file)
        {
            var user = await  _userRepository.GetOneByIdAsync(userId);
            if(user == null) {
                throw new UserNotFoundException();
            }

            var fileId = file.FileId;
            var fileResult = await _fileRepository.GetOneByAsync(f => f.user.Id == user.Id && f.Id == fileId);
            if(fileResult == null) {
                throw new ResourceNotFoundException("File not found!");
            }

            fileResult.FileName = file.Filename;
            fileResult = _fileRepository.Update(fileResult);

            await _fileRepository.SaveAsync();

            var fileDto = _mapper.Map<File,FileDto>(fileResult);
            return fileDto;
        }

        public async Task<FileDto> Upload(IFormFile file,int userId)
        {
            var user = await _userRepository.GetOneByIdAsync(userId);
            if(user == null) {
                throw new UserNotFoundException("User not found!");
            }

            var fileExtension = _fileWriter.GetFileExtension(file);
            var fileNameOnServer =await  _fileWriter.SaveFileOnServer(file,userId);

            var fileEntity = new File();
            
            fileEntity.FileName = file.FileName;
            fileEntity.FileNameOnServer = fileNameOnServer;
            fileEntity.Extension = fileExtension;
            fileEntity.FileSize = file.Length;
            fileEntity.user = user;

            var result = await _fileRepository.CreateAsync(fileEntity);
            
            await _fileRepository.SaveAsync();

            var fileDto = _mapper.Map<File,FileDto>(result);

            return fileDto;
        }



        private FileDtoPaged GetFilesPaged(PagedEntity<File> pagedEntity,FileDtoPagedQuery fileQuery){
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