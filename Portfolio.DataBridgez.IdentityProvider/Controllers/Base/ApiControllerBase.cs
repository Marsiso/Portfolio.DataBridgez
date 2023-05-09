using Microsoft.AspNetCore.Mvc;

namespace Portfolio.DataBridgez.IdentityProvider.Controllers.Base;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private readonly ILogger _logger;

    protected ApiControllerBase(ILogger logger)
    {
        _logger = logger;
    }
}