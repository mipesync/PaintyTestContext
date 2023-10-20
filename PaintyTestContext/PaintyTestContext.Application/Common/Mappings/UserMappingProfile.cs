using AutoMapper;
using PaintyTestContext.Application.DTOs.UserDTOs;
using PaintyTestContext.Application.DTOs.UserDTOs.ResponseDTOs;
using PaintyTestContext.Domain;

namespace PaintyTestContext.Application.Common.Mappings;

/// <summary>
/// Конфигурация маппинга пользователя
/// </summary>
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, GetUserByIdResponseDto>(MemberList.Source);
        CreateMap<User, FriendLookup>(MemberList.Source);
    }
}