using System.Runtime.Serialization;

namespace IraoGpi.Application.Management.Members.Requests;

[DataContract]
public class UpdateMemberRequest : CreateMemberRequest
{
    [DataMember]
    public int Id { get; set; }
}
