using Application.Services.Test.Command.CreateTest;
using Application.Services.Test.Query;
using Core.Entities.Test;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Test;

[ApiController]
[Route("api/test")]
public class TestController(ISender sender) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult<EntityBoundTest>> CreateAsync(string name)
    {
        var result = await sender.Send(new CreateTestCommand(name));
        return Ok(result);
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<List<EntityBoundTest>>> GetAllAsync()
    {
        var result = await sender.Send(new GetListModel());
        return Ok(result);
    }
}
