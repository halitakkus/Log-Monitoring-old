using Application.Core.Utilities.DataTransferObjects_DTO_.App;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Abstract.Profile;

public class ProfileBase : AutoMapper.Profile
{
    public ProfileBase()
    {
        CreateMap<App, AppResponse>();
        CreateMap<AppRequest, App>();
    }
}