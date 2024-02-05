using Api.Temperature.Data.Context;
using Api.Temperature.Data.Entity;
using Api.Temperature.Data.Repository.Contract;
using Microsoft.EntityFrameworkCore;


namespace Api.Temperature.Data.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentityDBContext _context;

        public RefreshTokenRepository(IdentityDBContext context)
        {
            _context = context;
        }

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string refreshToken)
        {
            return await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == refreshToken);
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
        }

    }
}
