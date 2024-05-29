using AutoMapper;
using IraoGpi.Application.Abstractions.Responses;
using IraoGpi.Application.Shared;
using IraoGpi.Domain.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IraoGpi.Domain.Entities;
using IraoGpi.Application.Management.Auth.Requests;
using IraoGpi.Application.Management.Members.Dtos;
using IraoGpi.Domain.Abstractions.Repository;
using LoginRequest = IraoGpi.Application.Management.Auth.Requests.LoginRequest;


namespace IraoGpi.Application.Management.Auth;

public class AuthService
{
    private readonly IMemberRepository _userRepository;
    private readonly SignInManager<Member> _signInManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthService(IMemberRepository userRepository, SignInManager<Member> signInManager, IMapper mapper)
    {
        _userRepository = userRepository;
        _signInManager = signInManager;
        _mapper = mapper;
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    }

    public async Task<IResponse<LoginResponse>> Login(LoginRequest request)
    {
        var user = await _userRepository.GetByMemberName(request.Username);

        if (user == null) return ResponseHelper.Fail<LoginResponse>(StatusCode.MemberNotFound);

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
            return ResponseHelper.Fail<LoginResponse>(StatusCode.InvalidMemberNameOrPassword);
        }

        var userInfo = _mapper.Map<MemberDto>(user);

        var accessToken = await GenerateAccessToken(user);

        return ResponseHelper.Ok(new LoginResponse { AccessToken = accessToken, Member = userInfo });
    }

    private async Task<string> GenerateAccessToken(Member user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
        };

        var roles = await _signInManager.UserManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.ASCII
            .GetBytes(_configuration.GetSection("Security:Secret").Value));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(15),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
