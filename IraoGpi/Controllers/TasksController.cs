using IraoGpi.API.Helpers.Extensions;
using IraoGpi.API.Helpers.Filters;
using IraoGpi.Application.Abstractions.Responses;
using IraoGpi.Application.Management.Members;
using IraoGpi.Application.Management.Members.Requests;
using IraoGpi.Application.Management.Tasks.Requests;
using IraoGpi.Application.Management.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IraoGpi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Support,User")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetTasksAsync([FromQuery] GetTasksRequest request)
    {
        var response = await _taskService.Get(request);

        Response.AddPagination(response.Data.CurrentPage, response.Data.ItemsPerPage,
            response.Data.TotalItems, response.Data.TotalPages);

        return Ok(response);
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskRequest request,
        CancellationToken cancellationToken)
    {
        request.MemberId = int.Parse(User.GetUserId());
        var response = await _taskService.Create(request, cancellationToken);

        return Ok(response);
    }

    [HttpPut]
    [ValidateModel]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateTaskAsync([FromBody] UpdateTaskRequest request,
        CancellationToken cancellationToken)
    {
        request.MemberId = int.Parse(User.GetUserId());

        var response = await _taskService.Update(request, cancellationToken);

        return Ok(response);
    }

    [HttpDelete]
    [ValidateModel]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteTaskAsync(DeleteTaskRequest request,
        CancellationToken cancellationToken)
    {
        request.MemberId = int.Parse(User.GetUserId());

        var response = await _taskService.Delete(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [ValidateModel]
    [ProducesResponseType(typeof(GetTaskByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTaskAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _taskService.Get(new GetTaskByIdRequest { Id = id }, cancellationToken);

        return Ok(response);
    }

    [HttpPut("{id:int}/status")]
    [ValidateModel]
    [Authorize(Roles = "Support")]
    public async Task<IActionResult> UpdateTaskStatusAsync(int id, [FromBody] UpdateTaskStatusRequest request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        request.MemberId = int.Parse(User.GetUserId());

        var response = await _taskService.UpdateStatus(request, cancellationToken);

        return Ok(response);
    }
}
