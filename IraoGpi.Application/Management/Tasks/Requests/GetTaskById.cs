using IraoGpi.Application.Management.Tasks.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IraoGpi.Application.Management.Tasks.Requests;

[DataContract]
public class GetTaskByIdRequest
{
    [DataMember]
    public int Id { get; set; }
}

public class GetTaskByIdResponse
{
    public TaskDto Task { get; set; }
}

