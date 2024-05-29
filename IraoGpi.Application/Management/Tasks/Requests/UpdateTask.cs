using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IraoGpi.Application.Management.Tasks.Requests;

[DataContract]
public class UpdateTaskRequest
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public int Priority { get; set; }

    [JsonIgnore]
    public int MemberId { get; set; }
}

