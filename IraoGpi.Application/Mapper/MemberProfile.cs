using AutoMapper;
using IraoGpi.Application.Management.Members.Dtos;
using IraoGpi.Application.Management.Members.Requests;
using IraoGpi.Domain.Entities;

namespace IraoGpi.Application.Mapper;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, MemberDto>();
        CreateMap<UpdateMemberRequest, Member>();
    }
}
