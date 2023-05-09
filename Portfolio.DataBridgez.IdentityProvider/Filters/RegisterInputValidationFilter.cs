using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Portfolio.Databridgez.Application.Mappings;
using Portfolio.DataBridgez.Domain.Dtos.Get;
using Portfolio.Databridgez.Domain.Dtos.Post;
using Portfolio.Databridgez.Domain.Validators;
using static System.String;

namespace Portfolio.DataBridgez.IdentityProvider.Filters;

public sealed class RegisterInputValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Get necessary route data
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];
        var registerUserInput = (RegisterInput?)context.ActionArguments
            .SingleOrDefault(keyValuePair => keyValuePair.Value?.GetType() == typeof(RegisterInput))
            .Value;
        
        // KeyValue not found
        if (registerUserInput == null)
        {
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            var response = new RegisterResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Format("Object sent from the client is null. Controller: {0}, action: {1}", controller, action),
            };
            
            context.Result = new BadRequestObjectResult(response);
            return;
        }

        // Validation
        var validator = new RegisterInputValidator();
        var validationResult = validator.Validate(registerUserInput);
        if (!validationResult.IsValid)
        {
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            var response = new RegisterResponse
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity,
                Message = Format("Object model sent from the client is invalid. Controller: {0}, action: {1}", controller, action),
                ValidationFailures =  validationResult.Errors.MapSourceToDestination(new List<ValidationFailureResponse>())
            };
            
            context.Result = new UnprocessableEntityObjectResult(response);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}