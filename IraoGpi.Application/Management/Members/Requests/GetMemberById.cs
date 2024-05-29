using IraoGpi.Application.Management.Members.Dtos;
using System.Runtime.Serialization;

namespace IraoGpi.Application.Management.Members.Requests;

[DataContract]
public class GetMemberRequest
{
    [DataMember]
    public int Id { get; set; }
}

public class GetMemberResponse
{
    public MemberDto Member { get; set; }
}
