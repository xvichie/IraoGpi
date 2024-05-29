using IraoGpi.Application.Management.Tasks.Dtos;
using IraoGpi.Application.Shared;
using System.Runtime.Serialization;

namespace IraoGpi.Application.Management.Tasks.Requests;

[DataContract]
public class GetTasksRequest : Request
{
    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public int? Priority { get; set; }

    [DataMember]
    public int? Status { get; set; }
}

public class GetTasksResponse : PaginationHeader
{
    public IEnumerable<TaskDto> Tasks { get; }

    public GetTasksResponse(IEnumerable<TaskDto> taskDtos,
        int currentPage, int itemsPerPage, int totalItems, int totalPages)
        : base(currentPage, itemsPerPage, totalItems, totalPages)
    {
        Tasks = taskDtos;
    }
}

