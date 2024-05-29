using IraoGpi.Application.Management.Tasks.Dtos;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace IraoGpi.Application.Management.Tasks.Requests;

[DataContract]
public class CreateTaskRequest
{
    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public int Priority { get; set; }

    [JsonIgnore]
    public int MemberId { get; set; }
}

public class CreateTaskResponse
{
    public TaskDto Task { get; set; }
}
