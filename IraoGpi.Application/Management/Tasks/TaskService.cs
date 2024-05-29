using AutoMapper;
using IraoGpi.Application.Abstractions.Responses;
using IraoGpi.Application.Management.Tasks.Dtos;
using IraoGpi.Application.Management.Tasks.Requests;
using IraoGpi.Application.Shared;
using IraoGpi.Domain.Abstractions.Repositories;
using IraoGpi.Domain.Enums;
using Task = IraoGpi.Domain.Entities.Task;
using TaskStatus = IraoGpi.Domain.Enums.TaskStatus;

namespace IraoGpi.Application.Management.Tasks;

public class TaskService
{
    private readonly IRepository<Task> _taskRepository;
    private readonly IMapper _mapper;

    public TaskService(IRepository<Task> taskRepository,
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<IResponse<GetTasksResponse>> Get(GetTasksRequest request)
    {
        var tasks = _taskRepository.GetAll();

        #region Filtering

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            tasks = tasks.Where(task => task.Title.Contains(request.Title));
        }

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            tasks = tasks.Where(task => task.Description.Contains(request.Description));
        }

        if (request.Priority.HasValue)
        {
            tasks = tasks.Where(task => (int)task.Priority == request.Priority);
        }

        if (request.Status.HasValue)
        {
            tasks = tasks.Where(task => (int)task.Status == request.Status);
        }

        #endregion

        var pagedList = await PagedList<Task>.CreateAsync(tasks, request.PageNumber, request.PageSize);

        var taskModels = _mapper.Map<IEnumerable<TaskDto>>(pagedList);
        var response = new GetTasksResponse(taskModels, pagedList.CurrentPage, pagedList.PageSize, pagedList.TotalCount,
            pagedList.TotalPages);

        return ResponseHelper.Ok(response);
    }
    public async Task<IResponse<GetTaskByIdResponse>> Get(GetTaskByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

        if (task == null) return ResponseHelper.Fail<GetTaskByIdResponse>(StatusCode.TaskNotFound);

        var taskModel = _mapper.Map<TaskDto>(task);

        return ResponseHelper.Ok(new GetTaskByIdResponse { Task = taskModel });
    }

    public async Task<IResponse<CreateTaskResponse>> Create(CreateTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var task = Task.Create(request.Title, request.Description, (TaskPriority)request.Priority, request.MemberId);

        await _taskRepository.AddAsync(task, cancellationToken);
        await _taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        var taskModel = _mapper.Map<TaskDto>(task);

        return ResponseHelper.Ok(new CreateTaskResponse { Task = taskModel });
    }

    public async Task<IResponse<EmptyResponse>> Update(UpdateTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

        if (task == null) return ResponseHelper.Fail(StatusCode.TaskNotFound);

        _mapper.Map(request, task);

        _taskRepository.Update(task);
        await _taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseHelper.Ok();
    }

    public async Task<IResponse<EmptyResponse>> Delete(DeleteTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

        if (task == null) return ResponseHelper.Fail(StatusCode.TaskNotFound);

        task.MarkAsDeleted();

        _taskRepository.Update(task);
        await _taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseHelper.Ok();
    }

    public async Task<IResponse<EmptyResponse>> UpdateStatus(UpdateTaskStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

        if (task == null) return ResponseHelper.Fail(StatusCode.TaskNotFound);

        task.SetStatus((TaskStatus)request.Status);

        _taskRepository.Update(task);
        await _taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseHelper.Ok();
    }
}
