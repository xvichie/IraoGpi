using IraoGpi.Domain.Abstractions.Repository;
using IraoGpi.Domain.Entities;
using IraoGpi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IraoGpi.Infrastructure.Repositories;

public class MemberRepository : BaseRepository<Member>, IMemberRepository
{
    public MemberRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Member> GetByUserName(string userName, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Member>().FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
    }
}
