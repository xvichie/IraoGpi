using System.Runtime.Serialization;

namespace IraoGpi.Application.Management.Members.Requests;

[DataContract]
public class UpdateMemberRequest
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string FirstName { get; set; }

    [DataMember]
    public string LastName { get; set; }

    [DataMember]
    public string Password { get; set; }

    [DataMember]
    public int Type { get; set; }
}
