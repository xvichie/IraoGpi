using IraoGpi.Application.Management.Members.Dtos;
using System.Runtime.Serialization;

namespace IraoGpi.Application.Management.Members.Requests;

[DataContract]
public class CreateMemberRequest
{
    [DataMember]
    public string FirstName { get; set; }

    [DataMember]
    public string LastName { get; set; }

    [DataMember]
    public string UserName { get; set; }

    [DataMember]
    public string Password { get; set; }

    [DataMember]
    public int Type { get; set; }
}

public class CreateMemberResponse
{
    public MemberDto Member { get; set; }
}
