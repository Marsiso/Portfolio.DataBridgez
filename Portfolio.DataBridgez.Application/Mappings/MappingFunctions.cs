using FluentValidation.Results;
using Mapster;
using Portfolio.DataBridgez.Domain.Dtos.Get;
using Portfolio.Databridgez.Domain.Dtos.Post;
using Portfolio.DataBridgez.Domain.Entities;

namespace Portfolio.Databridgez.Application.Mappings;

public static class MappingFunctions
{
    public static AppUser MapUserToExistingUser(this AppUser source, AppUser destination)
    {
        return source.Adapt(destination);
    }
    
    public static AppUser MapRegisterUserInputToExistingUser(this RegisterInput source, AppUser destination)
    {
        return source.Adapt(destination);
    }

    public static TDestination MapSourceToDestination<TSource, TDestination>(this TSource source,
        TDestination destination) where TSource : class where TDestination : class
    {
        return source.Adapt(destination);
    }
}