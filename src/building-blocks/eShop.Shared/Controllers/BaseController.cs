using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Shared.Controllers;

[ApiController]
[Route("api/v1[controller]/[action]")]
public class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;

    public BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }
}