using IraoGpi.Application.Management.Members.Dtos;
using System.Runtime.Serialization;

namespace IraoGpi.Application.Management.Members.Requests;

[DataContract]
public class GetMemberByIdRequest
{
    [DataMember]
    public int Id { get; set; }
}

public class GetMemberByIdResponse
{
    public MemberDto Member { get; set; }
}
