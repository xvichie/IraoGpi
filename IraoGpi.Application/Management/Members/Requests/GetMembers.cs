using IraoGpi.Application.Management.Members.Dtos;
using IraoGpi.Application.Shared;
using System.Runtime.Serialization;

namespace IraoGpi.Application.Management.Members.Requests;

[DataContract]
public class GetMembersRequest : Request
{
    [DataMember]
    public string? FirstName { get; set; }

    [DataMember]
    public string? LastName { get; set; }

    [DataMember]
    public string? UserName { get; set; }
}

public class GetMembersResponse : PaginationHeader
{
    public IEnumerable<MemberDto> Members { get; }

    public GetMembersResponse(IEnumerable<MemberDto> userModels,
        int currentPage, int itemsPerPage, int totalItems, int totalPages)
        : base(currentPage, itemsPerPage, totalItems, totalPages)
    {
        Members = userModels;
    }
}
