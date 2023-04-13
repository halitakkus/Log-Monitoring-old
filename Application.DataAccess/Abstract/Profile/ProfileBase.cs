using Application.DataAccess.Entities;
using Application.Core.Utilities.DataTransferObjects.RemoteWork;

namespace Application.DataAccess.Abstract.Profile;

public class ProfileBase : AutoMapper.Profile
{
    public ProfileBase()
    {
        CreateMap<RemoteWorkAddRequest, RemoteWork>();
        CreateMap<RemoteWorkUpdateRequest, RemoteWork>();
    }
}