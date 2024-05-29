using System.Runtime.Serialization;

namespace IraoGpi.Application.Management.Members.Requests;

[DataContract]
public class DeleteMemberRequest
{
    [DataMember]
    public int Id { get; set; }
}
