using IraoGpi.Domain.Abstractions.Repositories;
using IraoGpi.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IraoGpi.Domain.Abstractions.Repository;

public interface IMemberRepository : IRepository<Member>
{
    Task<Member> GetByMemberName(string userName, CancellationToken cancellationToken = default);
}
