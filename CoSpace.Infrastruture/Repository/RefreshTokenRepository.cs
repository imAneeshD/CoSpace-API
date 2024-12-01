using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class RefreshTokenRepository(ApplicationDbContext dbContext, RepositoryBase<UserRole> repositoryBase, ICurrentUserService currentUserService) : IRefreshTokenRepository
    {

        public async Task<RefreshToken> GetRefreshTokenAsync(string refreshToken)
        {
            var existingRefreshToken = dbContext.RefreshToken
                .Where(rt => rt.Token == refreshToken && rt.Expires > DateTime.Now && !rt.IsRevoked)
                .FirstOrDefaultAsync();

            if (existingRefreshToken != null)
            {
                return existingRefreshToken.Result;
            }
            return null;
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            dbContext.RefreshToken.Add(refreshToken);
            await dbContext.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<RefreshToken> UpdateRefreshToken(RefreshToken refreshToken)
        {
            dbContext.RefreshToken.Update(refreshToken);
            await dbContext.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<bool> DeleteRefreshToken()
        {
            try
            {

                var refreshTokens = await dbContext.RefreshToken
                    .Where(rt => rt.UserId == currentUserService.UserId
                    && rt.OrganizationId == currentUserService.OrgId)
                    .ToListAsync();

                if (refreshTokens == null || !refreshTokens.Any())
                {
                    return false;
                }

                dbContext.RefreshToken.RemoveRange(refreshTokens);

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
