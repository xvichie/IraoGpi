using AutoMapper;
using IraoGpi.Application.Abstractions.Responses;
using IraoGpi.Application.Management.Members.Dtos;
using IraoGpi.Application.Management.Members.Requests;
using IraoGpi.Application.Shared;
using IraoGpi.Domain.Abstractions.Repository;
using IraoGpi.Domain.Entities;
using IraoGpi.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IraoGpi.Application.Management.Members
{
    public class MemberService
    {
        private readonly UserManager<Member> _userManager;
        private readonly IMemberRepository _userRepository;
        private readonly IMapper _mapper;

        public MemberService(UserManager<Member> userManager, IMemberRepository userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IResponse<GetMembersResponse>> Get(GetMembersRequest request)
        {
            var query = _userRepository.GetAll();

            #region Filter

            if (!string.IsNullOrWhiteSpace(request.FirstName))
            {
                query = query.Where(user => user.FirstName.Equals(request.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(request.LastName))
            {
                query = query.Where(user => user.LastName.Equals(request.LastName));
            }

            if (!string.IsNullOrWhiteSpace(request.MemberName))
            {
                query = query.Where(user => user.UserName.Equals(request.MemberName));
            }

            #endregion

            var pagedList = await PagedList<Member>.CreateAsync(query, request.PageNumber, request.PageSize);

            var userModels = _mapper.Map<IEnumerable<MemberDto>>(pagedList);

            var response = new GetMembersResponse(userModels, pagedList.CurrentPage, pagedList.PageSize,
                pagedList.TotalCount, pagedList.TotalPages);

            return ResponseHelper.Ok(response);
        }

        public async Task<IResponse<GetMemberResponse>> Get(GetMemberRequest request,
            CancellationToken cancellationToken = default)
        {
            var query = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (query == null) return ResponseHelper.Fail<GetMemberResponse>(StatusCode.MemberNotFound);

            var user = _mapper.Map<MemberDto>(query);

            return ResponseHelper.Ok(new GetMemberResponse { Member = user });
        }

        public async Task<IResponse<CreateMemberResponse>> Create(CreateMemberRequest request,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByMemberName(request.MemberName, cancellationToken);
            if (user != null)
            {
                return ResponseHelper.Fail<CreateMemberResponse>(StatusCode.MembernameAlreadyExists);
            }

            user = Member.Create(request.FirstName, request.LastName, request.MemberName);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return ResponseHelper.Fail<CreateMemberResponse>(StatusCode.CannotCreateMember);
            }

            var role = Enum.GetName(typeof(MemberType), request.Type);
            var roleAdded = await _userManager.AddToRoleAsync(user, role);
            if (!roleAdded.Succeeded)
            {
                return ResponseHelper.Fail<CreateMemberResponse>(StatusCode.CannotAssignRoleToMember);
            }

            var userModel = _mapper.Map<MemberDto>(user);

            return ResponseHelper.Ok(new CreateMemberResponse { Member = userModel });
        }

        public async Task<IResponse<EmptyResponse>> Update(UpdateMemberRequest request,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null) return ResponseHelper.Fail(StatusCode.MemberNotFound);

            var userByMembername = await _userRepository.GetByMemberName(request.MemberName, cancellationToken);
            if (user.UserName == userByMembername.UserName && user.Id != userByMembername.Id)
                return ResponseHelper.Fail(StatusCode.MembernameAlreadyExists);

            _mapper.Map(request, user);

            var role = Enum.GetName(typeof(MemberType), request.Type);
            var isInRole = await _userManager.IsInRoleAsync(user, role);
            if (!isInRole)
            {
                var oldRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, oldRoles);

                var roleAdded = await _userManager.AddToRoleAsync(user, role);
                if (!roleAdded.Succeeded)
                {
                    return ResponseHelper.Fail(StatusCode.CannotAssignRoleToMember);
                }
            }

            var result = await _userManager.UpdateAsync(user);
            return !result.Succeeded ? ResponseHelper.Fail() : ResponseHelper.Ok();
        }

        public async Task<IResponse<EmptyResponse>> Delete(DeleteMemberRequest request,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null) return ResponseHelper.Fail(StatusCode.MemberNotFound);

            user.MarkAsDeleted();

            _userRepository.Update(user);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return ResponseHelper.Ok();
        }
    }
}
