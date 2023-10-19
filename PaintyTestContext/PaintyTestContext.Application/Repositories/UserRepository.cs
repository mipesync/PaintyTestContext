using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaintyTestContext.Application.Common.Exceptions;
using PaintyTestContext.Application.Common.Services;
using PaintyTestContext.Application.DTOs.UserDTOs;
using PaintyTestContext.Application.DTOs.UserDTOs.ResponseDTOs;
using PaintyTestContext.Application.Interfaces;
using PaintyTestContext.Application.Interfaces.Repositories;
using PaintyTestContext.Domain;

namespace PaintyTestContext.Application.Repositories;

/// <inheritdoc />
public class UserRepository : IUserRepository
{
    private readonly IDBContext _dbContext;
    private readonly IMapper _mapper;

    public UserRepository(IDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<GetUserByIdResponseDto> GetById(Guid userId, string hostUrl)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId, CancellationToken.None);

        if (user is null)
            throw new NotFoundException(user);

        var mappedUser = _mapper.Map<GetUserByIdResponseDto>(user);

        mappedUser.ImagesUrlsList = UrlParse(user, hostUrl);
        
        return mappedUser;
    }

    public async Task UpdateName(UpdateNameDto dto, string currentUserId)
    {
        throw new NotImplementedException();
    }

    public async Task<GetImagesResponseDto> GetImages(Guid currentUserId, Guid ownerId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UploadImage(Guid currentUserId, IFormFile image)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveImage(Guid currentUserId, string fileName)
    {
        throw new NotImplementedException();
    }

    public async Task SendFriendRequest(Guid targetId)
    {
        throw new NotImplementedException();
    }

    public async Task AddFriend(Guid targetId)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveFriend(Guid friendId)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Парсит названия файлов из БД в статические ссылки
    /// </summary>
    /// <param name="user">Пользователь, чьи файлы нужно парсить</param>
    /// <param name="hostUrl">Домен API</param>
    /// <returns><see cref="List{ImageLookup}"/></returns>
    private static List<ImageLookup> UrlParse(User user, string hostUrl)
    {
        List<ImageLookup> imageLookups = new();

        if (user.UrlList is null) return imageLookups;

        imageLookups.AddRange(user.UrlList.Select(url => 
            new ImageLookup
            {
                FileName = url, Url = UrlParser.Parse(hostUrl, user.Id.ToString(), url)!
            }));

        return imageLookups;
    }
}