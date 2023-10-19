using AutoMapper;
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
        CreateMap<User, GetUserByIdResponseDto>(MemberList.Source)
            .ForMember(dto => dto.FriendsCount, opt => 
                opt.MapFrom(user => user.FriendsIdList == null ? 0 : user.FriendsIdList.Count));
    }
}