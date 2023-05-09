using Mapster;
using Portfolio.Databridgez.Domain.Dtos.Post;
using Portfolio.DataBridgez.Domain.Entities;

namespace Portfolio.Databridgez.Application.Mappings;

public static class MappingFunctions
{
    public static AppUser MapUserToExistingUser(this AppUser source, AppUser destination)
    {
        source.Adapt(destination);
        return destination;
    }
    
    public static AppUser MapRegisterUserInputToExistingUser(this RegisterInput source, AppUser destination)
    {
        source.Adapt(destination);
        return destination;
    }
}