using IraoGpi.Application.Management.Members.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IraoGpi.Application.Management.Auth.Requests;


public class LoginRequest
{
    public string Username { get; set; }

    public string Password { get; set; }
}

public class LoginResponse
{
    public string AccessToken { get; set; }

    public MemberDto Member { get; set; }
}

