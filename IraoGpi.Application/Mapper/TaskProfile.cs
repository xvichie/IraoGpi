﻿using AutoMapper;
using IraoGpi.Application.Management.Tasks.Dtos;
using IraoGpi.Application.Management.Tasks.Requests;
using Task = IraoGpi.Domain.Entities.Task;

namespace IraoGpi.Application.Mapper;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Task, TaskDto>();
        CreateMap<UpdateTaskRequest, Task>();
    }
}
