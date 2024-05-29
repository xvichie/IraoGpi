using IraoGpi.API.Helpers.Extensions;
using IraoGpi.API.Helpers.Filters;
using IraoGpi.Application.Abstractions.Responses;
using IraoGpi.Application.Management.Members;
using IraoGpi.Application.Management.Members.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IraoGpi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Member")]
public class MembersController : ControllerBase
{
    private readonly MemberService _userService;

    public MembersController(MemberService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Support")]
    public async Task<IActionResult> GetMembersAsync([FromQuery] GetMembersRequest request)
    {
        var response = await _userService.Get(request);

        Response.AddPagination(response.Data.CurrentPage, response.Data.ItemsPerPage,
            response.Data.TotalItems, response.Data.TotalPages);

        return Ok(response);
    }

    [HttpPost]
    [ValidateModel]
    [AllowAnonymous]
    public async Task<IActionResult> CreateMemberAsync([FromBody] CreateMemberRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _userService.Create(request, cancellationToken);

        return Ok(response);
    }

    [HttpPut]
    [ValidateModel]
    public async Task<IActionResult> UpdateMemberAsync([FromBody] UpdateMemberRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _userService.Update(request, cancellationToken);

        return Ok(response);
    }

    [HttpDelete]
    [ValidateModel]
    public async Task<IActionResult> DeleteMemberAsync(DeleteMemberRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _userService.Delete(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [ValidateModel]
    [ProducesResponseType(typeof(GetMemberResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetMemberAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _userService.Get(new GetMemberRequest { Id = id }, cancellationToken);

        return Ok(response);
    }
}
